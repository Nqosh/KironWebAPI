using System.Text.Json.Serialization;

namespace KironWebAPI.Core.Models.CoinStats
{
    public class Result
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("price")]
        public float Price { get; set; }

        [JsonPropertyName("priceBtc")]
        public float PriceBtc { get; set; }

        [JsonPropertyName("volume")]
        public float Volume { get; set; }

        [JsonPropertyName("marketCap")]
        public float MarketCap { get; set; }

        [JsonPropertyName("availableSupply")]
        public long AvailableSupply { get; set; }

        [JsonPropertyName("totalSupply")]
        public long TotalSupply { get; set; }

        [JsonPropertyName("fullyDilutedValuation")]
        public float FullyDilutedValuation { get; set; }

        [JsonPropertyName("priceChange1h")]
        public float PriceChange1h { get; set; }

        [JsonPropertyName("priceChange1d")]
        public float PriceChange1d { get; set; }

        [JsonPropertyName("priceChange1w")]
        public float PriceChange1w { get; set; }

        [JsonPropertyName("redditUrl")]
        public string RedditUrl { get; set; }

        [JsonPropertyName("twitterUrl")]
        public string TwitterUrl { get; set; }

        [JsonPropertyName("websiteUrl")]
        public string WebsiteUrl { get; set; }

        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; }

        [JsonPropertyName("decimals")]
        public int Decimals { get; set; }
    }
}
