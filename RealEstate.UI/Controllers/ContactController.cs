using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.AddressDtos;
using RealEstate.UI.DTOs.ContactDtos;
using RealEstate.UI.Models;
using System.Text;

namespace RealEstate.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public ContactController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Addresses/1");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIDAddressDto>(jsonData);

                ViewBag.locationMap = values.Location; // harita konum bilgisini view'a tasiyabilmek icin
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateContactDto createContactDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createContactDto); // Ekleme veya guncelleme islemi sirasinda duz metni json veriye donusturecegimiz icin SerializeObject metodu kullanilmaktadir.
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.PostAsync("Contacts", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AdminContactList()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Contacts/AdminContactList");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AdminContactGetById(int contactId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync($"Contacts/{contactId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIDContactDto>(jsonData); 
                return View(values);
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AdminContactDelete(int contactId)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.DeleteAsync($"Contacts/{contactId}"); 
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminContactList");
            }
            return View();
        }
    }
}
