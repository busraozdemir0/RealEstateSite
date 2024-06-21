using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.ProductDetailDtos;
using RealEstate.UI.DTOs.ProductDtos;
using RealEstate.UI.Models;
using X.PagedList;

namespace RealEstate.UI.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;
        public PropertyController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var client = _httpClientFactory.CreateClient(); // istemci ornegi olusturuldu.
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Gelen icerigi string formatinda oku
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);   // DeserializeObject => json bir degeri okuyor ve bizim istedigimiz metin formatina donusturur
                                                                                                // SerializeObject => metin formatini json formatina donusturur.
                ViewBag.productCount = values.Count();
                return View(values.ToPagedList(page, 10)); // Her sayfada en fazla 10 veri olsun
            }
            return View();
        }

        [HttpGet("property/{slug}/{id}")]
        public async Task<IActionResult> PropertySingle(string slug, int id)
        {
            ViewBag.productID = id; // id'den gelen degeri component'e gonderebilmek icin

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Products/GetProductByProductId?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<ResultProductDto>(jsonData);

            var client2 = _httpClientFactory.CreateClient();
            client2.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage2 = await client2.GetAsync("ProductDetails/GetProductDetailByProductId?id=" + id);
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<GetProductDetailByIdDto>(jsonData2);

            ViewBag.productID = values.productID;
            ViewBag.title1 = values.title.ToString();
            ViewBag.price = values.price;
            ViewBag.city = values.city;
            ViewBag.district = values.district;
            ViewBag.address = values.address;
            ViewBag.type = values.type;
            ViewBag.description = values.description;
            ViewBag.date = values.advertisementDate;
            ViewBag.slugUrl = values.SlugUrl;

            ViewBag.roomCount = values2.RoomCount;
            ViewBag.bedCount = values2.BedRoomCount;
            ViewBag.bathCount = values2.BathCount;
            ViewBag.size = values2.ProductSize; // metrekare cinsinden boyutunu ifade etmektedir
            ViewBag.garageCount = values2.GarageSize;
            ViewBag.buildYear = values2.BuildYear;
            ViewBag.location = values2.Location;
            ViewBag.videoUrl = values2.Videourl;
            
            
            DateTime date1 = DateTime.Now; // Simdiki zaman
            DateTime date2 = values.advertisementDate; // ilanin yayin tarihi
            TimeSpan timeSpan = date1 - date2; // Simdiki zaman ile ilan arasinda ne kadar gun var
            int days = timeSpan.Days; // Gun sayisini 30'a bolunce ay bilgisine ulasilir. Fakat biz yalnizca gun sayisini gosterelim.

            ViewBag.datediff = days;

            // Slug Url olusturm islemi icin
            string slugFromTitle = CreateSlug(values.title);
            ViewBag.slugUrl = slugFromTitle;

            return View();

        }

        // Slug nedir icin => https://medium.com/@sahinahmetdursun/slug-nedir-331dc581a5d3
        private string CreateSlug(string title)
        {
            title = title.ToLowerInvariant(); // Küçük harfe çevir
            title = title.Replace(" ", "-"); // Boşlukları tire ile değiştir
            title = System.Text.RegularExpressions.Regex.Replace(title, @"[^a-z0-9\s-]", ""); // Geçersiz karakterleri kaldır
            title = System.Text.RegularExpressions.Regex.Replace(title, @"\s+", " ").Trim(); // Birden fazla boşluğu tek boşluğa indir ve kenar boşluklarını kaldır
            title = System.Text.RegularExpressions.Regex.Replace(title, @"\s", "-"); // Boşlukları tire ile değiştir

            return title;
        }

        // Ana sayfadaki feature alaninda yer alan filtreleme islemi icin calisacak
        public async Task<IActionResult> PropertyListWithSearch(string searchKeyValue, int propertyCategoryId, string city)
        {
            // Default controller'daki PartialSearch'dan gelen TempData'lar ile verileri tasimis olduk
            searchKeyValue = TempData["searchKeyValue"].ToString();
            propertyCategoryId = int.Parse(TempData["propertyCategoryId"].ToString());
            city = TempData["city"].ToString();
            
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync($"Products/ResultProductWithSearchList?searchKeyValue={searchKeyValue}&propertyCategoryId={propertyCategoryId}&city={city}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); 
                var values = JsonConvert.DeserializeObject<List<ResultProductWithSearchListDto>>(jsonData);   
                                                                                                
                return View(values);
            }
            return View();
        }
    }
}
