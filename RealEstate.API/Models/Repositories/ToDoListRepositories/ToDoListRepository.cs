using Dapper;
using RealEstate.API.DTOs.ServiceDtos;
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
        public async Task CreateToDoList(CreateToDoListDto createToDoListDto)
        {
            string query = "insert into ToDoList (Description, ToDoListStatus) values (@description, @toDoListStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@description", createToDoListDto.Description);
            parameters.Add("@toDoListStatus", false); 
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteToDoList(int toDoListId)
        {
            string query = "Delete From ToDoList Where ToDoListID=@toDoListID";
            var parameters = new DynamicParameters();
            parameters.Add("@toDoListID", toDoListId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultToDoListDto>> GetAllToDoList()
        {
            string query = "Select * From ToDoList";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultToDoListDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultToDoListDto>> GetAllToDoListStatusFalse()
        {
            string query = "Select * From ToDoList Where ToDoListStatus=0"; // Yapilmamis gorevler listelenecek
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultToDoListDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIDToDoListDto> GetToDoList(int toDoListId)
        {
            string query = "Select * From ToDoList Where ToDoListID=@toDoListID";
            var parameters = new DynamicParameters();
            parameters.Add("@toDoListID", toDoListId);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<GetByIDToDoListDto>(query, parameters);
                return value;
            }
        }

        public async Task ToDoListStatusChangeToFalse(int id)
        {
            string query = "Update ToDoList set ToDoListStatus=0 where ToDoListID=@toDoListID";
            var parameters = new DynamicParameters();
            parameters.Add("@toDoListID", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task ToDoListStatusChangeToTrue(int id)
        {
            string query = "Update ToDoList set ToDoListStatus=1 where ToDoListID=@toDoListID";
            var parameters = new DynamicParameters();
            parameters.Add("@toDoListID", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateToDoList(UpdateToDoListDto updateToDoListDto)
        {
            string query = "Update ToDoList Set Description=@description, ToDoListStatus=@toDoListStatus where ToDoListID=@toDoListID";
            var parameters = new DynamicParameters();
            parameters.Add("@description", updateToDoListDto.Description);
            parameters.Add("@toDoListStatus", updateToDoListDto.ToDoListStatus);
            parameters.Add("@toDoListID", updateToDoListDto.ToDoListID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
