using RealEstate.API.DTOs.TestimonialDtos;

namespace RealEstate.API.Models.Repositories.TestimonialRepositories
{
    public interface ITestimonialRepository
    {
        Task<List<ResultTestimonialDto>> GetAllTestimonial();

    }
}
