
using RealEstate.API.DTOs.ProductImageDtos;

namespace RealEstate.API.Models.Repositories.ProductImageRepositories
{
    public interface IProductImageRepository
    {
        Task<List<GetProductImageByProductIdDto>> GetProductImageByProductId(int id); // Girilen product id degerine gore o ilana sahip tum gorseller gelecek
    }
}
