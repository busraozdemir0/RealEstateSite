using RealEstate.API.DTOs.ToDoListDtos;

namespace RealEstate.API.Models.Repositories.ToDoListRepositories
{
    public interface IToDoListRepository
    {
        Task<List<ResultToDoListDto>> GetAllToDoList();
        Task CreateToDoList(CreateToDoListDto createToDoListDto);
        Task DeleteToDoList(int toDoListId);
        Task UpdateToDoList(UpdateToDoListDto updateToDoListDto);
        Task<GetByIDToDoListDto> GetToDoList(int toDoListId);
    }
}
