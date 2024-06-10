using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RealEstate.UI.Models;

namespace RealEstate.UI.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;
        public StatisticsController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }
        public async Task<IActionResult> Index()
        {
            #region Statistics1
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Statistics/ActiveCategoryCount");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            ViewBag.activeCategoryCount = jsonData;
            #endregion

            #region Statistics2
            var client2 = _httpClientFactory.CreateClient();
            client2.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage2 = await client2.GetAsync("Statistics/ActiveEmployeeCount");
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            ViewBag.activeEmployeeCount = jsonData2;
            #endregion

            #region Statistics3
            var client3 = _httpClientFactory.CreateClient();
            client3.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage3 = await client3.GetAsync("Statistics/ApartmentCount");
            var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
            ViewBag.apartmentCount = jsonData3;
            #endregion

            #region Statistics4
            var client4 = _httpClientFactory.CreateClient();
            client4.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage4 = await client4.GetAsync("Statistics/AverageProductPriceByRent");
            var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
            ViewBag.averageProductPriceByRent = (decimal.Parse(jsonData4.Replace(".", "")) / 1000000m).ToString("N2"); // Virgulden sonra sadece 2 hane gosterilmesi icin
            #endregion

            #region Statistics5
            var client5 = _httpClientFactory.CreateClient();
            client5.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage5 = await client5.GetAsync("Statistics/AverageProductPriceBySale");
            var jsonData5 = await responseMessage5.Content.ReadAsStringAsync();
            ViewBag.averageProductPriceBySale = (decimal.Parse(jsonData5.Replace(".", "")) / 1000000m).ToString("N2"); // Virgulden sonra sadece 2 hane gosterilmesi icin
            #endregion

            #region Statistics6
            var client6 = _httpClientFactory.CreateClient();
            client6.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage6 = await client6.GetAsync("Statistics/AverageRoomCount");
            var jsonData6 = await responseMessage6.Content.ReadAsStringAsync();
            ViewBag.averageRoomCount = jsonData6;
            #endregion

            #region Statistics7
            var client7 = _httpClientFactory.CreateClient();
            client7.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage7 = await client7.GetAsync("Statistics/CategoryCount");
            var jsonData7 = await responseMessage7.Content.ReadAsStringAsync();
            ViewBag.categoryCount = jsonData7;
            #endregion

            #region Statistics8
            var client8 = _httpClientFactory.CreateClient();
            client8.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage8 = await client8.GetAsync("Statistics/CategoryNameByMaxProductCount");
            var jsonData8 = await responseMessage8.Content.ReadAsStringAsync();
            ViewBag.categoryNameByMaxProductCount = jsonData8;
            #endregion

            #region Statistics9
            var client9 = _httpClientFactory.CreateClient();
            client9.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage9 = await client9.GetAsync("Statistics/CityNameByMaxProductCount");
            var jsonData9 = await responseMessage9.Content.ReadAsStringAsync();
            ViewBag.cityNameByMaxProductCount = jsonData9;
            #endregion

            #region Statistics10
            var client10 = _httpClientFactory.CreateClient();
            client10.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage10 = await client10.GetAsync("Statistics/DifferentCityCount");
            var jsonData10 = await responseMessage10.Content.ReadAsStringAsync();
            ViewBag.differentCityCount = jsonData10;
            #endregion

            #region Statistics11
            var client11 = _httpClientFactory.CreateClient();
            client11.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage11 = await client11.GetAsync("Statistics/EmployeeNameByMaxProductCount");
            var jsonData11 = await responseMessage11.Content.ReadAsStringAsync();
            ViewBag.employeeNameByMaxProductCount = jsonData11;
            #endregion

            #region Statistics12
            var client12 = _httpClientFactory.CreateClient();
            client12.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage12 = await client12.GetAsync("Statistics/LastProductPrice");
            var jsonData12 = await responseMessage12.Content.ReadAsStringAsync();
            ViewBag.lastProductPrice = (decimal.Parse(jsonData12.Replace(".", "")) / 10000m).ToString("N2"); // Virgulden sonra sadece 2 hane gosterilmesi icin
            #endregion

            #region Statistics13
            var client13 = _httpClientFactory.CreateClient();
            client13.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage13 = await client13.GetAsync("Statistics/NewestBuildingYear");
            var jsonData13 = await responseMessage13.Content.ReadAsStringAsync();
            ViewBag.newestBuildingYear = jsonData13;
            #endregion

            #region Statistics14
            var client14 = _httpClientFactory.CreateClient();
            client14.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage14 = await client14.GetAsync("Statistics/OldestBuildingYear");
            var jsonData14 = await responseMessage14.Content.ReadAsStringAsync();
            ViewBag.oldestBuildingYear = jsonData14;
            #endregion

            #region Statistics15
            var client15 = _httpClientFactory.CreateClient();
            client15.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage15 = await client15.GetAsync("Statistics/PassiveCategoryCount");
            var jsonData15 = await responseMessage15.Content.ReadAsStringAsync();
            ViewBag.passiveCategoryCount = jsonData15;
            #endregion

            #region Statistics16
            var client16 = _httpClientFactory.CreateClient();
            client16.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage16 = await client16.GetAsync("Statistics/ProductCount");
            var jsonData16 = await responseMessage16.Content.ReadAsStringAsync();
            ViewBag.productCount = jsonData16;
            #endregion

            return View();
        }
    }
}
