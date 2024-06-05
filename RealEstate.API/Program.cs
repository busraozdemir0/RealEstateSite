using RealEstate.API.Hubs;
using RealEstate.API.Models.DapperContext;
using RealEstate.API.Models.Repositories.BottomGridRepositories;
using RealEstate.API.Models.Repositories.CategoryRepository;
using RealEstate.API.Models.Repositories.ContactRepositories;
using RealEstate.API.Models.Repositories.EmployeeRepositories;
using RealEstate.API.Models.Repositories.EstateAgentRepositories.DashboardRepositories.ChartRepositories;
using RealEstate.API.Models.Repositories.EstateAgentRepositories.DashboardRepositories.LastProductRepositories;
using RealEstate.API.Models.Repositories.EstateAgentRepositories.DashboardRepositories.StatisticRepositories;
using RealEstate.API.Models.Repositories.MessageRepositories;
using RealEstate.API.Models.Repositories.PopularLocationRepositories;
using RealEstate.API.Models.Repositories.ProductRepository;
using RealEstate.API.Models.Repositories.ServiceRepository;
using RealEstate.API.Models.Repositories.StatisticsRepositories;
using RealEstate.API.Models.Repositories.TestimonialRepositories;
using RealEstate.API.Models.Repositories.ToDoListRepositories;
using RealEstate.API.Models.Repositories.WhoWeAreDetailRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<Context>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IWhoWeAreDetailRepository, WhoWeAreDetailRepository>();
builder.Services.AddTransient<IServiceRepository, ServiceRepository>();
builder.Services.AddTransient<IBottomGridRepository, BottomGridRepository>();
builder.Services.AddTransient<IPopularLocationRepository, PopularLocationRepository>();
builder.Services.AddTransient<ITestimonialRepository, TestimonialRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IStatisticsRepository, StatisticsRepository>();
builder.Services.AddTransient<IContactRepository, ContactRepository>();
builder.Services.AddTransient<IToDoListRepository, ToDoListRepository>();
builder.Services.AddTransient<IStatisticRepository, StatisticRepository>();
builder.Services.AddTransient<IChartRepository, ChartRepository>();
builder.Services.AddTransient<ILast5ProductRepository, Last5ProductRepository>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();

// SignalR anlik olarak verileri cekebilmemizi ve isleyebilmemizi saglar
// Baskalari bizim apimizi kullanabilsin diye konfigurasyon ayari yapiyoruz.
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader() // Disaridan gelen herhangi bir basliga izin ver
               .AllowAnyMethod() // Disaridan gelen herhangi bir metoda izin ver
               .SetIsOriginAllowed((host) => true)
               .AllowCredentials(); // Herhangi bir kimlik degerine izin ver
    });
});
builder.Services.AddHttpClient();
builder.Services.AddSignalR(); // Signal R dahil edildi

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy"); // Ýlgili policy cagirildi

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<SignalRHub>("/signalrhub");
// localhost:1234/swagger/category/index gibi uzun uzun yazmak yerine alttaki gibi
// localhost:1234/SignalRHub url'inde belirtildigi gibi SignalRHub sinifina istek atilarak gerceklestirilecektir. (SignalRHub => Hubs klasoru altinda bizim olusturdugumuz sinif)

app.Run();
