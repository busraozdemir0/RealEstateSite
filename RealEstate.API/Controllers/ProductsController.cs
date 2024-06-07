
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DTOs.ProductDtos;
using RealEstate.API.Models.Repositories.ProductRepository;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values = await _productRepository.GetAllProductAsync();
            return Ok(values);
        }

        // Urunleri kategori adlariyla birlikte getirme (inner join sorgusu ile)
        [HttpGet("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            var values = await _productRepository.GetAllProductWithCategoryAsync();
            return Ok(values);
        }

        [HttpGet("ProductDealOfTheDayStatusChangeToTrue/{id}")]
        public async Task<IActionResult> ProductDealOfTheDayStatusChangeToTrue(int id)
        {
            await _productRepository.ProductDealOfTheDayStatusChangeToTrue(id);
            return Ok("İlan günün fırsatı olarak seçildi.");
        }

        [HttpGet("ProductDealOfTheDayStatusChangeToFalse/{id}")]
        public async Task<IActionResult> ProductDealOfTheDayStatusChangeToFalse(int id)
        {
            await _productRepository.ProductDealOfTheDayStatusChangeToFalse(id);
            return Ok("İlan günün fırsatından çıkarıldı.");
        }

        [HttpGet("GetLast5ProductList")]
        public async Task<IActionResult> GetLast5ProductList()
        {
            var values = await _productRepository.GetLast5ProductAsync();
            return Ok(values);
        }

        [HttpGet("GetProductAdvertListByEmployeeAsyncByTrue")]
        public async Task<IActionResult> GetProductAdvertListByEmployeeAsyncByTrue(int id)
        {
            var values = await _productRepository.GetProductAdvertListByEmployeeAsyncByTrue(id);
            return Ok(values);
        }

        [HttpGet("GetProductAdvertListByEmployeeAsyncByFalse")]
        public async Task<IActionResult> GetProductAdvertListByEmployeeAsyncByFalse(int id)
        {
            var values = await _productRepository.GetProductAdvertListByEmployeeAsyncByFalse(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            await _productRepository.CreateProduct(createProductDto);
            return Ok("İlan başarıyla eklendi");
        }

        [HttpGet("GetProductByProductId")]
        public async Task<IActionResult> GetProductByProductId(int id)
        {
            var values = await _productRepository.GetProductByProductId(id);
            return Ok(values);
        }

        [HttpGet("ResultProductWithSearchList")]
        public async Task<IActionResult> ResultProductWithSearchList(string searchKeyValue, int propertyCategoryId, string city)
        {
            var values = await _productRepository.ResultProductWithSearchList(searchKeyValue, propertyCategoryId, city);
            return Ok(values);
        }

        [HttpGet("GetProductByDealOfTheDayTrueWithCategoryAsync")]
        public async Task<IActionResult> GetProductByDealOfTheDayTrueWithCategoryAsync()
        {
            var values = await _productRepository.GetProductByDealOfTheDayTrueWithCategoryAsync();
            return Ok(values);
        }
    }
}
