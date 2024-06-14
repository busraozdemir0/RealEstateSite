using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DTOs.AppUserDtos;
using RealEstate.API.Models.Repositories.AppUserRepositories;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IAppUserRepository _appUserRepository;

        public AppUsersController(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppUserByProductId(int id)
        {
            var value = await _appUserRepository.GetAppUserByProductId(id);
            return Ok(value);
        }

        [HttpGet("AppUserList")]
        public async Task<IActionResult> AppUserList()
        {
            var values = await _appUserRepository.GetAllAppUser();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppUser(CreateAppUserDto createAppUserDto)
        {
            await _appUserRepository.CreateAppUser(createAppUserDto);
            return Ok("Kullanıcı başarılı bir şekilde eklendi.");
        }

        [HttpDelete("{appUserId}")] // Silme islemi
        public async Task<IActionResult> DeleteAppUser(int appUserId)
        {
            await _appUserRepository.DeleteAppUser(appUserId);
            return Ok("Kullanıcı başarılı bir şekilde silindi.");
        }

        [HttpPut] // Guncelleme islemi
        public async Task<IActionResult> UpdateAppUser(UpdateAppUserDto updateAppUserDto)
        {
            await _appUserRepository.UpdateAppUser(updateAppUserDto);
            return Ok("Kullanıcı başarılı bir şekilde güncellendi.");
        }

        [HttpGet("{appUserId}")]
        public async Task<IActionResult> GetAppUser(int appUserId)
        {
            var value = await _appUserRepository.GetAppUser(appUserId);
            return Ok(value);
        }

        [HttpGet("GetLoginUserProfile/{appUserId}")]
        public async Task<IActionResult> GetLoginUserProfile(int appUserId)
        {
            var value = await _appUserRepository.GetLoginUserProfile(appUserId);
            return Ok(value);
        }
    }
}
