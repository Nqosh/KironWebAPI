using KironWebAPI.Core.Interfaces;
using KironWebAPI.Infrastructure.Data;
using KironWebAPI.Infrastructure.Data.Repositories;
using KironWebAPI.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace KironWebAPI.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
              IConfiguration config)
        {
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IBankHolidayRepository, BankHolidayRepository>();
            services.AddScoped<INavigationRepository, NavigationRepository>();
            services.AddScoped<ICoinStatService, CoinStatService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IHolidayService, HolidayService>();
            services.AddScoped<INavigationService, NavigationService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddMemoryCache();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
               AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.
                       GetBytes(config["Token:Key"])),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            return services;
        }
    }
}
