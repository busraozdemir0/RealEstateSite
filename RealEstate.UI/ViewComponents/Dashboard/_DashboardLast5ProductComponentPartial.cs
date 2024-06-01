using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.ProductDtos;

namespace RealEstate.UI.ViewComponents.Dashboard
{
    public class _DashboardLast5ProductComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DashboardLast5ProductComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Son 5 kiralik ilan consume edilerek listeleniyor
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44314/api/Products/GetLast5ProductList");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }

    }
}
