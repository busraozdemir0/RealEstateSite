using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DTOs.ServiceDtos;
using RealEstate.API.Models.Repositories.ServiceRepository;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;

        public ServicesController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceList()
        {
            var values = await _serviceRepository.GetAllService();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDto createServiceDto)
        {
            await _serviceRepository.CreateService(createServiceDto);
            return Ok("Hizmet kısmı başarılı bir şekilde eklendi.");
        }

        [HttpDelete("{serviceId}")] // Silme islemi
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            await _serviceRepository.DeleteService(serviceId);
            return Ok("Hizmet kısmı başarılı bir şekilde silindi.");
        }

        [HttpPut] // Guncelleme islemi
        public async Task<IActionResult> UpdateService(UpdateServiceDto updateServiceDto)
        {
            await _serviceRepository.UpdateService(updateServiceDto);
            return Ok("Hizmet kısmı başarılı bir şekilde güncellendi.");
        }

        [HttpGet("{serviceId}")]
        public async Task<IActionResult> GetService(int serviceId)
        {
            var value = await _serviceRepository.GetService(serviceId);
            return Ok(value);
        }
    }
}
