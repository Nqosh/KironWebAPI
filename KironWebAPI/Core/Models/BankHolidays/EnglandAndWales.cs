using System.Text.Json.Serialization;

namespace KironWebAPI.Core.Models.BankHolidays
{
    public class EnglandAndWales
    {
        [JsonPropertyName("division")]
        public string Division { get; set; }

        [JsonPropertyName("events")]
        public List<Event> Events { get; set; }
    }
}
