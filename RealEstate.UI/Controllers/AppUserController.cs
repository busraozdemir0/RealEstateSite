using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.AppRoleDtos;
using RealEstate.UI.DTOs.AppUserDtos;
using RealEstate.UI.Models;
using RealEstate.UI.Services;
using System.Text;

namespace RealEstate.UI.Controllers
{
    [Authorize]
    public class AppUserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly ApiSettings _apiSettings;

        public AppUserController(IHttpClientFactory httpClientFactory, ILoginService loginService, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var user = User.Claims;
            var userId = _loginService.GetUserId; // Giris yapan kullanicinin id bilgisi

            // Bu sayfaya erismek icin realestatetoken tipinde token olup olmadigi kontrol ediliyor.
            var token = User.Claims.FirstOrDefault(x => x.Type == "realestatetoken")?.Value; // Giren kullanicinin erisim token'i realestatetoken tipinde claim'e sahipse token degerine atanacak boyle bir token yok ise null olacak
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_apiSettings.BaseUrl);
                var responseMessage = await client.GetAsync("AppUsers/AppUserList");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultAppUserDto>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateAppUser()
        {
            // Kullanici olusturma sayfasinda rolleri dropdown ile listelemek icin
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("AppRoles");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultAppRoleDto>>(jsonData);

            List<SelectListItem> roleValues = (from x in values.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.RoleName,
                                                       Value = x.RoleId.ToString()
                                                   }).ToList();
            ViewBag.roles = roleValues;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppUser(CreateAppUserDto createAppUserDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createAppUserDto); // Ekleme veya guncelleme islemi sirasinda duz metni json veriye donusturecegimiz icin SerializeObject metodu kullanilmaktadir.
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.PostAsync("AppUsers", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteAppUser(int appUserId)
        {
            var client = _httpClientFactory.CreateClient();
            // Silme islemi sirasinda kullanilan metod DeleteAsync'dir.
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.DeleteAsync($"AppUsers/{appUserId}"); // buraya /AppUserId ekledigimiz icin API tarafinda da HttpDelete kismina  [HttpDelete("{AppUserId}")] seklinde duzenlemeliyiz.
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAppUser(int appUserId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            // Kullanici bilgilerini alma
            var responseMessage = await client.GetAsync($"AppUsers/{appUserId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateAppUserDto>(jsonData);

                // Rolleri alma
                var client2 = _httpClientFactory.CreateClient();
                client2.BaseAddress = new Uri(_apiSettings.BaseUrl);
                var responseMessage2 = await client2.GetAsync("AppRoles");
                if (responseMessage2.IsSuccessStatusCode)
                {
                    var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                    var values2 = JsonConvert.DeserializeObject<List<ResultAppRoleDto>>(jsonData2);

                    List<SelectListItem> roleValues = (from x in values2.ToList()
                                                       select new SelectListItem
                                                       {
                                                           Text = x.RoleName,
                                                           Value = x.RoleId.ToString()
                                                       }).ToList();
                    ViewBag.roles = roleValues;
                }

                return View(values);
            }

            return View(); // Hata durumunda bos view dondur
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAppUser(UpdateAppUserDto updateAppUserDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateAppUserDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.PutAsync("AppUsers", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
