using KironWebAPI.Core.Entities;
using KironWebAPI.Core.Interfaces;
using KironWebAPI.Core.Models.Navigation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KironWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationController : ControllerBase
    {
        private readonly INavigationService _navigationService;

        public NavigationController(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        // GET: api/<NavigationController>
        [HttpGet]
        public async Task<List<MappedMenu>> GetMenuLinks()
        {
            var menuLinks = await _navigationService.GetMenuLinks();
            return menuLinks;
        }
    }
}
