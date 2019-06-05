using fabiostefani.io.MapaCatalog.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using fabiostefani.io.MapaCatalog.Api.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using fabiostefani.io.MapaCatalog.Api;

namespace fabiostefani.io.MapaCatalog
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            //services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();
            
            //services.AddResponseCompression();

            services.AddScoped<StoreDataContext, StoreDataContext>(); // RESOLVER A INJEÇÃO DE DEPENDÊNCIA DOS CONTROLLERS. SCOPED: sempre retorna o mesmo se já existir
            services.AddTransient<ProductRepository, ProductRepository>(); // RESOLVER A INJEÇÃO DE DEPENDÊNCIA DOS CONTROLLERS: Transient: sempre retorna um novo
            services.AddTransient<CategoryRepository, CategoryRepository>();

            services.AddResponseCompression();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "fabiostefani", Version = "v1" });
                //x.SwaggerDoc("v2", new Info { Title = "fabiostefani", Version = "v2" });
                //c.SwaggerDoc("v1",
                //    new Info
                //    {
                //        Title = "Conversor de Temperaturas",
                //        Version = "v1",
                //        Description = "Exemplo de API REST criada com o ASP.NET Core",
                //        Contact = new Contact
                //        {
                //            Name = "Renato Groffe",
                //            Url = "https://github.com/renatogroffe"
                //        }
                //    });

                //string caminhoAplicacao =
                //    PlatformServices.Default.Application.ApplicationBasePath;
                //string nomeAplicacao =
                //    PlatformServices.Default.Application.ApplicationName;
                //string caminhoXmlDoc =
                //    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                //x.IncludeXmlComments(GetXmlCommentsPath());

                //https://docs.microsoft.com/pt-br/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio
                //https://medium.com/@renato.groffe/asp-net-core-swagger-documentando-apis-com-o-package-swashbuckle-aspnetcore-5eef480ba1c0
                //https://aspnetcore.readthedocs.io/en/stable/tutorials/web-api-help-pages-using-swagger.html
            });
            //services.ConfigureSwaggerGen(o=>
            //{
            //    o.
            //})

            //services.AddDbContext<StoreDataContext>(options =>
            //    options.UseNpgsql(@"Host = localhost; Port = 5432; Pooling = true; Database = MapaCatalog; User Id = postgres; Password = Postgres2018!"));

            Settings.ConnectionString = $"{Configuration["connectionString"]}";
        }

        //private static string GetXmlCommentsPath()
        //{
        //    var path =  System.AppDomain.CurrentDomain.BaseDirectory + @"\bin\fabiostefani.io.MapaCatalog.xml";
        //    return path;
        //}

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseResponseCompression();
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "fabiostefani - V1");                

            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
