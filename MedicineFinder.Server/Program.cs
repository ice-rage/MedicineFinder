using MedicineFinder.Server.Services.Interfaces;
using MedicineFinder.Server.Services;

namespace MedicineFinder.Server
{
    /// <summary>
    /// ����� ��� ��������� � ������� ��������� ����� ���-����������.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ���� � URL-������ API ������ (�������� � ������������ �������).
        /// </summary>
        private const string VidalApiKey = "VidalApi";

        /// <summary>
        /// ����� ������� � API ������ (�������� � �������� ������� ������������ - User Secret).
        /// </summary>
        private const string AccessTokenName = "x-token";

        /// <summary>
        /// ����� ����� � ��������� ����� ���-����������.
        /// </summary>
        /// <param name="args"> ��������� ��������� ������.</param>
        /// <returns> ����������� ������ �� ������� �������.</returns>
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