
using Dapper;
using RealEstate.API.DTOs.CategoryDtos;
using RealEstate.API.DTOs.ProductDetailDtos;
using RealEstate.API.DTOs.ProductDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            string query = "Select * From Product";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductDto>(query);
                return values.ToList();
            }
        }

        // Urunleri kategori adlariyla birlikte getirme (inner join sorgusu ile)
        public async Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync()
        {
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay From Product " +
                "inner join Category on Product.ProductCategory=Category.CategoryID"; // Product tablosunda yer alan ProductCategory alani ile Category tablosunda yer alan CategoryID alanini birbirine inner join yontemi ile entegre ettik.
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultLast5ProductWithCategoryDto>> GetLast5ProductAsync()
        {
            // Turu kiralik olan son 5 ilan listeleniyor
            string query = "Select Top(5) ProductID, Title, Price, City, District, ProductCategory, CategoryName, AdvertisementDate From Product Inner Join Category On Product.ProductCategory=Category.CategoryID Where Type='Kiralık' Order By ProductID Desc"; // Once ilanlari azalan bir sekilde sirala ve ardindan en ustten 5 ilani cek (Product ile Category tablosunu birlestirerek kategorinin adina ulastik
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast5ProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        // Kullaninin yayinladigi aktif ilan listesini veren metod
        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByTrue(int id)
        {
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay From Product inner join Category on Product.ProductCategory=Category.CategoryID where EmployeeID=@employeeId And ProductStatus=1"; 
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query,parameters);
                return values.ToList();
            }
        }

        // Kullaninin yayinladigi pasif ilan listesini veren metod
        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByFalse(int id)
        {
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay From Product inner join Category on Product.ProductCategory=Category.CategoryID where EmployeeID=@employeeId And ProductStatus=0";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task ProductDealOfTheDayStatusChangeToFalse(int id)
        {
            string query = "Update Product set DealOfTheDay=0 where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task ProductDealOfTheDayStatusChangeToTrue(int id)
        {
            string query = "Update Product set DealOfTheDay=1 where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task CreateProduct(CreateProductDto createProductDto)
        {
            string query = "insert into Product (Title, Price, City, District, CoverImage, Address, Description, Type, DealOfTheDay, AdvertisementDate, ProductStatus, ProductCategory, EmployeeID) values (@title, @price, @city, @district, @coverImage, @address, @description, @type, @dealOfTheDay, @advertisementDate, @productStatus, @productCategory, @employeeID)";
            var parameters = new DynamicParameters();
            parameters.Add("@title", createProductDto.Title);
            parameters.Add("@price", createProductDto.Price);
            parameters.Add("@city", createProductDto.City);
            parameters.Add("@district", createProductDto.District);
            parameters.Add("@coverImage", createProductDto.CoverImage);
            parameters.Add("@address", createProductDto.Address);
            parameters.Add("@description", createProductDto.Description);
            parameters.Add("@type", createProductDto.Type);
            parameters.Add("@dealOfTheDay", createProductDto.DealOfTheDay);
            parameters.Add("@advertisementDate", createProductDto.AdvertisementDate);
            parameters.Add("@productStatus", createProductDto.ProductStatus);
            parameters.Add("@productCategory", createProductDto.ProductCategory);
            parameters.Add("@employeeID", createProductDto.EmployeeID);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<GetProductByProductIdDto> GetProductByProductId(int id)
        {
            // Gelen id'ye gore ilani gosterme
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay From Product inner join Category on Product.ProductCategory=Category.CategoryID where ProductID=@productID"; 
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryAsync<GetProductByProductIdDto>(query,parameters);
                return value.FirstOrDefault();
            }
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
    }
}
