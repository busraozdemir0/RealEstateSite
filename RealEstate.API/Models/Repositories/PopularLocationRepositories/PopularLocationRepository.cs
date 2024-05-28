using Dapper;
using RealEstate.API.DTOs.CategoryDtos;
using RealEstate.API.DTOs.PopularLocationDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.PopularLocationRepositories
{
    public class PopularLocationRepository : IPopularLocationRepository
    {
        private readonly Context _context;

        public PopularLocationRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<ResultPopularLocationDto>> GetAllPopularLocationAsync()
        {
            string query = "Select * From PopularLocation";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultPopularLocationDto>(query); // Kategorileri DTO yadimiyla listeleme
                return values.ToList();
            }
        }
    }
}
