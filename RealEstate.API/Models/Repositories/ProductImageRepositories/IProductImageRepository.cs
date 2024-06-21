
using RealEstate.API.DTOs.ProductImageDtos;

namespace RealEstate.API.Models.Repositories.ProductImageRepositories
{
    public interface IProductImageRepository
    {
        Task<List<GetProductImageByProductIdDto>> GetProductImageByProductId(int id); // Girilen product id degerine gore o ilana sahip tum gorseller gelecek
        Task<List<ResultProductImageDto>> GetAllProductImages();
        Task<List<ResultProductImageDto>> GetAllProductImagesByAppUserId(int id); // İlani ekleyen emlakcinin id'sine gore yalnizca o ilanin gorselleri listelenecek
        Task CreateProductImage(CreateProductImageDto createProductImageDto);
        Task DeleteProductImage(int id);
        Task<string> GetImageUrl(int id);
    }
}
