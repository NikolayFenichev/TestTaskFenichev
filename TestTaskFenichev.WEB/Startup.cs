using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using TestTask.BLL.Services;
using TestTask.BLL.Services.Interfaces;
using TestTask.DAL;
using TestTask.DAL.Repositories;

namespace TestTaskFenichev.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRestaurantManagementService, RestaurantManagementService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<RestaurantManagementContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection")));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "TestTask.WEB.xml");
                c.IncludeXmlComments(filePath);
                filePath = Path.Combine(System.AppContext.BaseDirectory, "TestTask.Common.xml");
                c.IncludeXmlComments(filePath);
                filePath = Path.Combine(System.AppContext.BaseDirectory, "TestTask.BLL.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
