using MedicineFinder.Server.Services.Interfaces;
using MedicineFinder.Server.Services;

namespace MedicineFinder.Server
{
    public class Program
    {
        private const string VidalApiKey = "VidalApi";

        private const string AccessTokenName = "x-token";

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddHttpClient<IVidalService, VidalService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration[VidalApiKey]!);
                client.DefaultRequestHeaders.Add(AccessTokenName,
                    builder.Configuration[AccessTokenName]);
            });

            builder.Services.AddControllers();

            var app = builder.Build();

            Console.WriteLine($"x-token: {builder.Configuration["x-token"]}");

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