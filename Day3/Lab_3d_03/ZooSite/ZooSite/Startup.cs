using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZooSite.Data;
using ZooSite.Middleware;

namespace ZooSite
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ZooContext>(options =>
                  options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc(opt=>opt.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ZooContext zooContext)
        {
            zooContext.Database.EnsureDeleted();
            zooContext.Database.EnsureCreated();

            app.UseStaticFiles();
            app.UseNodeModules(env.ContentRootPath);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "ZooRoute",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Zoo", action = "Index" },
                    constraints: new { id = "[0-9]+" });
            });
        }
    }
}
