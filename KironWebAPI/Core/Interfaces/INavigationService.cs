using KironWebAPI.Core.Models.Navigation;

namespace KironWebAPI.Core.Interfaces
{
    public interface INavigationService
    {
        Task<List<MappedMenu>> GetMenuLinks();
    }
}
