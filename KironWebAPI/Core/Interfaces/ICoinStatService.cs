using KironWebAPI.Core.Models.CoinStats;

namespace KironWebAPI.Core.Interfaces
{
    public interface ICoinStatService
    {
        Task<CoinStats> GetlatestCoinStats();
    }
}
