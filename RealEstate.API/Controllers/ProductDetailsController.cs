using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DTOs.ProductDetailDtos;
using RealEstate.API.Models.Repositories.ProductDetailRepositories;
using RealEstate.API.Models.Repositories.ProductRepository;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailRepository _productDetailRepository;

        public ProductDetailsController(IProductDetailRepository productDetailRepository)
        {
            _productDetailRepository = productDetailRepository;
        }

        [HttpGet("GetProductDetailByProductId")]
        public async Task<IActionResult> GetProductDetailByProductId(int id)
        {
            var values = await _productDetailRepository.GetProductDetailByProductId(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            await _productDetailRepository.CreateProductDetail(createProductDetailDto);
            return Ok("Ürün detayı başarılı bir şekilde eklendi.");
        }

        [HttpPut] 
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            await _productDetailRepository.UpdateProductDetail(updateProductDetailDto);
            return Ok("Ürün detayı başarılı bir şekilde güncellendi.");
        }
    }
}
