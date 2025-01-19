using AquariumAutomationAPI.Extensions;

namespace AquariumAutomationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            //Add services to the container
            builder.Services.AddApplicationService(config);
            builder.Services.AddIdentityServices(config);

            //Build app
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            var allowedFrontendOrigins = builder.Configuration
                                                .GetSection("AllowedFrontendOrigins")
                                                .Get<string[]>() ?? Array.Empty<string>(); ;

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins(allowedFrontendOrigins));
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            //Run app
            app.Run();
        }
    }
}
