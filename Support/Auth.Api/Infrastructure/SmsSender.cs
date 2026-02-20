using System.Text;
using System.Text.Json;

namespace Auth.Api.Infrastructure
{
    public static class SmsSender
    {
        public static void SendSmsCode(string phoneNumber, string code)
        {
            //call an api with a pre-bought token
            //token can reside in appsettings
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("x-api-key", "5AjUpQILp9t7D2UdaoaJxxxxJdXX0c1dAo456usriKbgyYXqblciFvTm5NLM2346Ipcs");

            var model = new
            {
                Mobile = "9120000000",
                TemplateId = 123456,
                Parameters = new[] {
                  new {
                    Name = "CODE", Value = code
                  }
                }
            };

            string payload = JsonSerializer.Serialize(model);
            StringContent stringContent = new(payload, Encoding.UTF8, "application/json");

            //HttpResponseMessage response = Task.Run(() => httpClient.PostAsync("https://api.sms.ir/v1/send/verify", stringContent)).GetAwaiter().GetResult();



        }
    }
}