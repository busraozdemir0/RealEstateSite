using Dapper;
using RealEstate.API.DTOs.CategoryDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;

        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultCategoryDto>> GetAllCategory()
        {
            // * Dapper ORM yardimi ile kategori listeleme islemi* 
            string query = "Select * From Category";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCategoryDto>(query); // Kategorileri DTO yadimiyla listeleme
                return values.ToList();
            }
        }

        public async Task CreateCategory(CreateCategoryDto createCategoryDto)
        {
            // Dapper ORM'si ile kategori ekleme islemi
            string query = "insert into Category (CategoryName, CategoryStatus) values (@categoryName, @categoryStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", createCategoryDto.CategoryName);
            parameters.Add("@categoryStatus", true); // CategoryStatus alanini eklendigi anda direkt true olarak belirledik.
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters); // Kategori ekleme islemi
            }
        }

        public async Task DeleteCategory(int categoryId)
        {
            string query = "Delete From Category Where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryID", categoryId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            string query = "Update Category Set CategoryName=@categoryName, CategoryStatus=@categoryStatus where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", updateCategoryDto.CategoryName);
            parameters.Add("@categoryStatus", updateCategoryDto.CategoryStatus);
            parameters.Add("@categoryID", updateCategoryDto.CategoryID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<GetByIDAddressDto> GetCategory(int categoryId)
        {
            string query = "Select * From Category Where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryID", categoryId);
            using (var connection = _context.CreateConnection())
            {
                var value=await connection.QueryFirstOrDefaultAsync<GetByIDAddressDto>(query, parameters);
                return value;
            }
        }
    }
}
