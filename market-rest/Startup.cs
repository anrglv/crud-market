using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CRUDMarket
{
    public class Startup
    {
        public IConfiguration Configuration { get; } 

        public Startup(IConfiguration configuration) // парсит appsettings.json 
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) // Добавление контроллеров для http запросов
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  
        {
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage(); // выводит ошибки 
            }

            app.UseRouting(); // маршрутизатор 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // определяет endpoint 
            });
        }
    }
}