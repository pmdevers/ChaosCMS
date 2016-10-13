using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

using ChaosCMS.Managers;
using ChaosCMS.Stores;
using ChaosCMS.EntityFramework;
using ChaosCMS.Mvc.Models;

namespace ChaosCMS.Mvc
{
    public class Program
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddScoped<IPageStore<Page>, PageStore<Page>>();
            services.TryAddScoped<PageManager<Page>, PageManager<Page>>();
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            loggerFactory.AddConsole();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Program>()
                .Build();
            
            host.Run();
        }
    }
}
