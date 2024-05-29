using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.CategoryDtos;
using System.Text;

namespace RealEstate.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // API'yi consume ederek CRUD islemleri yapilmaktadir.
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

        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto); // Ekleme veya guncelleme islemi sirasinda duz metni json veriye donusturecegimiz icin SerializeObject metodu kullanilmaktadir.
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44314/api/Categories",stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var client = _httpClientFactory.CreateClient();
            // Silme islemi sirasinda kullanilan metod DeleteAsync'dir.
            var responseMessage = await client.DeleteAsync($"https://localhost:44314/api/Categories/{categoryId}"); // buraya /categoryId ekledigimiz icin API tarafinda da HttpDelete kismina  [HttpDelete("{categoryId}")] seklinde duzenlemeliyiz.
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int categoryId)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage=await client.GetAsync($"https://localhost:44314/api/Categories/{categoryId}");
            if(responseMessage.IsSuccessStatusCode) {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData); // Guncelleme butonuna basilan kategorinin id'si ile o kategori bulunuyor ve bu json turunde oldugu icin bunu DeserializeObject ile duz metne ceviriyoruz.
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto); // Ekleme veya guncelleme islemi sirasinda duz metni json veriye donusturecegimiz icin SerializeObject metodu kullanilmaktadir.
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44314/api/Categories", stringContent); // Guncelleme islemi icin kullanilan metod PutAsync'dir.
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
