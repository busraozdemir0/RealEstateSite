using RealEstate.API.DTOs.EmployeeDtos;

namespace RealEstate.API.Models.Repositories.EmployeeRepositories
{
    public interface IEmployeeRepository
    {
        Task<List<ResultEmployeeDto>> GetAllEmployeeAsync();
        void CreateEmployee(CreateEmployeeDto createEmployeeDto);
        void DeleteEmployee(int employeeId);
        void UpdateEmployee(UpdateEmployeeDto updateEmployeeDto);
        Task<GetByIDEmployeeDto> GetEmployee(int employeeId);
    }
}
