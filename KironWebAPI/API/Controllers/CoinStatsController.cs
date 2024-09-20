using KironWebAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KironWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinStatsController : ControllerBase
    {
        private readonly ICoinStatService _coinStatService;
        public CoinStatsController(ICoinStatService coinStatService) 
        {
            _coinStatService = coinStatService;
        }

        /// <summary>  
        /// The end point https://api.coinstats.app/public/v1/coins has been deprecated 
        /// and was disabled on Oct 31 2023
        /// </summary> 
        [HttpGet("coinstats")]
        public async Task<IActionResult> GetCoinStats()
        {
            var latestCoinStats = await _coinStatService.GetlatestCoinStats();

            if (latestCoinStats == null)
            {
                return BadRequest("Latest Coin Stats not found");
            }

            return Ok(latestCoinStats);
        }
    }
}
