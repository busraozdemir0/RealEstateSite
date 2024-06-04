using RealEstate.API.DTOs.ChartDtos;

namespace RealEstate.API.Models.Repositories.EstateAgentRepositories.DashboardRepositories.ChartRepositories
{
    public interface IChartRepository
    {
        Task<List<ResultChartDto>> Get5CityForChart(); // Chart icin 5 tane sehir bilgisi getir
    }
}
