using RealEstate.API.DTOs.ProductDtos;

namespace RealEstate.API.Models.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByTrue(int id); // Giris yapan kullanicinin yayinladigi ve aktif olan ilan listesi
        Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByFalse(int id); // Giris yapan kullanicinin yayinladigi ve pasif olan ilan listesi
        Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync(); // Urunleri kategori adlariyla birlikte getirecek olan metod
        Task ProductDealOfTheDayStatusChangeToTrue(int id); // Bu ilani gunun firsati yap
        Task ProductDealOfTheDayStatusChangeToFalse(int id); // Bu ilani gunun firsatindan cikar
        Task<List<ResultLast5ProductWithCategoryDto>> GetLast5ProductAsync(); // Son eklenen 5 ilan listelenecek
        Task<List<ResultOptimalPrice3ProductWithCategoryDto>> GetOptimalPrice3ProductAsync(); // Ana sayfada gunun 3 firsati kismi icin en uygun/ucuz fiyatli 3 ilan listelenecek
        Task CreateProduct(CreateProductDto createProductDto);
        Task<GetProductByProductIdDto> GetProductByProductId(int id); // id'ye gore ilani getirecek
        Task<List<ResultProductWithSearchListDto>> ResultProductWithSearchList(string searchKeyValue, int propertyCategoryId, string city); // Ana sayfada yer alan filtreleme islemi icin calisacak
        Task<List<ResultProductWithCategoryDto>> GetProductByDealOfTheDayTrueWithCategoryAsync(); // Urunleri kategori adlariyla birlikte ve gunun firsati olanlari getirecek olan metod
        Task DeleteProduct(int productId);
        Task<GetProductByIdDto> GetProductById(int id); // id'ye gore ilani getirecek (user Id bilgisi ile)
        Task UpdateProduct(UpdateProductDto updateProductDto);
        Task ProductStatusChangeToTrue(int id); // Bu ilani aktif yap
        Task ProductStatusChangeToFalse(int id); // Bu ilani pasif yap
        Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetLast3ProductAdvertListByUserIdAsync(int id); // Gelen userId bilgisine gore bu kullanicinin ekledigi son 3 ilan
    }
}
