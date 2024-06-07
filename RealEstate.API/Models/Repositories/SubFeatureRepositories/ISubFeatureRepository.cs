using RealEstate.API.DTOs.SubFeatureDtos;

namespace RealEstate.API.Models.Repositories.SubFeatureRepositories
{
    public interface ISubFeatureRepository
    {
        Task<List<ResultSubFeatureDto>> GetAllSubFeature();
    }
}
