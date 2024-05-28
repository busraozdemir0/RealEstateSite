using RealEstate.API.DTOs.ProductDtos;

namespace RealEstate.API.Models.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync(); // Urunleri kategori adlariyla birlikte getirecek olan metod
    }
}
