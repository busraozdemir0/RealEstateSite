using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.ServiceDtos;
using RealEstate.UI.DTOs.WhoWeAreDetailDtos;
using RealEstate.UI.Models;

namespace RealEstate.UI.ViewComponents.HomePage
{
    public class _DefaultWhoWeAreComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;
        public _DefaultWhoWeAreComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient(); // Biz kimiz alani icin
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var client2 = _httpClientFactory.CreateClient(); // Biz kimiz yazisi altinda yer alan hizmetler icin bir client daha olusturuldu
            client2.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var responseMessage = await client.GetAsync("WhoWeAreDetails"); // Biz kimiz alani icin
            var responseMessage2 = await client2.GetAsync("Services"); //Biz kimiz yazisi altinda yer alan hizmetler icin

            if (responseMessage.IsSuccessStatusCode && responseMessage2.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Biz kimiz alani icin
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync(); // Services alani icin

                var value = JsonConvert.DeserializeObject<List<ResultWhoWeAreDetailDto>>(jsonData); // Biz kimiz alani icin
                var value2 = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData2);  // Services alani icin
               
                ViewBag.title = value.Select(x => x.Title).FirstOrDefault();
                ViewBag.subTitle = value.Select(x => x.SubTitle).FirstOrDefault();
                ViewBag.description1 = value.Select(x => x.Description1).FirstOrDefault();
                ViewBag.description2 = value.Select(x => x.Description2).FirstOrDefault();
                
                return View(value2); // Services alanini listelemek icin viewbag kullanmadigimizdan oturu direkt View icerisinde donebiliriz.
            }
            return View();
        }
    }
}
