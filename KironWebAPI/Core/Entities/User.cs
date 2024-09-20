namespace KironWebAPI.Core.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public User()
        {
            IsActive = true;
            Created = DateTime.Now;
        }
    }
}
