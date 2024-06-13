using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RealEstate.UI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using RealEstate.UI.DTOs.AppUserDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace RealEstate.UI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;
        public RegisterController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateAppUserDto createAppUserDto)
        {
            createAppUserDto.UserRole = 2; // Kayit olan kullanici Employee rolunde kaydolmus olacak
            createAppUserDto.UserImageUrl = "~/user.png";

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createAppUserDto); // Ekleme veya guncelleme islemi sirasinda duz metni json veriye donusturecegimiz icin SerializeObject metodu kullanilmaktadir.
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.PostAsync("AppUsers", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Login");
            }
            return View();
        }
    }
}
