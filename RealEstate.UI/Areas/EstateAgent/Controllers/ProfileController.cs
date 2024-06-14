using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.AppUserDtos;
using RealEstate.UI.Models;
using RealEstate.UI.Services;
using System.Text;

namespace RealEstate.UI.Areas.EstateAgent.Controllers
{
    [Area("EstateAgent")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly ApiSettings _apiSettings;

        public ProfileController(IHttpClientFactory httpClientFactory, ILoginService loginService, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
            _apiSettings = apiSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var id = _loginService.GetUserId; // Giris yapmis olan kullanicinin id bilgisini cekiyoruz
            int loginAppUserId = int.Parse(id);

            ViewBag.loginUserId = int.Parse(id);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            // Kullanici bilgilerini alma
            var responseMessage = await client.GetAsync($"AppUsers/GetLoginUserProfile/{loginAppUserId}"); // Giris yapmis kullanicinin bilgilerini getirerek guncelleme islemi yapilacak
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateAppUserDto>(jsonData);

                ViewBag.roleId = value.UserRole; // Kullanicinin rol bilgisini guncellemeyecegimiz icin ViewBag ile tasiyalim

                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateAppUserDto updateAppUserDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateAppUserDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.PutAsync("AppUsers", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return Redirect("/EstateAgent/Dashboard/Index/");
            }

            return View();
        }
    }
}
