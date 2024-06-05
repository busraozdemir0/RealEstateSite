using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.ProductDtos;
using RealEstate.UI.Services;

namespace RealEstate.UI.Areas.EstateAgent.Controllers
{
    [Area("EstateAgent")]
    public class MyAdvertsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;

        public MyAdvertsController(IHttpClientFactory httpClientFactory, ILoginService loginService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }
        public async Task<IActionResult> ActiveAdverts()
        {
            var id= _loginService.GetUserId; // Giris yapan kullanicinin id bilgisi (Kullanicinin id'si ile kullanici kendi ekledigi ilanlarini gorebilecek)
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44314/api/Products/GetProductAdvertListByEmployeeAsyncByTrue?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); 
                var values = JsonConvert.DeserializeObject<List<ResultProductAdvertListWithCategoryByEmployeeDto>>(jsonData);   // DeserializeObject => json bir degeri okuyor ve bizim istedigimiz metin formatina donusturur
                                                                                                                                // SerializeObject => metin formatini json formatina donusturur.
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> PassiveAdverts()
        {
            var id = _loginService.GetUserId;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44314/api/Products/GetProductAdvertListByEmployeeAsyncByFalse?id=" + id);
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
