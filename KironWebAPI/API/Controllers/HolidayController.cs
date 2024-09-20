using KironWebAPI.Core.Entities;
using KironWebAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KironWebAPI.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }


        // GET: api/<HolidaysController>
        [HttpGet("bankholidays")]
        public async Task<IActionResult> GetBankHolidays()
        {
            var holidays = await _holidayService.GetBankHolidays();

            if (holidays == null)
            {
                return BadRequest();
            }
            if(await _holidayService.ValidateIfDatabaseHasBankHolidays(holidays))
            {
                if (await _holidayService.SaveBankHolidays(holidays))
                {
                    return Ok();
                }
            }
            else
            {
                return BadRequest("The work for this endpoint has been fulfilled");
            }

            return BadRequest("Failed to Save or Update the Bank Holidays");
        }

        [HttpGet("regions")]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _holidayService.GetAllRegions();
            if (regions == null)
                return BadRequest("There are no Regions available");
            return Ok(regions);
        }

        [HttpGet("bankholidaysforregion")]
        public async Task<IActionResult> GetBankHolidaysForRegion(int regionId)
        {
            var bankHolidaysForRegion = await _holidayService.GetBankHolidaysForRegion(regionId);
            if (bankHolidaysForRegion == null)
                return BadRequest("There are no bank holidays for this region available");
            return Ok(bankHolidaysForRegion);
        }
    }
}
