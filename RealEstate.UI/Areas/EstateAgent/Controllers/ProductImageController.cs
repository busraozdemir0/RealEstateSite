using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.ProductDtos;
using RealEstate.UI.DTOs.ProductImageDtos;
using RealEstate.UI.Models;
using RealEstate.UI.Services;
using System.Text;
using X.PagedList;

namespace RealEstate.UI.Areas.EstateAgent.Controllers
{
    [Area("EstateAgent")]
    [Authorize]
    public class ProductImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;
        private readonly ILoginService _loginService;
        private readonly IWebHostEnvironment _webHost;
        public ProductImageController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings, ILoginService loginService, IWebHostEnvironment webHost)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
            _loginService = loginService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var id = _loginService.GetUserId; // Giris yapan emlakcinin id bilgisi
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync($"ProductImages/GetAllProductImagesByAppUserId/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductImageDto>>(jsonData);
                return View(values.ToPagedList(page, 10)); // Her sayfada en fazla 10 veri olsun
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProductImage()
        {
            // İlan gorseli yukleme sayfasinda ilanlari dropdown ile listeleyebilmek icin (ilan gorseli eklerken ilan secimi yapabilmek icin)

            var id = _loginService.GetUserId; // Giris yapan kullanicinin id bilgisi (Kullanicinin id'si ile kullanici kendi ekledigi ilanlarini gorebilecek)
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Products/GetProductAdvertListByEmployeeAsyncByTrue?id=" + id);

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductAdvertListWithCategoryByEmployeeDto>>(jsonData);

            List<SelectListItem> productValues = (from x in values.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Title + " ~ " + x.City,
                                                      Value = x.ProductID.ToString()
                                                  }).ToList();
            ViewBag.products = productValues;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
        {
            if (createProductImageDto.Image != null)
            {
                string wwwRootPath = _webHost.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(createProductImageDto.Image.FileName);
                string extension = Path.GetExtension(createProductImageDto.Image.FileName);
                string imageUrl = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/ProductImageFiles/", imageUrl);

                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await createProductImageDto.Image.CopyToAsync(filestream);
                }

                createProductImageDto.ImageUrl = "/ProductImageFiles/" + imageUrl; // resim yolunu ImageUrl'e atama

                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_apiSettings.BaseUrl);
                var jsonData = JsonConvert.SerializeObject(createProductImageDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("ProductImages", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return Redirect("/EstateAgent/ProductImage/Index");
                }
            }

            return View();
        }

        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            // Once gorseli wwwroot'dan silme islemini gerceklestiriyoruz
            var imageUrlResponse = await client.GetAsync($"ProductImages/GetImageUrl/{id}"); // ProductImageId'si gonderilerek resim yolu gelmesi saglaniyor

            if (imageUrlResponse.IsSuccessStatusCode)
            {
                var imageUrl = await imageUrlResponse.Content.ReadAsStringAsync();
                string wwwRootPath = _webHost.WebRootPath;
                string path = Path.Combine(wwwRootPath + imageUrl); // wwwroot yolu ile resim yolu birlestiriliyor 

                if (System.IO.File.Exists(path)) // ve elde edilen path gercekten varsa ildili dizindeki gorsel kaldiriliyor
                {
                    System.IO.File.Delete(path);
                }
            }

            // Ardindan ProductImage tablosundan ilgili gorselin oldugu kaydi kaldiriyoruz
            var client2 = _httpClientFactory.CreateClient();
            client2.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client2.DeleteAsync($"ProductImages/{id}");

            if (responseMessage.IsSuccessStatusCode && imageUrlResponse.IsSuccessStatusCode)
            {
                return Redirect("/EstateAgent/ProductImage/Index");
            }

            return View();
        }
    }
}
