using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Models.Repositories.EstateAgentRepositories.DashboardRepositories.StatisticRepositories;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateAgentDashboardStatisticsController : ControllerBase
    {
        private readonly IStatisticRepository _statisticRepository;

        public EstateAgentDashboardStatisticsController(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        [HttpGet("ProductCountByEmployeeId")]
        public IActionResult ProductCountByEmployeeId(int id)
        {
            return Ok(_statisticRepository.ProductCountByEmployeeId(id));
        }
        [HttpGet("ProductCountByStatusTrue")]
        public IActionResult ProductCountByStatusTrue(int id)
        {
            return Ok(_statisticRepository.ProductCountByStatusTrue(id));
        }
        [HttpGet("ProductCountByStatusFalse")]
        public IActionResult ProductCountByStatusFalse(int id)
        {
            return Ok(_statisticRepository.ProductCountByStatusFalse(id));
        }
        [HttpGet("AllProductCount")]
        public IActionResult AllProductCount()
        {
            return Ok(_statisticRepository.AllProductCount());
        }
    }
}
