using KironWebAPI.Core.Entities;
using KironWebAPI.Core.Models.Navigation;

namespace KironWebAPI.Core.Interfaces
{
    public interface INavigationRepository
    {
        Task<List<Navigation>> GetTopMenuLinks();
        Task<List<Navigation>> GetChildMenuLinks(int topMenuId);
    }
}
