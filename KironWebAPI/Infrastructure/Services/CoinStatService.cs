using KironWebAPI.Core.Interfaces;
using KironWebAPI.Core.Models.BankHolidays;
using KironWebAPI.Core.Models.CoinStats;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestSharp;


namespace KironWebAPI.Infrastructure.Services
{
    public class CoinStatService : ICoinStatService
    {
        private readonly ILogger<CoinStatService> _logger;
        private readonly IConfiguration _config;
        private readonly ICacheService _cacheService;
        private readonly string cachekey = "latestCoinStats";
        public CoinStatService(ILogger<CoinStatService> logger, IConfiguration config, ICacheService cacheService)
        {
            _logger = logger;
            _config = config;
            _cacheService = cacheService;
        }

        public async Task<CoinStats> GetlatestCoinStats()
        {
            try
            {
                var options = new RestClientOptions(_config["EndPoints:CoinStats"]);
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.AddHeader("accept", "application/json");
                request.AddHeader("X-API-KEY", _config["EndPoints:ApiKey"]);
                var response = await client.GetAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    var latestCoinStats = JsonConvert.DeserializeObject<CoinStats>(response.Content);
                     _cacheService.SetData(cachekey, latestCoinStats, 1);          
                    return latestCoinStats;
                }            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to get bank holidays");
            }
            return null;
        }
    }
}
