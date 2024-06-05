﻿
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult ProductDealOfTheDayStatusChangeToTrue(int id)
        {
            _productRepository.ProductDealOfTheDayStatusChangeToTrue(id);
            return Ok("İlan günün fırsatı olarak seçildi.");
        }

        [HttpGet("ProductDealOfTheDayStatusChangeToFalse/{id}")]
        public IActionResult ProductDealOfTheDayStatusChangeToFalse(int id)
        {
            _productRepository.ProductDealOfTheDayStatusChangeToFalse(id);
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

    }
}
