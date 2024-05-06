using MedicineFinder.Server.Services.Interfaces;
using MedicineFinder.Server.Services;

namespace MedicineFinder.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddHttpClient<IVidalService, VidalService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["VidalApi"]);
                client.DefaultRequestHeaders.Add("x-token", "5HnGnVMkMx5e");
            });

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            await app.RunAsync();
        }
    }
}