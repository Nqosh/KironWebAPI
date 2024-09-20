namespace KironWebAPI.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
    }
}
