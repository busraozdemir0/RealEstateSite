using RealEstate.API.DTOs.CategoryDtos;
using RealEstate.API.DTOs.PopularLocationDtos;

namespace RealEstate.API.Models.Repositories.PopularLocationRepositories
{
    public interface IPopularLocationRepository
    {
        Task<List<ResultPopularLocationDto>> GetAllPopularLocationAsync();
    }
}
