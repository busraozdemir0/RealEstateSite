using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.ContactDtos;

namespace RealEstate.UI.ViewComponents.Dashboard
{
    public class _DashboardLast4ContactListComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DashboardLast4ContactListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient(); 
            var responseMessage = await client.GetAsync("https://localhost:44314/api/Contacts/GetLast4Contact");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); 
                var values = JsonConvert.DeserializeObject<List<Last4ContactResultDto>>(jsonData);   
                return View(values);
            }
            return View();
        }
    }
}
