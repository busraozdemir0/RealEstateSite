﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate.UI.DTOs.CategoryDtos;
using RealEstate.UI.DTOs.ProductDetailDtos;
using RealEstate.UI.DTOs.ProductDtos;
using RealEstate.UI.Models;
using RealEstate.UI.Services;
using System.Text;
using X.PagedList;

namespace RealEstate.UI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;
        private readonly ILoginService _loginService;
        public ProductController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings, ILoginService loginService)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
            _loginService = loginService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values.ToPagedList(page, 10)); // Her sayfada en fazla 10 veri olsun
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            // İlan ekleme sayfasinda kategorileri dropdown ile listeleyebilmek icin (ilan eklerken kategori secimi islemi icin)
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Categories");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

            List<SelectListItem> categoryValues = (from x in values.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.categories = categoryValues;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            createProductDto.DealOfTheDay = false;
            createProductDto.AdvertisementDate = DateTime.Now;
            createProductDto.ProductStatus = true;

            var id = _loginService.GetUserId;
            createProductDto.AppUserId = int.Parse(id);

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.PostAsync("Products", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddProductDetail(int id)
        {
            ViewBag.productId = id;

            // İlana ait onceden detay tablosuna kayit eklenmis mi diye bakiliyor
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("ProductDetails/GetProductDetailByProductId?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<GetProductDetailByIdDto>(jsonData);

            // Eklenmisse yani deger null degilse veriler modele aktariliyor.
            if (values != null)
            {
                var model = new CreateProductDetailDto
                {
                    ProductDetailID = values.ProductDetailID,
                    ProductID = values.ProductID,
                    ProductSize = values.ProductSize,
                    BedRoomCount = values.BedRoomCount,
                    BathCount = values.BathCount,
                    RoomCount = values.RoomCount,
                    GarageSize = values.GarageSize,
                    BuildYear = values.BuildYear,
                    Price = values.Price,
                    Location = values.Location,
                    Videourl = values.Videourl
                };
                return View(model);
            }

            // Eger onceden detay tablosuna o ilanla ilgili veri eklenmemisse modelde sadece ProductID bilgisi doldurularak donduruluyor.
            var emptyModel = new CreateProductDetailDto
            {
                ProductID = id
            };
            return View(emptyModel);

        }

        [HttpPost]
        public async Task<IActionResult> AddProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var responseMessage = await client.GetAsync("ProductDetails/GetProductDetailByProductId?id=" + createProductDetailDto.ProductID);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var existingDetail = JsonConvert.DeserializeObject<GetProductDetailByIdDto>(jsonData);

            if (existingDetail != null) // Eger o ilana ait detay onceden eklenmisse guncelleme islemi yapilacak.
            {
                // Guncelleme islemi
                var updateJsonData = JsonConvert.SerializeObject(createProductDetailDto);
                StringContent updateStringContent = new StringContent(updateJsonData, Encoding.UTF8, "application/json");
                var updateResponseMessage = await client.PutAsync("ProductDetails", updateStringContent);

                if (updateResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            else // Eger o ilana ait detay onceden hic eklenmemisse ekleme islemi yapilacak.
            {
                // Ekleme islemi
                var addJsonData = JsonConvert.SerializeObject(createProductDetailDto);
                StringContent addStringContent = new StringContent(addJsonData, Encoding.UTF8, "application/json");
                var addResponseMessage = await client.PostAsync("ProductDetails", addStringContent);

                if (addResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(createProductDetailDto);
        }

        public async Task<IActionResult> ProductDealOfTheDayStatusChangeToTrue(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Products/ProductDealOfTheDayStatusChangeToTrue/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> ProductDealOfTheDayStatusChangeToFalse(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Products/ProductDealOfTheDayStatusChangeToFalse/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.DeleteAsync($"Products/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            // İlan bilgilerini alma
            var responseMessage = await client.GetAsync($"Products/GetProductById?productId={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);

                ViewBag.appUserId = values.AppUserId; // İlani ekleyen kullanicinin id bilgisi degismemesi icin boyle bir yol izliyoruz.

                // Kategorileri alma
                var client2 = _httpClientFactory.CreateClient();
                client2.BaseAddress = new Uri(_apiSettings.BaseUrl);
                var responseMessage2 = await client2.GetAsync("Categories");
                if (responseMessage2.IsSuccessStatusCode)
                {
                    var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                    var values2 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData2);

                    List<SelectListItem> categoryValues = (from x in values2.ToList()
                                                           select new SelectListItem
                                                           {
                                                               Text = x.CategoryName,
                                                               Value = x.CategoryID.ToString()
                                                           }).ToList();
                    ViewBag.categories = categoryValues;
                }

                return View(values);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            updateProductDto.DealOfTheDay = false;
            updateProductDto.AdvertisementDate = DateTime.Now;
            updateProductDto.ProductStatus = true;

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.PutAsync("Products", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> ProductStatusChangeToTrue(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Products/ProductStatusChangeToTrue/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> ProductStatusChangeToFalse(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Products/ProductStatusChangeToFalse/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
