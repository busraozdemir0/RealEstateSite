
using Dapper;
using RealEstate.API.DTOs.CategoryDtos;
using RealEstate.API.DTOs.ServiceDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.ServiceRepository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly Context _context;

        public ServiceRepository(Context context)
        {
            _context = context;
        }
        public void CreateService(CreateServiceDto createServiceDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteService(int serviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultServiceDto>> GetAllServiceAsync()
        {
            string query = "Select * From Service";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultServiceDto>(query); 
                return values.ToList();
            }
        }

        public Task<GetByIDServiceDto> GetService(int serviceId)
        {
            throw new NotImplementedException();
        }

        public void UpdateService(UpdateServiceDto updateServiceDto)
        {
            throw new NotImplementedException();
        }
    }
}
