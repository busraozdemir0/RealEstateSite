using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.BottomGridDtos;

namespace RealEstate.UI.ViewComponents.HomePage
{
    public class _DefaultBottomGridComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultBottomGridComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient(); // istemci ornegi olusturuldu.
            var responseMessage = await client.GetAsync("https://localhost:44314/api/BottomGrids");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Gelen icerigi string formatinda oku
                var values = JsonConvert.DeserializeObject<List<ResultBottomGridDto>>(jsonData);   // DeserializeObject => json bir degeri okuyor ve bizim istedigimiz metin formatina donusturur
                                                                                                 // SerializeObject => metin formatini json formatina donusturur.
                return View(values);
            }
            return View();
        }
    }
}
