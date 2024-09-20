using KironWebAPI.Core.Entities;
using KironWebAPI.Core.Interfaces;
using KironWebAPI.Core.Models.Navigation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KironWebAPI.Infrastructure.Data.Repositories
{
    public class NavigationRepository : INavigationRepository
    {
        public readonly DataContext _context;

        public NavigationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Navigation>> GetTopMenuLinks()
        {
            var topMenuLinks = await _context.Navigation.Where(x => x.ParentId == -1)
                                    .OrderBy(x => x.ParentId).ToListAsync();
            return topMenuLinks;
        }

        public async Task<List<Navigation>> GetChildMenuLinks(int topMenuId)
        {
           var childMenuLinkList = await _context.Navigation.Where(x => x.ParentId == topMenuId).ToListAsync();
            return childMenuLinkList;
        }

        private async Task<List<MappedMenu>> MapNavigationLinks(List<Navigation> topMenuLinks)
        {
            List<MappedMenu> menuLinks = new List<MappedMenu>();
            List<ChildMenu> childMenuLinks = new List<ChildMenu>();

            foreach (var topMenuLink in topMenuLinks)
            {
                var childMenuLinkList = await _context.Navigation.Where(x => x.ParentId == topMenuLink.Id).ToListAsync();
                childMenuLinks = new List<ChildMenu>();

                if(childMenuLinkList == null || childMenuLinkList.Count== 0)
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
