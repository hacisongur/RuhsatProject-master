using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RuhsaProject.Entities.Concrete;
using RuhsatProject.Business.IServices;
using RuhsatProject.Business.Services;
using RuhsatProject.Core.Interfaces;
using RuhsatProject.DataAccess.Contexts;
using RuhsatProject.DataAccess.EntityFramework.Repositories;

namespace RuhsaProject.Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // ✅ Veritabanı sağlayıcısını belirtiyoruz (SQL Server)
            serviceCollection.AddDbContext<RuhsatDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RuhsatConnection")));

            // ✅ Identity yapılandırması
            serviceCollection.AddIdentity<User, Role>(options =>
            {
                // ✅ Şifre kuralları
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                // ✅ Kullanıcı ayarları
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
                options.User.RequireUniqueEmail = true;

                // ✅ Kilitleme ayarları   
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3; // ❌ 3 hatalı girişte
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2); // ⏳ 2 dakika kilitle
            }).AddEntityFrameworkStores<RuhsatDbContext>();

            // ✅ Servis ve repo kayıtları
            serviceCollection.AddScoped<IRuhsatRepository, RuhsatRepository>();
            serviceCollection.AddScoped<IRuhsatService, RuhsatManager>();

            return serviceCollection;
        }
    }
}
