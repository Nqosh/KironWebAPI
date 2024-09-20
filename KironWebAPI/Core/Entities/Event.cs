namespace KironWebAPI.Core.Entities
{
    public class Event : BaseEntity
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public bool Bunting { get; set; }

        public Event()
        {
            IsActive = true;
            Created = DateTime.Now;
        }
    }
}
