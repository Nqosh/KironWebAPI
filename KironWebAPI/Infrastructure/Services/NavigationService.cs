using KironWebAPI.Core.Entities;
using KironWebAPI.Core.Interfaces;
using KironWebAPI.Core.Models.Navigation;
using Microsoft.EntityFrameworkCore;

namespace KironWebAPI.Infrastructure.Services
{
    public class NavigationService : INavigationService
    {
        private readonly INavigationRepository _navigationRepository;
        public NavigationService(INavigationRepository navigationRepository) 
        {
            _navigationRepository = navigationRepository;
        }
        public async Task<List<MappedMenu>> GetMenuLinks()
        {
            var topMenuLinks = await _navigationRepository.GetTopMenuLinks();

            var mappedNavigationList = await MapNavigationLinks(topMenuLinks);

            foreach (var topMenuLink in topMenuLinks)
            {

                var childMenuList = await _navigationRepository.GetChildMenuLinks(topMenuLink.Id);
                var mappedChildNavigationList = await MapNavigationLinks(childMenuList);

                if (mappedChildNavigationList.Count > 0)
                {
                    mappedNavigationList.AddRange(mappedChildNavigationList);
                }
            }

            return mappedNavigationList;
        }

        private async Task<List<MappedMenu>> MapNavigationLinks(List<Navigation> topMenuLinks)
        {
            List<MappedMenu> menuLinks = new List<MappedMenu>();
            List<ChildMenu> childMenuLinks = new List<ChildMenu>();

            foreach (var topMenuLink in topMenuLinks)
            {
                var childMenuLinkList = await _navigationRepository.GetChildMenuLinks(topMenuLink.Id);
                childMenuLinks = new List<ChildMenu>();

                if (childMenuLinkList == null || childMenuLinkList.Count == 0)
                    continue;

                foreach (var childMenuLink in childMenuLinkList)
                {
                    var childMenu = new ChildMenu()
                    {
                        Text = childMenuLink.Text
                    };

                    childMenuLinks.Add(childMenu);
                }

                var mappedMenu = new MappedMenu()
                {
                    NavText = topMenuLink.Text,
                    Children = childMenuLinks
                };

                menuLinks.Add(mappedMenu);
            }
            return menuLinks;
        }
    }
}
