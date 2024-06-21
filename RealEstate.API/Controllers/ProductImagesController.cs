using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DTOs.ProductImageDtos;
using RealEstate.API.Models.Repositories.ProductImageRepositories;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageRepository _productImageRepository;

        public ProductImagesController(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductImageById(int id)
        {
            var values = await _productImageRepository.GetProductImageByProductId(id);
            return Ok(values);
        }

        [HttpGet("GetAllProductImages")]
        public async Task<IActionResult> GetAllProductImages()
        {
            var values = await _productImageRepository.GetAllProductImages();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage([FromBody] CreateProductImageDto createProductImageDto)
        {
            if (createProductImageDto == null || string.IsNullOrEmpty(createProductImageDto.ImageUrl) || createProductImageDto.ProductId <= 0)
            {
                return BadRequest("Geçersiz veri.");
            }
            await _productImageRepository.CreateProductImage(createProductImageDto);
            return Ok("İlan resmi başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            await _productImageRepository.DeleteProductImage(id);
            return Ok("İlan görseli başarılı bir şekilde silindi.");
        }

        [HttpGet("GetImageUrl/{id}")]
        public async Task<IActionResult> GetImageUrl(int id)
        {
            var values = await _productImageRepository.GetImageUrl(id);
            return Ok(values);
        }

        [HttpGet("GetAllProductImagesByAppUserId/{id}")]
        public async Task<IActionResult> GetAllProductImagesByAppUserId(int id)
        {
            var values = await _productImageRepository.GetAllProductImagesByAppUserId(id);
            return Ok(values);
        }
    }
}
