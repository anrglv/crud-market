using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CRUDMarket
{
    public class Program
    {
        public static void Main(string[] args) // Входной метод без него и не работает
        {
            CreateHostBuilder(args).Build().Run(); // Вызов метода CreateHostBuilder
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => // Настройка хоста, 
            Host.CreateDefaultBuilder(args) // Создание веб-сервера
                .ConfigureWebHostDefaults(webBuilder => // Как веб хост будет настроен указано в классе Startup
                {
                    webBuilder.UseStartup<Startup>(); // Вызов класса Startup 
                });
    }
}