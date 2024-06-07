using Dapper;
using RealEstate.API.DTOs.CategoryDtos;
using RealEstate.API.DTOs.TestimonialDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.TestimonialRepositories
{
    public class TestimonialRepository : ITestimonialRepository
    {
        private readonly Context _context;

        public TestimonialRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultTestimonialDto>> GetAllTestimonial()
        {
            string query = "Select * From Testimonial";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultTestimonialDto>(query);
                return values.ToList();
            }
        }
    }
}
