using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Models.Repositories.EstateAgentRepositories.DashboardRepositories.LastProductRepositories;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateAgentLastProductsController : ControllerBase
    {
        private readonly ILast5ProductRepository _last5ProductRepository;

        public EstateAgentLastProductsController(ILast5ProductRepository last5ProductRepository)
        {
            _last5ProductRepository = last5ProductRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLast5ProductAsync(int id)
        {
            var values = await _last5ProductRepository.GetLast5Product(id);
            return Ok(values);
        }
    }
}
