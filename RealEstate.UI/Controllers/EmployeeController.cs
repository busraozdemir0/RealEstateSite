using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.EmployeeDtos;
using RealEstate.UI.Services;
using System.Text;

namespace RealEstate.UI.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;

        public EmployeeController(IHttpClientFactory httpClientFactory, ILoginService loginService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }

        public async Task<IActionResult> Index()
        {
            var user = User.Claims;
            var userId = _loginService.GetUserId; // Giris yapan kullanicinin id bilgisi

            // Bu sayfaya erismek icin realestatetoken tipinde token olup olmadigi kontrol ediliyor.
            var token = User.Claims.FirstOrDefault(x => x.Type == "realestatetoken")?.Value; // Giren kullanicinin erisim token'i realestatetoken tipinde claim'e sahipse token degerine atanacak boyle bir token yok ise null olacak
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:44314/api/Employees");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultEmployeeDto>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createEmployeeDto); // Ekleme veya guncelleme islemi sirasinda duz metni json veriye donusturecegimiz icin SerializeObject metodu kullanilmaktadir.
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44314/api/Employees", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var client = _httpClientFactory.CreateClient();
            // Silme islemi sirasinda kullanilan metod DeleteAsync'dir.
            var responseMessage = await client.DeleteAsync($"https://localhost:44314/api/Employees/{employeeId}"); // buraya /employeeId ekledigimiz icin API tarafinda da HttpDelete kismina  [HttpDelete("{employeeId}")] seklinde duzenlemeliyiz.
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(int employeeId)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44314/api/Employees/{employeeId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateEmployeeDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateEmployeeDto); // Ekleme veya guncelleme islemi sirasinda duz metni json veriye donusturecegimiz icin SerializeObject metodu kullanilmaktadir.
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44314/api/Employees", stringContent); // Guncelleme islemi icin kullanilan metod PutAsync'dir.
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
