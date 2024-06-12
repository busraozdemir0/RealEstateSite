using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Models.Repositories.AppRoleRepositories;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppRolesController : ControllerBase
    {
        private readonly IAppRoleRepository _appRoleRepository;

        public AppRolesController(IAppRoleRepository appRoleRepository)
        {
            _appRoleRepository = appRoleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> AppRoleList()
        {
            var values = await _appRoleRepository.GetAllAppRole();
            return Ok(values);
        }
    }
}
