namespace KironWebAPI.Core.Entities
{
    public class MappingCountryEvent : BaseEntity
    {
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }

        public MappingCountryEvent()
        {
            IsActive = true;
            Created = DateTime.Now;
        }
    }
}
