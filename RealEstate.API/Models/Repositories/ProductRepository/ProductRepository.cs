
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
            string query = "Select * From Product where ProductStatus=1";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductDto>(query);
                return values.ToList();
            }
        }

        // Urunleri kategori adlariyla birlikte getirme (inner join sorgusu ile)
        public async Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync()
        {
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay, ProductStatus, SlugUrl From Product inner join Category on Product.ProductCategory=Category.CategoryID where ProductStatus=1"; // Product tablosunda yer alan ProductCategory alani ile Category tablosunda yer alan CategoryID alanini birbirine inner join yontemi ile entegre ettik.
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultLast5ProductWithCategoryDto>> GetLast5ProductAsync()
        {
            // Turu kiralik olan son 5 ilan listeleniyor
            string query = "Select Top(5) ProductID, Title, Price, City, District, ProductCategory, CategoryName, AdvertisementDate From Product Inner Join Category On Product.ProductCategory=Category.CategoryID Where Type='Kiralık' and ProductStatus=1 Order By ProductID Desc"; // Once ilanlari azalan bir sekilde sirala ve ardindan en ustten 5 ilani cek (Product ile Category tablosunu birlestirerek kategorinin adina ulastik
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast5ProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        // Kullaninin yayinladigi aktif ilan listesini veren metod
        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByTrue(int id)
        {
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay From Product inner join Category on Product.ProductCategory=Category.CategoryID where AppUserId=@userId And ProductStatus=1";
            var parameters = new DynamicParameters();
            parameters.Add("@userId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query, parameters);
                return values.ToList();
            }
        }

        // Kullaninin yayinladigi pasif ilan listesini veren metod
        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByFalse(int id)
        {
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay From Product inner join Category on Product.ProductCategory=Category.CategoryID where AppUserId=@userId And ProductStatus=0";
            var parameters = new DynamicParameters();
            parameters.Add("@userId", id);
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
            string query = "insert into Product (Title, Price, City, District, CoverImage, Address, Description, Type, DealOfTheDay, AdvertisementDate, ProductStatus, ProductCategory, AppUserId) values (@title, @price, @city, @district, @coverImage, @address, @description, @type, @dealOfTheDay, @advertisementDate, @productStatus, @productCategory, @userId)";
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
            parameters.Add("@userId", createProductDto.AppUserId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<GetProductByProductIdDto> GetProductByProductId(int id)
        {
            // Gelen id'ye gore ilani gosterme
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, Description, DealOfTheDay, AdvertisementDate, SlugUrl, AppUserId From Product inner join Category on Product.ProductCategory=Category.CategoryID where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryAsync<GetProductByProductIdDto>(query, parameters);
                return value.FirstOrDefault();
            }
        }

        

        public async Task<List<ResultProductWithSearchListDto>> ResultProductWithSearchList(string searchKeyValue, int propertyCategoryId, string city)
        {
            string query = "Select * From Product Where Title like '%" + searchKeyValue + "%' And ProductCategory=@propertyCategoryId And City=@city ";
            var parameters = new DynamicParameters();
            parameters.Add("@searchKeyValue", searchKeyValue);
            parameters.Add("@propertyCategoryId", propertyCategoryId);
            parameters.Add("@city", city);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithSearchListDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductByDealOfTheDayTrueWithCategoryAsync()
        {
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay From Product inner join Category on Product.ProductCategory=Category.CategoryID Where DealOfTheDay=1"; // Product tablosunda DealOfTheDay alani yani gunun firsati mi alanini true olanlari getirecek
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultOptimalPrice3ProductWithCategoryDto>> GetOptimalPrice3ProductAsync()
        {
            // Ana sayfada gunun 3 firsati kismi icin en uygun fiyatli 3 ilan listelenecek (Price alanina gore artan bir siralama yapildi ve en ustteki 3 ilan cekildi)
            string query = "Select Top(3) ProductID, Title, Price, City, District, Description, ProductCategory, CategoryName, CoverImage, AdvertisementDate From Product Inner Join Category On Product.ProductCategory=Category.CategoryID Where Type='Satılık' Order By Price Asc"; 
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultOptimalPrice3ProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task DeleteProduct(int productId)
        {
            string queryProductDetails = "Delete From ProductDetails Where ProductID=@productId"; // Oncelikle ProductDetails tablosunda yer alan productId bilgisine esit olan kayit silinecek.
            var parameters = new DynamicParameters();
            parameters.Add("@productId", productId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(queryProductDetails, parameters);
            }

            string queryProduct = "Delete From Product Where ProductID=@productId"; // Ardindan Product tablosunda productId'ye esti olan ilgili ilan silinecek. (ilk Product tablosundan silme yapildiginda conflict hatasi veriyor.)
            var parameters2 = new DynamicParameters();
            parameters2.Add("@productId", productId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(queryProduct, parameters2);
               
            }
        }

        public async Task UpdateProduct(UpdateProductDto updateProductDto)
        {
            string query = "Update Product set Title=@title, Price=@price, City=@city, District=@district, CoverImage=@coverImage, Address=@address, Description=@description, Type=@type, DealOfTheDay=@dealOfTheDay, AdvertisementDate=@advertisementDate, ProductStatus=@productStatus, ProductCategory=@productCategory, AppUserId=@userId where ProductID=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@title", updateProductDto.Title);
            parameters.Add("@price", updateProductDto.Price);
            parameters.Add("@city", updateProductDto.City);
            parameters.Add("@district", updateProductDto.District);
            parameters.Add("@coverImage", updateProductDto.CoverImage);
            parameters.Add("@address", updateProductDto.Address);
            parameters.Add("@description", updateProductDto.Description);
            parameters.Add("@type", updateProductDto.Type);
            parameters.Add("@dealOfTheDay", updateProductDto.DealOfTheDay);
            parameters.Add("@advertisementDate", updateProductDto.AdvertisementDate);
            parameters.Add("@productStatus", updateProductDto.ProductStatus);
            parameters.Add("@productCategory", updateProductDto.ProductCategory);
            parameters.Add("@userId", updateProductDto.AppUserId);
            parameters.Add("@productId", updateProductDto.ProductID);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<GetProductByIdDto> GetProductById(int productId)
        {
            // Gelen id'ye gore ilanin tum bilgilerini getirme (UserId dahil)
            string query = "Select ProductID, Title, Price, City, District, CoverImage, Address, Description, Type, DealOfTheDay, AdvertisementDate, ProductStatus, ProductCategory, AppUserId From Product inner join Category on Product.ProductCategory=Category.CategoryID where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", productId);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryAsync<GetProductByIdDto>(query, parameters);
                return value.FirstOrDefault();
            }
        }

        public async Task ProductStatusChangeToTrue(int id)
        {
            string query = "Update Product set ProductStatus=1 where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task ProductStatusChangeToFalse(int id)
        {
            string query = "Update Product set ProductStatus=0 where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetLast3ProductAdvertListByUserIdAsync(int id)
        {
            // Gelen userId bilgisine gore o kullanicinin yayinlamis oldugu son 3 aktif ilan
            string query = "Select Top(3) ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay, AdvertisementDate From Product inner join Category on Product.ProductCategory=Category.CategoryID where AppUserId=@userId And ProductStatus=1";
            var parameters = new DynamicParameters();
            parameters.Add("@userId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query, parameters);
                return values.ToList();
            }
        }
    }
}
