using RealEstate.API.DTOs.ProductDtos;

namespace RealEstate.API.Models.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByTrue(int id); // Giris yapan kullanicinin yayinladigi ve aktif olan ilan listesi
        Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByFalse(int id); // Giris yapan kullanicinin yayinladigi ve pasif olan ilan listesi
        Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync(); // Urunleri kategori adlariyla birlikte getirecek olan metod
        void ProductDealOfTheDayStatusChangeToTrue(int id); // Bu ilani gunun firsati yap
        void ProductDealOfTheDayStatusChangeToFalse(int id); // Bu ilani gunun firsatindan cikar
        Task<List<ResultLast5ProductWithCategoryDto>> GetLast5ProductAsync(); // Son eklenen 5 ilan listelenecek
    }
}
