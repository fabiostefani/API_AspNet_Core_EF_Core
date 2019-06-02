using fabiostefani.io.MapaCatalog.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;


namespace fabiostefani.io.MapaCatalog
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<StoreDataContext, StoreDataContext>(); // RESOLVER A INJEÇÃO DE DEPENDÊNCIA DOS CONTROLLERS
            //services.AddDbContext<StoreDataContext>(options =>
            //    options.UseNpgsql(@"Host = localhost; Port = 5432; Pooling = true; Database = MapaCatalog; User Id = postgres; Password = Postgres2018!"));
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();            

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
