using KironWebAPI.Core.Models;
using KironWebAPI.Core.Models.BankHolidays;

namespace KironWebAPI.Core.Interfaces
{
    public interface IHolidayService
    {
        Task<BankHoliday> GetBankHolidays();
        Task<List<Core.Entities.Country>> GetAllRegions();
        Task<MappedBankHoliday> GetBankHolidaysForRegion(int regionId);
        Task<bool> SaveBankHolidays(BankHoliday bankHoliday);
        Task<bool> ValidateIfDatabaseHasBankHolidays(BankHoliday bankHoliday);
    }
}
