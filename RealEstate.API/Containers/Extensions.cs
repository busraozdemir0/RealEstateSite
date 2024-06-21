
using RealEstate.API.Models.DapperContext;
using RealEstate.API.Models.Repositories.AddressRepositories;
using RealEstate.API.Models.Repositories.AppRoleRepositories;
using RealEstate.API.Models.Repositories.AppUserRepositories;
using RealEstate.API.Models.Repositories.BottomGridRepositories;
using RealEstate.API.Models.Repositories.CategoryRepository;
using RealEstate.API.Models.Repositories.ContactRepositories;
using RealEstate.API.Models.Repositories.EstateAgentRepositories.DashboardRepositories.ChartRepositories;
using RealEstate.API.Models.Repositories.EstateAgentRepositories.DashboardRepositories.LastProductRepositories;
using RealEstate.API.Models.Repositories.EstateAgentRepositories.DashboardRepositories.StatisticRepositories;
using RealEstate.API.Models.Repositories.MessageRepositories;
using RealEstate.API.Models.Repositories.PopularLocationRepositories;
using RealEstate.API.Models.Repositories.ProductDetailRepositories;
using RealEstate.API.Models.Repositories.ProductImageRepositories;
using RealEstate.API.Models.Repositories.ProductRepository;
using RealEstate.API.Models.Repositories.PropertyAmenityRepositories;
using RealEstate.API.Models.Repositories.ServiceRepository;
using RealEstate.API.Models.Repositories.StatisticsRepositories;
using RealEstate.API.Models.Repositories.SubFeatureRepositories;
using RealEstate.API.Models.Repositories.TestimonialRepositories;
using RealEstate.API.Models.Repositories.ToDoListRepositories;
using RealEstate.API.Models.Repositories.WhoWeAreDetailRepository;

namespace RealEstate.API.Containers
{
    public static class Extensions
    {
        public static void ContainerDependencies(this IServiceCollection services)
        {
            services.AddTransient<Context>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IWhoWeAreDetailRepository, WhoWeAreDetailRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IBottomGridRepository, BottomGridRepository>();
            services.AddTransient<IPopularLocationRepository, PopularLocationRepository>();
            services.AddTransient<ITestimonialRepository, TestimonialRepository>();
            services.AddTransient<IStatisticsRepository, StatisticsRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IToDoListRepository, ToDoListRepository>();
            services.AddTransient<IStatisticRepository, StatisticRepository>();
            services.AddTransient<IChartRepository, ChartRepository>();
            services.AddTransient<ILast5ProductRepository, Last5ProductRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IProductImageRepository, ProductImageRepository>();
            services.AddTransient<IAppUserRepository, AppUserRepository>();
            services.AddTransient<IPropertyAmenityRepository, PropertyAmenityRepository>();
            services.AddTransient<ISubFeatureRepository, SubFeatureRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IAppRoleRepository, AppRoleRepository>();
            services.AddTransient<IProductDetailRepository, ProductDetailRepository>();
        }
    }
}
