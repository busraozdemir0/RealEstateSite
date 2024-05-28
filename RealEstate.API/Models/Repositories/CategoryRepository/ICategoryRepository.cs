using RealEstate.API.DTOs.CategoryDtos;

namespace RealEstate.API.Models.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        void CreateCategory(CreateCategoryDto createCategoryDto);
        void DeleteCategory(int categoryId);
        void UpdateCategory(UpdateCategoryDto updateCategoryDto);
        Task<GetByIDCategoryDto> GetCategory(int categoryId);
    }
}
