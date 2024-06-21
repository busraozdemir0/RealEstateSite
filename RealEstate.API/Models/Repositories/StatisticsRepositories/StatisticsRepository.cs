using Dapper;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.StatisticsRepositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly Context _context;

        public StatisticsRepository(Context context)
        {
            _context = context;
        }
        public int ActiveCategoryCount()
        {
            string query = "Select Count(*) From Category where CategoryStatus = 1"; // Kategorinin statusu 1 olan yani true olan kac addet veri oldugunu cekecek olan sql sorgusu
            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<int>(query);
                return value;
            }
        }

        public int ActiveEmployeeCount()
        {
            string query = "Select Count(*) From AppUser";
            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<int>(query);
                return value;
            }
        }

        public int ApartmentCount()
        {
            // Title'in icinde Daire kelimesi gecen ilanlari listeleyip sayacak bir sorgu
            string query = "Select Count(*) From Product where Title like '%Daire%'";
            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<int>(query);
                return value;
            }
        }

        public decimal AverageProductPriceByRent()
        {
            string query = "Select Avg(Price) From Product where Type='Kiralık'"; // Kiralik ilanlarin ortalama fiyati
            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<decimal>(query);
                return value;
            }
        }

        public decimal AverageProductPriceBySale()
        {
            string query = "Select Avg(Price) From Product where Type='Satılık'"; // Satilik ilanlarin ortalama fiyati
            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<decimal>(query);
                return value;
            }
        }

        public int AverageRoomCount()
        {
            string query = "Select Avg(RoomCount) From ProductDetails";
            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<int>(query);
                return value;
            }
        }

        public int CategoryCount()
        {
            string query = "Select Count(*) From Category";
            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<int>(query);
                return value;
            }
        }

        // En fazla ilani olan kategorinin adini getirme islemi
        public string CategoryNameByMaxProductCount()
        {
            // Product ile Category tablosunu birlestirdik ve ilanin kategori ID'si ile Kategori tablosundaki kategori ID'si esit olanin kategori adina gore gruplama islemi yaptik.
            // Ardindan kategori adina gore gruplanan bu verileri kategori sayisina gore azalan bir sekilde siralayip top(1) ile en ustteki degeri almis olduk.
            string query = "Select top(1) CategoryName, Count(*) From Product inner join Category On Product.ProductCategory=Category.CategoryID Group By CategoryName order by Count(*) desc";   
            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<string>(query);
                return value;
            }
        }

        // İlanlar icerisinde en fazla hangi sehirde urun var
        public string CityNameByMaxProductCount()
        {
            // İlanlari sehre gore grupla ve sehir ile o sehirde kac ilan oldugunu azalan bir sekilde sirala. Top(1) ifadesi ile de en cok ilana sahip olan sehri cek
            string query = "Select Top(1) City, Count(*) as 'product_count' From Product Group By City order by product_count Desc";
                
            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<string>(query);
                return value;
            }
        }

        public int DifferentCityCount()
        {
            string query = "Select Count(Distinct(City)) From Product"; // Kac farkli sehir var ifadesini Distinct ile cekip ardindan bunlari saydiriyoruz

            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<int>(query);
                return value;
            }
        }

        // En fazla ilan veren personelin adini getir
        public string EmployeeNameByMaxProductCount()
        {
            string query = "Select Name, Count(*) 'product_count' From Product Inner Join AppUser On Product.AppUserId=AppUser.UserId Group By Name Order By product_count Desc";

            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<string>(query); // QueryFirstOrDefault metodu kullandigimiz icin sarti saglayan ilk degeri almaktadir
                return value;
            }
        }

        public decimal LastProductPrice()
        {
            string query = "Select Top(1) Price From Product order By ProductId Desc "; // İlanlari id'ye gore azalan bicimde siraladiktan sonra en ustte yer alanin price degerini cekiyoruz.

            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<decimal>(query); 
                return value;
            }
        }

        public string NewestBuildingYear()
        {
            // En yeni bina yilini veren sorgu
            string query = "Select Top(1) BuildYear From ProductDetails Order By BuildYear Desc";

            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<string>(query);
                return value;
            }
        }

        public string OldestBuildingYear()
        {
            // En eski bina yilini veren sorgu
            string query = "Select Top(1) BuildYear From ProductDetails Order By BuildYear Asc";

            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<string>(query);
                return value;
            }
        }

        public int PassiveCategoryCount()
        {
            // Toplam pasif kategori sayisini vermektedir.
            string query = "Select Count(*) From Category where CategoryStatus = 0";

            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<int>(query);
                return value;
            }
        }

        public int ProductCount()
        {
            // Toplam ilan sayisini vermektedir
            string query = "Select Count(*) From Product";

            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<int>(query);
                return value;
            }
        }
    }
}
