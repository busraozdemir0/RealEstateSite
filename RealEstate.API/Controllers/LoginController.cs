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
            parameters.Add("@username",loginDto.UserName);
            parameters.Add("@password",loginDto.Password);
            using(var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<CreateLoginDto>(query,parameters);
                var values2 = await connection.QueryFirstAsync<GetAppUserIdDto>(query2,parameters);
                if (values != null) // Eger girilen username ve password degerine ait kayitli bir kullanici var ise o kullanici icin token olusturma islemleri gerceklesecek
                {
                    GetCheckAppUserViewModel model = new GetCheckAppUserViewModel();
                    model.UserName = values.UserName;
                    model.Id = values2.UserId; // Kullanicinin id bilgisi
                    var token = JwtTokenGenerator.GenerateToken(model);
                    return Ok(token);
                }
                else
                {
                    return Ok("Başarısız");
                }
            }
        }
    }
}
