using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DTOs.CategoryDtos;
using RealEstate.API.Models.Repositories.CategoryRepository;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var values = await _categoryRepository.GetAllCategoryAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            _categoryRepository.CreateCategory(createCategoryDto);
            return Ok("Kategori başarılı bir şekilde eklendi.");
        }

        [HttpDelete("{categoryId}")] // Silme islemi
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            _categoryRepository.DeleteCategory(categoryId);
            return Ok("Kategori başarılı bir şekilde silindi.");
        }

        [HttpPut] // Guncelleme islemi
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            _categoryRepository.UpdateCategory(updateCategoryDto);
            return Ok("Kategori başarılı bir şekilde güncellendi.");
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var value = await _categoryRepository.GetCategory(categoryId);
            return Ok(value);
        }
    }
}
