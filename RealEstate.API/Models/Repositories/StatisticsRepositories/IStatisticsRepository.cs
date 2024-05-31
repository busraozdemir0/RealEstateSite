namespace RealEstate.API.Models.Repositories.StatisticsRepositories
{
    public interface IStatisticsRepository
    {
        int CategoryCount();
        int ActiveCategoryCount(); // Aktif kategori sayisi
        int PassiveCategoryCount(); // Pasif kategori sayisi
        int ProductCount();
        int ApartmentCount(); // Toplam daire sayisi
        string EmployeeNameByMaxProductCount(); // En fazla ilani olan personelin adini bize donduruyor olacak 
        string CategoryNameByMaxProductCount(); // En fazla ilani olan kategorinin adini bize donduruyor olacak 
        decimal AverageProductPriceByRent(); // Kiralik ilanlarin ortalama fiyatini getirecek
        decimal AverageProductPriceBySale(); // Satilik ilanlarin ortalama fiyatini getirecek
        string CityNameByMaxProductCount(); // İlanlar icerisinde en fazla hangi sehirden ilan var
        int DifferentCityCount(); // Kac farkli sehirden ilan var
        decimal LastProductPrice(); // Son eklenen ilanin fiyati
        string NewestBuildingYear(); // En yeni bina
        string OldestBuildingYear(); // En eski bina
        int AverageRoomCount(); // Ortalama oda sayisi
        int ActiveEmployeeCount(); // Aktif personel sayisi
    }
}
