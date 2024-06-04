using Dapper;
using RealEstate.API.DTOs.ChartDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.EstateAgentRepositories.DashboardRepositories.ChartRepositories
{
    public class ChartRepository : IChartRepository
    {
        private readonly Context _context;

        public ChartRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<ResultChartDto>> Get5CityForChart()
        {
            string query = "Select Top(5) City, Count(*) as 'CityCount' From Product Group By City order By CityCount Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultChartDto>(query);
                return values.ToList();
            }
        }
    }
}
