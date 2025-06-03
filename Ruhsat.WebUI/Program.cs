using Rotativa.AspNetCore;
using RuhsaProject.Business.Extensions;
using RuhsaProject.Business.IServices;
using RuhsaProject.Business.Services;
using RuhsaProject.Core.Interfaces;
using RuhsaProject.DataAccess.EntityFramework.Repositories;
using RuhsatProject.Business.IServices;
using RuhsatProject.Business.Mapping;
using RuhsatProject.Business.Services;
using RuhsatProject.Core.Interfaces;
using RuhsatProject.DataAccess.EntityFramework.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ? Session güvenli þekilde ekleniyor
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".RuhsatProject.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

//// ? AutoMapper
builder.Services.AddAutoMapper(typeof(RuhsatProfile));

builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddScoped<IRuhsatTuruRepository, RuhsatTuruRepository>();
builder.Services.AddScoped<IRuhsatTuruService, RuhsatTuruManager>();

builder.Services.AddScoped<IFaaliyetKonusuRepository, FaaliyetKonusuRepository>();
builder.Services.AddScoped<IFaaliyetKonusuService, FaaliyetKonusuManager>();

builder.Services.AddScoped<IRuhsatSinifiRepository, RuhsatSinifiRepository>();
builder.Services.AddScoped<IRuhsatSinifiService, RuhsatSinifiManager>();
builder.Services.AddScoped<IRuhsatImzaRepository, RuhsatImzaRepository>();
builder.Services.AddScoped<IRuhsatImzaService, RuhsatImzaManager>();



// ? Service + Identity + DbContext (SQL Server baðlantýsýyla)
builder.Services.LoadMyServices(builder.Configuration);

// ? Identity Cookie ayarlarý
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/User/Login");
    options.LogoutPath = new PathString("/User/Logout");
    options.AccessDeniedPath = new PathString("/User/AccessDenied");

    options.Cookie = new CookieBuilder
    {
        Name = "RuhsatProject.Auth",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.Always,
        IsEssential = true
    };

    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});

// ? MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ? Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();         // ?? Session middleware aktif
app.UseAuthentication();  // Giriþ iþlemleri
app.UseAuthorization();   // Yetki kontrolü
RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa"); // ? BU SATIR
// ? Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();