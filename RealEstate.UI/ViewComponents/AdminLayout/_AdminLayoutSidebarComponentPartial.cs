using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.AppUserDtos;
using RealEstate.UI.Models;
using RealEstate.UI.Services;

namespace RealEstate.UI.ViewComponents.AdminLayout
{
    public class _AdminLayoutSidebarComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly ApiSettings _apiSettings;
        public _AdminLayoutSidebarComponentPartial(IHttpClientFactory httpClientFactory, ILoginService loginService, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
            _apiSettings = apiSettings.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loginuserId = _loginService.GetUserId;

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            // Giri yapan Kullanicinin bilgilerini alma
            var responseMessage = await client.GetAsync($"AppUsers/{loginuserId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetByIDAppUserDto>(jsonData);
                ViewBag.LoginUserName = value.Name; // Giris yapan kullanicinin adini cekmek icin
                ViewBag.LoginProfileImage = value.UserImageUrl; // Giris yapan kullanicinin fotosunu cekmek icin
                return View();
            }
            return View();
        }
    }
}
