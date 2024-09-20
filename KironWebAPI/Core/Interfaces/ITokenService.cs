using KironWebAPI.Core.Entities;

namespace KironWebAPI.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
