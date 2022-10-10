using ElectricStore.Data;
using ElectricStore.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using ElectricStore.Hubs;

namespace ElectricStore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StoreContext>(options =>
                 options.UseSqlite("Data Source=electricStore.db"));

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(60);
            });

            services.AddSignalR();

            services.AddMvc(opt => opt.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, StoreContext storeContext, IHostingEnvironment environment)
        {
			
            storeContext.Database.EnsureDeleted();
            storeContext.Database.EnsureCreated();

            app.UseSession();

            app.UseStaticFiles();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            });

            // for new verions
            //app.UseEndpoints(route =>
            //{
            //    route.MapHub<ChatHub>("/chatHub");
            //});

            app.UseNodeModules(environment.ContentRootPath);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "ElectricStoreRoute",
                    template: "{controller}/{action}/{id?}/{RefreshCache?}",
                    defaults: new { controller = "Products", action = "Index" },
                    constraints: new { id = "[0-9]+" });
            });
        }
    }
}
