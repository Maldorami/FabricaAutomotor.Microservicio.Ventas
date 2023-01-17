using FabricaAutomotor.Microservicio.Ventas.DataProvider;
using FabricaAutomotor.Microservicio.Ventas.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FabricaAutomotor.Microservicio.Ventas.API.Services;
using FabricaAutomotor.Microservicio.Ventas.Domain.Repositories;
using FabricaAutomotor.Microservicio.Ventas.Repository;
using Microsoft.OpenApi.Models;
using System;
using FabricaAutomotor.Microservicio.Ventas.Models.Request;
using System.IO;
using Microsoft.Extensions.Logging;

namespace FabricaAutomotor.Microservicio.Ventas
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
            services.AddControllers();

            services.AddTransient<ISaleService, SaleService>();
            services.AddTransient<IRepository, Repository.Repository>();
            services.AddTransient<IDataProvider.IDataProvider, MockedDataProvider>();

            AddSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            loggerFactory.AddLog4Net();
        }
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Foo {groupName}",
                    Version = groupName,
                    Description = "Foo API",
                    Contact = new OpenApiContact
                    {
                        Name = "Foo Company",
                        Email = string.Empty,
                        Url = new Uri("https://foo.com/"),
                    }
                });

                var XMLPath = AppDomain.CurrentDomain.BaseDirectory + nameof(Ventas) + ".xml";
                if (File.Exists(XMLPath))
                {
                    options.IncludeXmlComments(XMLPath);
                }
            });
        }
    }
}
