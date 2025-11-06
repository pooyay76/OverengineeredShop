using Microsoft.Extensions.Options;
using Sales.Domain.BillAgg.Contracts;
using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common.ValueObjects;
using Sales.Infrastructure.Configurations;
using System.Text;
using System.Text.Json;

namespace Sales.Infrastructure.External.Client.ZarinPal
{
    public class ZarinPalService : IPaymentGatewayService
    {
        private readonly ZarinPalOptions zarinPalOptions;

        public ZarinPalService(IOptions<ZarinPalOptions> config)
        {
            zarinPalOptions = config.Value;
        }

        public async Task<string> GetNewSessionIdAsync(Money amount, BillId billId)
        {
            var callbackUrl = zarinPalOptions.CallBackUrl + $"{billId}";
            var requestData = new
            {
                merchant_id = zarinPalOptions.MerchantId,
                amount = amount.GetValue().ToString(),
                callback_url = callbackUrl,
                description = "test",
                metadata = new
                {
                    mobile = "09121234567",
                    email = "info.test@gmail.com"
                }
            };

            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(zarinPalOptions.RequestUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<ZarinPalRequestResponse>(responseContent, options);

                if (result?.Data?.Code == 100)
                {
                    return result.Data.Authority;
                }
                else
                    throw new Exception("Connection to payment gateway failed, contact admin");
                //else if (result?.Errors != null && result.Errors.Length > 0)
                //{
                //    Console.WriteLine("❌ Payment request failed:");
                //    foreach (var error in result.Errors)
                //    {
                //        Console.WriteLine($"- {error.Message} (Code: {error.Code})");
                //    }
                //}
                //else
                //{
                //    Console.WriteLine("❌ Unknown response or failure.");
                //    Console.WriteLine(responseContent); // for debugging
                //}
            }
        }
        public string GetPaymentPageUrl(string sessionId)
        {
            return zarinPalOptions.PaymentUrl + sessionId;
        }
        public async Task<bool> VerifyPaymentAsync(string sessionId, Money amount)
        {
            var requestData = new
            {
                merchant_id = zarinPalOptions.MerchantId,
                authority = sessionId,
                amount = amount.GetValue().ToString(),
            };

            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(zarinPalOptions.RequestUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<ZarinPalVerifyResponse>(responseContent, options);

                return result.Data.Code == 100 || result.Data.Code == 101;

            }

        }
    }
}
