using System.Text.Json.Serialization;

namespace KironWebAPI.Core.Models.CoinStats
{
    public class CoinStats
    {
        [JsonPropertyName("result")]
        public IEnumerable<Result> Result { get; set; }
    }

   
}
