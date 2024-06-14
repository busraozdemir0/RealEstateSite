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
        public async Task CreateAppUser(CreateAppUserDto createAppUserDto)
        {
            string query = "insert into AppUser (Name, UserName, Password, UserRole, Email, PhoneNumber, UserImageUrl) values (@name, @userName, @password, @userRole, @email, @phoneNumber, @userImageUrl)";
            var parameters = new DynamicParameters();
            parameters.Add("@name", createAppUserDto.Name);
            parameters.Add("@userName", createAppUserDto.UserName);
            parameters.Add("@password", createAppUserDto.Password);
            parameters.Add("@userRole", createAppUserDto.UserRole);
            parameters.Add("@email", createAppUserDto.Email);
            parameters.Add("@phoneNumber", createAppUserDto.PhoneNumber);
            parameters.Add("@userImageUrl", createAppUserDto.UserImageUrl);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAppUser(int appUserId)
        {
            string query = "Delete From AppUser Where UserId=@userId";
            var parameters = new DynamicParameters();
            parameters.Add("@userId", appUserId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultAppUserDto>> GetAllAppUser()
        {
            string query = "Select * From AppUser inner join AppRole on AppUser.UserRole=AppRole.RoleId";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultAppUserDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIDAppUserDto> GetAppUser(int appUserId)
        {
            string query = "Select * From AppUser inner join AppRole on AppUser.UserRole=AppRole.RoleId Where UserId=@userId";
            var parameters = new DynamicParameters();
            parameters.Add("@userId", appUserId);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<GetByIDAppUserDto>(query, parameters);
                return value;
            }
        }

        public async Task<ProfileUpdateDto> GetLoginUserProfile(int appUserId)
        {
            string query = "Select * From AppUser Where UserId=@userId";
            var parameters = new DynamicParameters();
            parameters.Add("@userId", appUserId);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<ProfileUpdateDto>(query, parameters);
                return value;
            }
        }

        public async Task UpdateAppUser(UpdateAppUserDto updateAppUserDto)
        {
            string query = "Update AppUser set Name=@name, UserName=@userName, Password=@password, UserRole=@userRole, Email=@email, PhoneNumber=@phoneNumber, UserImageUrl=@userImageUrl where UserId=@userId";
            var parameters = new DynamicParameters();
            parameters.Add("@name", updateAppUserDto.Name);
            parameters.Add("@userName", updateAppUserDto.UserName);
            parameters.Add("@password", updateAppUserDto.Password);
            parameters.Add("@userRole", updateAppUserDto.UserRole);
            parameters.Add("@email", updateAppUserDto.Email);
            parameters.Add("@phoneNumber", updateAppUserDto.PhoneNumber);
            parameters.Add("@userImageUrl", updateAppUserDto.UserImageUrl);
            parameters.Add("@userId", updateAppUserDto.UserId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
