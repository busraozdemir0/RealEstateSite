using Dapper;
using RealEstate.API.DTOs.BottomGridDtos;
using RealEstate.API.DTOs.CategoryDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.BottomGridRepositories
{
    public class BottomGridRepository : IBottomGridRepository
    {
        private readonly Context _context;

        public BottomGridRepository(Context context)
        {
            _context = context;
        }
        public void CreateBottomGrid(CreateBottomGridDto createBottomGridDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteBottomGrid(int bottomGridId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultBottomGridDto>> GetAllBottomGridAsync()
        {
            string query = "Select * From BottomGrid";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultBottomGridDto>(query); // Kategorileri DTO yadimiyla listeleme
                return values.ToList();
            }
        }

        public Task<GetByIDBottomGridDto> GetBottomGrid(int bottomGridId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBottomGrid(UpdateBottomGridDto updateBottomGridDto)
        {
            throw new NotImplementedException();
        }
    }
}
