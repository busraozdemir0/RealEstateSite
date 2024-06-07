using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DTOs.BottomGridDtos;
using RealEstate.API.Models.Repositories.BottomGridRepositories;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BottomGridsController : ControllerBase
    {
        private readonly IBottomGridRepository _bottomGridRepository;

        public BottomGridsController(IBottomGridRepository bottomGridRepository)
        {
            _bottomGridRepository = bottomGridRepository;
        }

        [HttpGet]
        public async Task<IActionResult> BottomGridList()
        {
            var values = await _bottomGridRepository.GetAllBottomGrid();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBottomGrid(CreateBottomGridDto createBottomGridDto)
        {
            await _bottomGridRepository.CreateBottomGrid(createBottomGridDto);
            return Ok("Veri başarılı bir şekilde eklendi.");
        }

        [HttpDelete("{bottomGridId}")]
        public async Task<IActionResult> DeleteBottomGrid(int bottomGridId)
        {
            await _bottomGridRepository.DeleteBottomGrid(bottomGridId);
            return Ok("Veri başarılı bir şekilde silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBottomGrid(UpdateBottomGridDto updateBottomGridDto)
        {
            await _bottomGridRepository.UpdateBottomGrid(updateBottomGridDto);
            return Ok("Veri başarılı bir şekilde güncellendi.");
        }

        [HttpGet("{bottomGridId}")]
        public async Task<IActionResult> GetBottomGrid(int bottomGridId)
        {
            var value = await _bottomGridRepository.GetBottomGrid(bottomGridId);
            return Ok(value);
        }
    }
}
