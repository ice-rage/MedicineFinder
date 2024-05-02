using System.Net.Http.Headers;

namespace MedicineFinder.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddHttpClient<VidalClient>(client =>
            //{
            //    client.BaseAddress = new Uri(builder.Configuration["VidalApi"]);
            //    client.DefaultRequestHeaders.Add("x-token", "5HnGnVMkMx5e");
            //});

            builder.Services.AddControllers();

            var app = builder.Build();

            //var vidalClient = app.Services.GetRequiredService<VidalClient>();

            //var medicineInfo = await vidalClient.GetMedicineInfo("Аспирин");

            //if (medicineInfo.success)
            //{
            //    Console.WriteLine("Данные успешно получены");
            //}

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            await app.RunAsync();
        }
    }
}
