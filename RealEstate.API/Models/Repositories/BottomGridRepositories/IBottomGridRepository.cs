using RealEstate.API.DTOs.BottomGridDtos;

namespace RealEstate.API.Models.Repositories.BottomGridRepositories
{
    public interface IBottomGridRepository
    {
        Task<List<ResultBottomGridDto>> GetAllBottomGridAsync();
        void CreateBottomGrid(CreateBottomGridDto createBottomGridDto);
        void DeleteBottomGrid(int bottomGridId);
        void UpdateBottomGrid(UpdateBottomGridDto updateBottomGridDto);
        Task<GetByIDBottomGridDto> GetBottomGrid(int bottomGridId);
    }
}
