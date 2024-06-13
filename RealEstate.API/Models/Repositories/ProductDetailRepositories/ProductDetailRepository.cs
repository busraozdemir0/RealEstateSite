using Dapper;
using RealEstate.API.DTOs.ProductDetailDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.ProductDetailRepositories
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly Context _context;

        public ProductDetailRepository(Context context)
        {
            _context = context;
        }
        public async Task<GetProductDetailByIdDto> GetProductDetailByProductId(int id)
        {
            // Gelen id'ye gore ilan detaylarini getirme
            string query = "Select * From ProductDetails Where ProductID=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryAsync<GetProductDetailByIdDto>(query, parameters);
                return value.FirstOrDefault();
            }
        }
        public async Task CreateProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            string query = "insert into ProductDetails (ProductSize, BedroomCount, BathCount, RoomCount, GarageSize, BuildYear, Price, Location, Videourl, ProductID) values (@productSize, @bedroomCount, @bathCount, @roomCount, @garageSize, @buildYear, @price, @location, @videourl, @productID)";
            var parameters = new DynamicParameters();
            parameters.Add("@productSize", createProductDetailDto.ProductSize);
            parameters.Add("@bedroomCount", createProductDetailDto.BedRoomCount);
            parameters.Add("@bathCount", createProductDetailDto.BathCount);
            parameters.Add("@roomCount", createProductDetailDto.RoomCount);
            parameters.Add("@garageSize", createProductDetailDto.GarageSize);
            parameters.Add("@buildYear", createProductDetailDto.BuildYear);
            parameters.Add("@price", createProductDetailDto.Price);
            parameters.Add("@location", createProductDetailDto.Location);
            parameters.Add("@videourl", createProductDetailDto.Videourl);
            parameters.Add("@productID", createProductDetailDto.ProductID);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            string query = "Update ProductDetails Set ProductSize=@productSize, BedroomCount=@bedroomCount, BathCount=@bathCount, RoomCount=@roomCount, GarageSize=@garageSize, BuildYear=@buildYear, Price=@price, Location=@location, Videourl=@videourl, ProductID=@productID where ProductDetailID=@productDetailID";
            var parameters = new DynamicParameters();
            parameters.Add("@productSize", updateProductDetailDto.ProductSize);
            parameters.Add("@bedroomCount", updateProductDetailDto.BedRoomCount);
            parameters.Add("@bathCount", updateProductDetailDto.BathCount);
            parameters.Add("@roomCount", updateProductDetailDto.RoomCount);
            parameters.Add("@garageSize", updateProductDetailDto.GarageSize);
            parameters.Add("@buildYear", updateProductDetailDto.BuildYear);
            parameters.Add("@price", updateProductDetailDto.Price);
            parameters.Add("@location", updateProductDetailDto.Location);
            parameters.Add("@videourl", updateProductDetailDto.Videourl);
            parameters.Add("@productID", updateProductDetailDto.ProductID);
            parameters.Add("@productDetailID", updateProductDetailDto.ProductDetailID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
