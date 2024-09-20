using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KironWebAPI.Core.Models.BankHolidays
{
    public class BankHoliday
    {
        [JsonProperty(PropertyName = "england-and-wales")]
        public Country EnglandAndWales { get; set; }

        [JsonPropertyName("scotland")]
        public Country Scotland { get; set; }

        [JsonProperty(PropertyName = "northern-ireland")]
        public Country NorthernIreland { get; set; }
    }
}
