using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.ProductDtos;
using RealEstate.UI.Models;

namespace RealEstate.UI.ViewComponents.HomePage
{
    public class _Last3ActiveProductByUserIdComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;
        public _Last3ActiveProductByUserIdComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync($"Products/GetLast3ProductAdvertListByUserIdAsync/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductAdvertListWithCategoryByEmployeeDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
