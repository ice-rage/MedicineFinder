using MedicineFinder.Server.Services.Interfaces;
using MedicineFinder.Server.Services;

namespace MedicineFinder.Server
{
    /// <summary>
    /// Класс для настройки и запуска серверной части веб-приложения.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Ключ к URL-адресу API Видаль (хранится в конфигурации сервера).
        /// </summary>
        private const string VidalApiKey = "VidalApi";

        /// <summary>
        /// Токен доступа к API Видаль (хранится в качестве секрета пользователя - User Secret).
        /// </summary>
        private const string AccessTokenName = "x-token";

        /// <summary>
        /// Точка входа в серверную часть веб-приложения.
        /// </summary>
        /// <param name="args"> Аргументы командной строки.</param>
        /// <returns> Асинхронная задача по запуску сервера.</returns>
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