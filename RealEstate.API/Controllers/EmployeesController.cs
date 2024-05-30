using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DTOs.EmployeeDtos;
using RealEstate.API.Models.Repositories.EmployeeRepositories;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeList()
        {
            var values = await _employeeRepository.GetAllEmployeeAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            _employeeRepository.CreateEmployee(createEmployeeDto);
            return Ok("Personel başarılı bir şekilde eklendi.");
        }

        [HttpDelete("{employeeId}")] // Silme islemi
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            _employeeRepository.DeleteEmployee(employeeId);
            return Ok("Personel başarılı bir şekilde silindi.");
        }

        [HttpPut] // Guncelleme islemi
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            _employeeRepository.UpdateEmployee(updateEmployeeDto);
            return Ok("Personel başarılı bir şekilde güncellendi.");
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployee(int employeeId)
        {
            var value = await _employeeRepository.GetEmployee(employeeId);
            return Ok(value);
        }
    }
}
