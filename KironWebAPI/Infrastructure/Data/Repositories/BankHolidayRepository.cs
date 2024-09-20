using KironWebAPI.Core.Entities;
using KironWebAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KironWebAPI.Infrastructure.Data.Repositories
{
    public class BankHolidayRepository : IBankHolidayRepository
    {
        public readonly DataContext _context;
        public BankHolidayRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Country>> GetAllRegions()
        {
            var regions = await _context.Countries.ToListAsync();
            return regions;
        }
        public async Task<List<Core.Entities.MappingCountryEvent>> GetBankHolidaysForRegion(int regionId)
        {
            var result = await _context.MappingCountryEvents
               .Include(x => x.Country)
               .Include(x => x.Event)
               .Where(x => x.Country.Id == regionId).ToListAsync();
            return result;
        }
        public async Task<Core.Entities.Event> CheckEventExists(Core.Entities.Event holidayEvent)
        {
            var result = await _context.Events.FirstOrDefaultAsync(x => x.Title == holidayEvent.Title && x.Date.Date == holidayEvent.Date.Date);
            return result;
        }
        public async Task<Core.Entities.Country> CheckCountryExists(Core.Entities.Country country)
        {
            var result = await _context.Countries.FirstOrDefaultAsync(x => x.Division == country.Division);
            return result;
        }
        public async Task<Core.Entities.MappingCountryEvent> CheckCountryAndEventMapping(int countryId, int eventId)
        {
            var result = await _context.MappingCountryEvents
                .Include(x => x.Country)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.CountryId == countryId && x.EventId == eventId);
            return result;
        }
        public async Task<List<Core.Entities.MappingCountryEvent>> ValidateCountryMappingRecords(string division)
        {
            var result = await _context.MappingCountryEvents
                .Include(x => x.Country)
                .Include(x => x.Event)
                .Where(x => x.Country.Division == division).ToListAsync();
            return result;
        }
        public async Task<int> SaveCountryAndEventMapping(Core.Entities.Country country, Core.Entities.Event holidayEvent)
        {
            var mappingObj = new MappingCountryEvent()
            {
                CountryId = country.Id,
                //Country = country,
                EventId = holidayEvent.Id,
                //Events = holidayEvent
            };

            await _context.MappingCountryEvents.AddAsync(mappingObj);
            await _context.SaveChangesAsync();
            return mappingObj.Id;
        }
        public async Task<int> SaveCountry(Core.Entities.Country country)
        {
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return country.Id;
        }
        public async Task<int> SaveEvent(Core.Entities.Event holidayEvent)
        {
            await _context.Events.AddAsync(holidayEvent);
            await _context.SaveChangesAsync();
            return holidayEvent.Id;
        }
    }
}
