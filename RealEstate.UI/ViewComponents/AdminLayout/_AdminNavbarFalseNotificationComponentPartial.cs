using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.ToDoListDtos;
using RealEstate.UI.Models;
using RealEstate.UI.Services;

namespace RealEstate.UI.ViewComponents.AdminLayout
{
    public class _AdminNavbarFalseNotificationComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;
        public _AdminNavbarFalseNotificationComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("ToDoLists/GetAllToDoListStatusFalse/"); // Durumu false olan yani yapilmamis gorevler listelenecek
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultToDoListDto>>(jsonData);
                ViewBag.ToDoListCount = values.Count();
                return View(values);
            }
            return View();
        }
    }
}
