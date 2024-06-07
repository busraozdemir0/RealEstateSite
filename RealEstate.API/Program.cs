using RealEstate.API.Containers;
using RealEstate.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ContainerDependencies();

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
