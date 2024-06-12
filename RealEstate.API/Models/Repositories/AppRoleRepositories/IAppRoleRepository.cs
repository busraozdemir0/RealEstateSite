using RealEstate.API.DTOs.AppRoleDtos;

namespace RealEstate.API.Models.Repositories.AppRoleRepositories
{
    public interface IAppRoleRepository
    {
        Task<List<ResultAppRoleDto>> GetAllAppRole();
    }
}
