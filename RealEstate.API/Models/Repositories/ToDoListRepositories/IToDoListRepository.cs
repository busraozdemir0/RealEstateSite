using RealEstate.API.DTOs.ToDoListDtos;

namespace RealEstate.API.Models.Repositories.ToDoListRepositories
{
    public interface IToDoListRepository
    {
        Task<List<ResultToDoListDto>> GetAllToDoListAsync();
        void CreateToDoList(CreateToDoListDto createToDoListDto);
        void DeleteToDoList(int toDoListId);
        void UpdateToDoList(UpdateToDoListDto updateToDoListDto);
        Task<GetByIDToDoListDto> GetToDoList(int toDoListId);
    }
}
