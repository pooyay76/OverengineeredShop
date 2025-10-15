
using Newtonsoft.Json;

namespace Sales.Infrastructure.External.ZarinPal
{
    public class ZarinPalVerifyResponse
    {
        public ZarinPalVerifyData Data { get; set; }
        public ZarinPalError[] Errors { get; set; }
    }
    public class ZarinPalVerifyData
    {
        [JsonProperty("card_pan")]
        public int Code { get; set; }
        [JsonProperty("card_pan")]
        public string Message { get; set; }
        [JsonProperty("card_hash")]
        public string CardHash { get; set; }
        [JsonProperty("card_pan")]
        public string CardPan { get; set; }
        [JsonProperty("ref_id")]

        public int RefId { get; set; }
        [JsonProperty("fee_type")]

        public string FeeType { get; set; }
        [JsonProperty("card_pan")]
        public int Fee { get; set; }


    }
}
