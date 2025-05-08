using Domain.Contracts;
using Domain.Models.Identity;
using ECommerce.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Identity;
using Services;
using Shared.ErrorModels;

namespace ECommerce.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInService();
            services.AddSwaggerService();

            services.AddInfrastructureServices(configuration);

            services.AddApplicationServices();

            services.ConfigureServives();

            services.AddIdentityServices();



            return services;
        }

        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers();
            
            return services;
        }

        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection ConfigureServives(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {


                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                 .Select(m => new ValidationError()
                                 {
                                     Field = m.Key,
                                     Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
                                 });

                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };


                    return new BadRequestObjectResult(response);
                };

            });

            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;

        }

        public static async Task<WebApplication> ConfigureMiddelware(this WebApplication app)
        {
            await app.InitializeDataBaseAsync();

            app.UseGlobalErrorHandling();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            return app;
        }

        private static async Task<WebApplication> InitializeDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();
            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
