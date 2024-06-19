using RealEstate.API.DTOs.ToDoListDtos;

namespace RealEstate.API.Models.Repositories.ToDoListRepositories
{
    public interface IToDoListRepository
    {
        Task<List<ResultToDoListDto>> GetAllToDoList();
        Task<List<ResultToDoListDto>> GetAllToDoListStatusFalse(); // Yapilmamis yani durumu false olan gorevler navbar'da bildirimler kisminda listelenecek.
        Task CreateToDoList(CreateToDoListDto createToDoListDto);
        Task DeleteToDoList(int toDoListId);
        Task UpdateToDoList(UpdateToDoListDto updateToDoListDto);
        Task<GetByIDToDoListDto> GetToDoList(int toDoListId);
        Task ToDoListStatusChangeToTrue(int id); // Bu gorevi aktif yap
        Task ToDoListStatusChangeToFalse(int id); // Bu gorevi pasif yap
    }
}
