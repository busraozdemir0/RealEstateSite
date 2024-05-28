﻿
using Dapper;
using RealEstate.API.DTOs.CategoryDtos;
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
            string query = "Select ProductID, Title, Price, City, District, CategoryName From Product " +
                "inner join Category on Product.ProductCategory=Category.CategoryID"; // Product tablosunda yer alan ProductCategory alani ile Category tablosunda yer alan CategoryID alanini birbirine inner join yontemi ile entegre ettik.
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }
    }
}
