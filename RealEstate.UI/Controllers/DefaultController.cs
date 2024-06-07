using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.CategoryDtos;

namespace RealEstate.UI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

		[HttpGet]
		public async Task<IActionResult> Index()
        {
			var client = _httpClientFactory.CreateClient(); // istemci ornegi olusturuldu.
			var responseMessage = await client.GetAsync("https://localhost:44314/api/Categories");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Gelen icerigi string formatinda oku
				var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);   // DeserializeObject => json bir degeri okuyor ve bizim istedigimiz metin formatina donusturur
																								 // SerializeObject => metin formatini json formatina donusturur.
				return View(values);
			}
			return View();
		}

        // Ana sayfada filtreleme islemi
        [HttpPost]
        public IActionResult PartialSearch(string searchKeyValue, int propertyCategoryId, string city)
        {
			// TempData'lar ile verileri Property Controller'da yer alan PropertyListWithSearch action'ununa gonderdik.
			TempData["searchKeyValue"] = searchKeyValue;
            TempData["propertyCategoryId"] = propertyCategoryId;
            TempData["city"] = city;
            return RedirectToAction("PropertyListWithSearch","Property");
        }
    }
}
