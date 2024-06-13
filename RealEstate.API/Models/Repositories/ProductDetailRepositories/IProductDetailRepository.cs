using RealEstate.API.DTOs.ProductDetailDtos;

namespace RealEstate.API.Models.Repositories.ProductDetailRepositories
{
    public interface IProductDetailRepository
    {
        Task CreateProductDetail(CreateProductDetailDto createProductDetailDto);
        Task UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto);
        Task<GetProductDetailByIdDto> GetProductDetailByProductId(int id); // id'ye gore ilanin detaylarini getirecek
    }
}
