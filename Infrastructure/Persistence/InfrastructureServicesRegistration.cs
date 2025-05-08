using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<ECommerceDbContext>(option => option.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<StoreIdentityDbContext>(options => options.UseSqlServer(config.GetConnectionString("IdentityConnection")));

            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                return ConnectionMultiplexer.Connect(config.GetConnectionString("Redis")!);
            });

            services.AddScoped<IBasketRepository,BasketRepository>();

            services.AddScoped<ICacheRepository,CacheRepository>();
            return services;
        }
    }
}
