using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Models.Repositories.EstateAgentRepositories.DashboardRepositories.ChartRepositories;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateAgentChartsController : ControllerBase
    {
        private readonly IChartRepository _chartRepository;

        public EstateAgentChartsController(IChartRepository chartRepository)
        {
            _chartRepository = chartRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get5CityForChart()
        {
            return Ok(await _chartRepository.Get5CityForChart());
        }
    }
}
