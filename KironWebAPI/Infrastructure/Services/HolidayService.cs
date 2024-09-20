using KironWebAPI.Core.Entities;
using KironWebAPI.Core.Interfaces;
using KironWebAPI.Core.Models;
using KironWebAPI.Core.Models.BankHolidays;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace KironWebAPI.Infrastructure.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IBankHolidayRepository _bankHolidayRepository;
        private readonly ILogger<HolidayService> _logger;
        private readonly IConfiguration _config;
        public HolidayService(IBankHolidayRepository bankHolidayRepository, ILogger<HolidayService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _bankHolidayRepository = bankHolidayRepository;  
        }

        public async Task<BankHoliday> GetBankHolidays()
        {
            var rawJson = string.Empty;
            try
            {
                using (var webClient = new HttpClient())
                {
                    rawJson = await webClient.GetStringAsync(_config["EndPoints:BankHolidays"]);
                }

                var bankHolidays = JsonConvert.DeserializeObject<BankHoliday>(rawJson);
                if (bankHolidays == null)
                    return null;

                return bankHolidays;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to get bank holidays");
            }
            return null;
        }
        public async Task<List<Core.Entities.Country>> GetAllRegions()
        {
            try
            {
                var regions = await _bankHolidayRepository.GetAllRegions();
                return regions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to get all regions");
            }

            return new List<Core.Entities.Country>();
        }
        public async Task<MappedBankHoliday> GetBankHolidaysForRegion(int regionId)
        {
            List<Core.Entities.Event> bankHolidays = new List<Core.Entities.Event>();

            try
            {
                var mappingsList = await _bankHolidayRepository.GetBankHolidaysForRegion(regionId);

                if (mappingsList != null)
                {
                    foreach (var mappings in mappingsList)
                    {
                        var bankholiday = new Core.Entities.Event()
                        {
                            Id = mappings.Event.Id,
                            Title = mappings.Event.Title,
                            Date = mappings.Event.Date,
                            Notes = mappings.Event.Notes,
                            Bunting = mappings.Event.Bunting,
                            Created = mappings.Event.Created,
                            IsActive = mappings.Event.IsActive
                        };

                        bankHolidays.Add(bankholiday);
                    }
                }

                var mappedBankHoliday = new MappedBankHoliday()
                {
                    Country = mappingsList.FirstOrDefault().Country,
                    Events = bankHolidays
                };

                return mappedBankHoliday;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to get bank holidays for a region");
            }

            return null;
        }
        public async Task<bool> SaveBankHolidays(BankHoliday bankHoliday)
        {
            try
            {
                var scotland = bankHoliday.Scotland;
                var englandAndWales = bankHoliday.EnglandAndWales;
                var northenIreland = bankHoliday.NorthernIreland;

                if (scotland != null)
                {
                    await SaveRecordsPerRegion(scotland);
                }
                if (englandAndWales != null)
                {
                    await SaveRecordsPerRegion(englandAndWales);
                }
                if (northenIreland != null)
                {
                    await SaveRecordsPerRegion(northenIreland);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to save the regions and holidays");
                return false;
            }

            return true;
        }
        public async Task<bool> ValidateIfDatabaseHasBankHolidays(BankHoliday bankHoliday)
        {
            try
            {
                var scotland = bankHoliday.Scotland;
                var englandAndWales = bankHoliday.EnglandAndWales;
                var northenIreland = bankHoliday.NorthernIreland;

                var checkScotlandCountryEventMapping = await _bankHolidayRepository.ValidateCountryMappingRecords(scotland.Division);
                var checkEnglandAndWalesCountryEventMapping = await _bankHolidayRepository.ValidateCountryMappingRecords(englandAndWales.Division);
                var checkNorthenIrelandCountryEventMapping = await _bankHolidayRepository.ValidateCountryMappingRecords(northenIreland.Division);

                if ((checkScotlandCountryEventMapping != null && checkScotlandCountryEventMapping.Count > 0)
                    && (checkEnglandAndWalesCountryEventMapping != null && checkEnglandAndWalesCountryEventMapping.Count > 0)
                    && (checkNorthenIrelandCountryEventMapping != null && checkNorthenIrelandCountryEventMapping.Count > 0))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to validate if database has bank holidays");
                return false;
            }
            
            return true;
        }
        private async Task<bool> SaveRecordsPerRegion(Core.Models.BankHolidays.Country country)
        {
            try
            {
                int countryId = 0;
                int holidayEventId = 0;
                var countryObj = new Core.Entities.Country()
                {
                    Division = country.Division,
                };

                var checkCountry = await _bankHolidayRepository.CheckCountryExists(countryObj);

                if ((checkCountry == null))
                {
                    countryId = await _bankHolidayRepository.SaveCountry(countryObj);
                }
                else
                {
                    countryId = checkCountry.Id;
                }

                foreach (var item in country.Events)
                {
                    var holidayEventObj = new Core.Entities.Event()
                    {
                        Title = item.Title,
                        Date = Convert.ToDateTime(item.Date),
                        Bunting = item.Bunting,
                        Notes = item.Notes,
                    };

                    var checkholidayEvent = await _bankHolidayRepository.CheckEventExists(holidayEventObj);

                    if ((checkholidayEvent == null))
                    {
                        holidayEventId = await _bankHolidayRepository.SaveEvent(holidayEventObj);

                    }
                    else
                    {
                        holidayEventId = checkholidayEvent.Id;
                    }

                    if (countryId > 0 && holidayEventId > 0)
                    {
                        countryObj.Id = countryId;
                        holidayEventObj.Id = holidayEventId;
                        var checkCountryEventMapping = await _bankHolidayRepository.CheckCountryAndEventMapping(countryId, holidayEventId);
                        if((checkCountryEventMapping != null)) continue;
                        await _bankHolidayRepository.SaveCountryAndEventMapping(countryObj, holidayEventObj);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to save records per region");
                return false;
            }
            return true;
        }
    }
}
