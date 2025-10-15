using System.Text.Json.Serialization;

namespace Sales.Infrastructure.External.ZarinPal
{
    public class ZarinPalRequestResponse
    {
        [JsonPropertyName("data")]
        public ZarinpalRequestData Data { get; set; }

        [JsonPropertyName("errors")]
        public ZarinPalError[] Errors { get; set; }
    }
    public class ZarinpalRequestData
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public string Authority { get; set; }

        public string FeeType { get; set; }

        public int Fee { get; set; }
    }
}
