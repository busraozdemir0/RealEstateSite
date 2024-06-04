namespace RealEstate.API.Models.Repositories.EstateAgentRepositories.DashboardRepositories.StatisticRepositories
{
    public interface IStatisticRepository
    {
        int ProductCountByEmployeeId(int id); // Sisteme giris yapan kullanicinin ilan sayisini verecek
        int ProductCountByStatusTrue(int id);  // Sisteme giris yapan kullanicinin aktif ilan sayisini verecek
        int ProductCountByStatusFalse(int id);  // Sisteme giris yapan kullanicinin pasif ilan sayisini verecek
        int AllProductCount(); // Tum ilan sayisi
    }
}
