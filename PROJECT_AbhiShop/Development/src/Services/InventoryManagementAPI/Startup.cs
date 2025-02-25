using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI
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
            services.Configure<InventorySettings>(Configuration);

            var server = Configuration["DatabaseServer"];
            var database = Configuration["DatabaseName"];
            var user = Configuration["DatabaseUser"];
            var password = Configuration["DatabaseUserPassword"];
            var connectionString = String.Format("Server={0};Database={1};User={2};Password={3};",server,database,user,password);

          //  services.AddDbContext<InventoryContext>(options=>options.UseSqlServer(Configuration["ConnectionString"]));
            services.AddDbContext<InventoryContext>(options=>options.UseSqlServer(connectionString));

            services.AddMvc();

            //swagger code generation code
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1",new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "AbhiShop - Inventory Catalog API",
                    Version = "v1",
                    Description = "Inventory Microservice",
                    TermsOfService = "Terms Of Service"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger()
             .UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint($"/swagger/v1/swagger.json","InventoryAPI V1");
             });
          //  app.UseMvc();
         app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
        });
        } 
    }
}

