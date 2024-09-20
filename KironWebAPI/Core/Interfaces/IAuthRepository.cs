using KironWebAPI.Core.Entities;

namespace KironWebAPI.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string Email, string password);
        Task<bool> UserExist(string Email);
    }
}
