using KironWebAPI.Core.Entities;

namespace KironWebAPI.Core.Interfaces
{
    public interface IBankHolidayRepository
    {
        Task<int> SaveCountry(Country country);
        Task<int> SaveEvent(Event holidayEvent);
        Task<Event> CheckEventExists(Event holidayEvent);
        Task<List<Country>> GetAllRegions();
        Task<List<Core.Entities.MappingCountryEvent>> GetBankHolidaysForRegion(int regionId);
        Task<Core.Entities.Country> CheckCountryExists(Core.Entities.Country country);
        Task<int> SaveCountryAndEventMapping(Core.Entities.Country country, Core.Entities.Event holidayEvent);
        Task<List<Core.Entities.MappingCountryEvent>> ValidateCountryMappingRecords(string division);
        Task<Core.Entities.MappingCountryEvent> CheckCountryAndEventMapping(int countryId, int eventId);
    }
}
