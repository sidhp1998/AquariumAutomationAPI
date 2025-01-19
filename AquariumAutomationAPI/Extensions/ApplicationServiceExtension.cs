using AquariumAutomationAPI.Context;
using AquariumAutomationAPI.Repository;
using AquariumAutomationAPI.Services;

namespace AquariumAutomationAPI.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddSingleton<DataContext>();
            services.AddScoped<IDataRepository, DataRepository>();
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
