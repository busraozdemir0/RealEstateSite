using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.ProductDetailDtos;
using RealEstate.UI.DTOs.ProductDtos;

namespace RealEstate.UI.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PropertyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); // istemci ornegi olusturuldu.
            var responseMessage = await client.GetAsync("https://localhost:44314/api/Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Gelen icerigi string formatinda oku
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);   // DeserializeObject => json bir degeri okuyor ve bizim istedigimiz metin formatina donusturur
                                                                                                // SerializeObject => metin formatini json formatina donusturur.
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PropertySingle(int id)
        {
            id = 1;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44314/api/Products/GetProductByProductId?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<ResultProductDto>(jsonData);

            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = await client2.GetAsync("https://localhost:44314/api/ProductDetails/GetProductDetailByProductId?id=" + id);
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<GetProductDetailByIdDto>(jsonData2);

            ViewBag.title1 = values.title.ToString();
            ViewBag.price = values.price;
            ViewBag.city = values.city;
            ViewBag.district = values.district;
            ViewBag.address = values.address;
            ViewBag.type = values.type;
            ViewBag.description = values.description;
            ViewBag.date = values.advertisementDate;

            ViewBag.roomCount = values2.RoomCount;
            ViewBag.bedCount = values2.BedRoomCount;
            ViewBag.bathCount = values2.BathCount;
            ViewBag.size = values2.ProductSize; // metrekare cinsinden boyutunu ifade etmektedir
            ViewBag.garageCount = values2.GarageSize;
            ViewBag.buildYear = values2.BuildYear;
            ViewBag.location = values2.Location;
            ViewBag.videoUrl = values2.VideoUrl;
            
            
            DateTime date1 = DateTime.Now; // Simdiki zaman
            DateTime date2 = values.advertisementDate; // ilanin yayin tarihi
            TimeSpan timeSpan = date1 - date2; // Simdiki zaman ile ilan arasinda ne kadar gun var
            int month = timeSpan.Days / 30; // Gun sayisini 30'a bolunce ay bilgisine ulasilir.

            ViewBag.datediff = month;

            return View();

        }

    }
}
