using Dapper;
using RealEstate.API.DTOs.ToDoListDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.ToDoListRepositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly Context _context;

        public ToDoListRepository(Context context)
        {
            _context = context;
        }
        public void CreateToDoList(CreateToDoListDto createToDoListDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteToDoList(int toDoListId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultToDoListDto>> GetAllToDoListAsync()
        {
            string query = "Select * From ToDoList";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultToDoListDto>(query);
                return values.ToList();
            }
        }

        public Task<GetByIDToDoListDto> GetToDoList(int toDoListId)
        {
            throw new NotImplementedException();
        }

        public void UpdateToDoList(UpdateToDoListDto updateToDoListDto)
        {
            throw new NotImplementedException();
        }
    }
}
