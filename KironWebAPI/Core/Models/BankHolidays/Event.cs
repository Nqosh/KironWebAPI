using System.Text.Json.Serialization;

namespace KironWebAPI.Core.Models.BankHolidays
{
    public class Event
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("notes")]
        public string Notes { get; set; }

        [JsonPropertyName("bunting")]
        public bool Bunting { get; set; }
    }
}
