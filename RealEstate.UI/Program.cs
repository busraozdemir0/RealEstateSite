using Microsoft.AspNetCore.Authentication.JwtBearer;
using RealEstate.UI.Models;
using RealEstate.UI.Services;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// appsettings.json sinifinda yazdigimiz ApiSettingsKey'i burada kayit ediyoruz.
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettingsKey")); // Models sinifinda yer alan ApiSettings classina appsettings.json dosyasinda yer alan BaseUrl'i configure ediyoruz.

// Add services to the container.
builder.Services.AddHttpClient();

// JWT ile giris yapabilmek icin yapilandirmalar
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Login/Index/"; // Kullanici giris yapmadan baska sayfalara erismeye calisirsa bu sayfaya yonlendirilecek.
    opt.LogoutPath = "/Login/Logout/";
    opt.AccessDeniedPath = "/Pages/AccessDenied/";
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.Cookie.Name = "RealEstateJwt"; // Cookie'nin ismini belirledik
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Login islemi kullandigimizi uygulamamiza bildirmis olduk
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    // Slug Url icin yapilandirma
    endpoints.MapControllerRoute(
        name: "property",
        pattern: "property/{slug}/{id}",
        defaults: new { controller = "Property", action = "PropertySingle" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Default}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
