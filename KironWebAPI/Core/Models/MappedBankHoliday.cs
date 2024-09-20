using KironWebAPI.Core.Entities;

namespace KironWebAPI.Core.Models
{
    public class MappedBankHoliday
    {
        public Country Country { get; set; }
        public List<Event> Events { get; set; }
    }
}
