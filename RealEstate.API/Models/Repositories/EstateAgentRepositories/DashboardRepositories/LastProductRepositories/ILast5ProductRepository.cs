using RealEstate.API.DTOs.ProductDtos;

namespace RealEstate.API.Models.Repositories.EstateAgentRepositories.DashboardRepositories.LastProductRepositories
{
    public interface ILast5ProductRepository
    {
        Task<List<ResultLast5ProductWithCategoryDto>> GetLast5ProductAsync(int id); // Giris yapan kullanicinin son ekledigi 5 ilan listelenecek

    }
}
