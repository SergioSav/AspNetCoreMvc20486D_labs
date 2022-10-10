using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Data;

namespace Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RestaurantContext>(options =>
                  options.UseSqlite("Data Source=restaurant.db"));


            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                     .AllowAnyMethod()
                                                                      .AllowAnyHeader()));

            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, RestaurantContext restaurantContext)
        {
            restaurantContext.Database.EnsureDeleted();
            restaurantContext.Database.EnsureCreated();

            app.UseCors("AllowAll");

            app.UseMvc();

        }
    }
}
