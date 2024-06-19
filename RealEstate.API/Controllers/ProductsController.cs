
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

        // Ana sayfada gunun 3 firsati kismi icin en uygun fiyatli 3 ilan listelenecek (Price alanina gore artan bir siralama yapildi ve en ustteki 3 ilan cekildi)
        [HttpGet("GetOptimalPrice3Product")]
        public async Task<IActionResult> GetOptimalPrice3Product()
        {
            var values = await _productRepository.GetOptimalPrice3ProductAsync();
            return Ok(values);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _productRepository.DeleteProduct(productId);
            return Ok("İlan başarılı bir şekilde silindi.");
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var values = await _productRepository.GetProductById(productId);
            return Ok(values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productRepository.UpdateProduct(updateProductDto);
            return Ok("İlan başarılı bir şekilde güncellendi.");
        }

        [HttpGet("ProductStatusChangeToTrue/{id}")] // İlgili ilani aktif yapma
        public async Task<IActionResult> ProductStatusChangeToTrue(int id)
        {
            await _productRepository.ProductStatusChangeToTrue(id);
            return Ok("İlan aktif olarak işaretlendi.");
        }

        [HttpGet("ProductStatusChangeToFalse/{id}")] // İlgili ilani pasif yapma
        public async Task<IActionResult> ProductStatusChangeToFalse(int id)
        {
            await _productRepository.ProductStatusChangeToFalse(id);
            return Ok("İlan pasif olarak işaretlendi.");
        }

        [HttpGet("GetLast3ProductAdvertListByUserIdAsync/{id}")] // Gelen userId bilgisine gore bu kullanicinin ekledigi son 3 ilan
        public async Task<IActionResult> GetLast3ProductAdvertListByUserIdAsync(int id)
        {
            var values = await _productRepository.GetLast3ProductAdvertListByUserIdAsync(id);
            return Ok(values);
        }
    }
}
