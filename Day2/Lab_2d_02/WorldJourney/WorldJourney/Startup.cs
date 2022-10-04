using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WorldJourney.Models;

namespace WorldJourney
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IData, Data>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
