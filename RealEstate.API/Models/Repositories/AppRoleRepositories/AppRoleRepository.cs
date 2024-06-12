using Dapper;
using RealEstate.API.DTOs.AppRoleDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.AppRoleRepositories
{
    public class AppRoleRepository : IAppRoleRepository
    {
        private readonly Context _context;

        public AppRoleRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<ResultAppRoleDto>> GetAllAppRole()
        {
            string query = "Select * From AppRole";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultAppRoleDto>(query);
                return values.ToList();
            }
        }
    }
}
