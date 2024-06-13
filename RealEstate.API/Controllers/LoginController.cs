using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DTOs.LoginDtos;
using RealEstate.API.Models.DapperContext;
using RealEstate.API.Tools;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(CreateLoginDto loginDto)
        {
            string query = "Select * From AppUser Where UserName=@username and Password=@password";
            string query2 = "Select UserId From AppUser Where UserName=@username and Password=@password"; // Giris yapan kullanicinin id'si
            var parameters = new DynamicParameters();
            parameters.Add("@username", loginDto.UserName);
            parameters.Add("@password", loginDto.Password);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<CreateLoginDto>(query, parameters);
                var getAppUserId = await connection.QueryFirstAsync<GetAppUserIdDto>(query2, parameters);
                if (values != null) // Eger girilen username ve password degerine ait kayitli bir kullanici var ise o kullanici icin token olusturma islemleri gerceklesecek
                {
                    // Kullanıcının rol id'sini al
                    string roleId = "Select UserRole From AppUser Where UserId=@userId"; // Kullanicinin rol id'sini almak icin sorgu
                    parameters = new DynamicParameters();
                    parameters.Add("@userId", getAppUserId.UserId);

                    var roleIdInfo = await connection.QueryFirstOrDefaultAsync<string>(roleId, parameters);

                    // Kullanicinin rol adi bilgisini rol tablosundan cek
                    string roleName = "Select RoleName From AppRole Where RoleId=@roleId";
                    parameters = new DynamicParameters();
                    parameters.Add("@roleId", roleIdInfo);

                    var role = await connection.QueryFirstOrDefaultAsync<string>(roleName, parameters);

                    if (!string.IsNullOrEmpty(role))
                    {
                        GetCheckAppUserViewModel model = new GetCheckAppUserViewModel();
                        model.UserName = values.UserName;
                        model.Id = getAppUserId.UserId; // Kullanicinin id bilgisi
                        var token = JwtTokenGenerator.GenerateToken(model);

                        if (role == "Admin")
                        {
                            return Ok(new { token, redirectUrl = "/Dashboard/Index" });
                        }
                        else if (role == "Employee" || role == "Manager")
                        {
                            return Ok(new { token, redirectUrl = "/EstateAgent/Dashboard/Index" });
                        }
                        else
                        {
                            return Ok(new { token, redirectUrl = "/Default/Index" });
                        }
                    }
                    else
                    {
                        return Ok("Rol bulunamadı.");
                    }
                }
                else
                {
                    return Ok("Başarısız");
                }
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Başarıyla çıkış yapıldı." });
        }
    }
}
