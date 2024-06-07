using RealEstate.API.DTOs.CategoryDtos;

namespace RealEstate.API.Models.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<List<ResultCategoryDto>> GetAllCategory();
        Task CreateCategory(CreateCategoryDto createCategoryDto);
        Task DeleteCategory(int categoryId);
        Task UpdateCategory(UpdateCategoryDto updateCategoryDto);
        Task<GetByIDCategoryDto> GetCategory(int categoryId);
    }
}
