using Dapper;
using RealEstate.API.DTOs.ProductImageDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.ProductImageRepositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly Context _context;

        public ProductImageRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<GetProductImageByProductIdDto>> GetProductImageByProductId(int id)
        {
            // Girilen product id degerine gore o ilana sahip tum gorseller gelecek
            string query = "Select * From ProductImage Where ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductImageByProductIdDto>(query, parameters);
                return values.ToList();
            }
        }
        public async Task<List<ResultProductImageDto>> GetAllProductImages()
        {
            // İlanlara yuklenmis tum gorseller listelenecek
            string query = "Select * From ProductImage inner join Product on ProductImage.ProductId=Product.ProductID";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductImageDto>(query);
                return values.ToList();
            }
        }
        public async Task CreateProductImage(CreateProductImageDto createProductImageDto)
        {
            string query = "insert into ProductImage (ImageUrl, ProductId) values (@imageUrl, @productId)";
            var parameters = new DynamicParameters();
            parameters.Add("@imageUrl", createProductImageDto.ImageUrl);
            parameters.Add("@productId", createProductImageDto.ProductId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteProductImage(int id)
        {
            string query = "Delete From ProductImage Where ProductImageId=@productImageId";
            var parameters = new DynamicParameters();
            parameters.Add("@productImageId", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<string> GetImageUrl(int id)
        {
            // Gelen id'ye gore ImageUrl dondurulecek
            string query = "Select ImageUrl From ProductImage Where ProductImageId=@productImageId";
            var parameters = new DynamicParameters();
            parameters.Add("@productImageId", id);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryAsync<string>(query, parameters);
                
                return value.FirstOrDefault();
            }
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImagesByAppUserId(int id)
        {
            // Giris yapmis olan emlakcinin kendi ilanlarina yuklenmis tum gorseller listelenecek
            string query = "Select * From ProductImage inner join Product on ProductImage.ProductId=Product.ProductID inner join AppUser on AppUser.UserId=Product.AppUserId Where Product.AppUserId=@userId";
            var parameters = new DynamicParameters();
            parameters.Add("@userId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductImageDto>(query, parameters);
                return values.ToList();
            }
        }
    }
}
