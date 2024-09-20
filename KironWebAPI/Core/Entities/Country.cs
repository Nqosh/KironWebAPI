namespace KironWebAPI.Core.Entities
{
    public class Country : BaseEntity
    {
        public string Division { get; set; }
        public Country()
        {
            IsActive = true;
            Created = DateTime.Now;
        }
    }
}
