using System.Text.Json.Serialization;

namespace KironWebAPI.Core.Models.BankHolidays
{
    public class Scotland
    {
        [JsonPropertyName("division")]
        public string Division { get; set; }

        [JsonPropertyName("events")]
        public IEnumerable<Event> Events { get; set; }
    }
}
