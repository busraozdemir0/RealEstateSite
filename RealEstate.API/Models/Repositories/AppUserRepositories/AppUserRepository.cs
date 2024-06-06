using Dapper;
using RealEstate.API.DTOs.AppUserDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.AppUserRepositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly Context _context;

        public AppUserRepository(Context context)
        {
            _context = context;
        }
        public async Task<GetAppUserByProductIdDto> GetAppUserByProductId(int id)
        {
            string query = "Select * From AppUser Where UserId=@userId";
            var parameters = new DynamicParameters();
            parameters.Add("@userId", id);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<GetAppUserByProductIdDto>(query, parameters);
                return value;
            }
        }
    }
}
