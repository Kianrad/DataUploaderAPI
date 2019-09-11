using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataUploadAPI.API.Configurations;
using DataUploadAPI.API.Extentions;
using DataUploadAPI.Data.Entities;
using DataUploadAPI.Business.ApiModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace DataUploadAPI.API
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
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.DescribeStringEnumsInCamelCase();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "HTTP API",
                    Version = "v1",
                    Description = "The Service HTTP API"
                });
            });
            services.Configure<MvcOptions>(options =>
            {
                options.MaxValidationDepth = 2;
            });
            
            var configuration = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Product, ProductApiModel>().ReverseMap();
                cfg.CreateMap<Category, CategoryApiModel>().ReverseMap();;
            });

            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
            
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddConnectionProvider(Configuration)
                .ConfigureRepositories()
                .ConfigureDataService()
                .ConfigureJsonWriter(Configuration)
                .AddMiddleware()
                .AddCorsConfiguration()
                .AddAppSettings(Configuration)
                .AddCaching()
                .AddCORS();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
          
            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            app.ConfigureExceptionHandler(loggerFactory);
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseMvc(
                routes => routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"));
            app.UseSwagger();
            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 docs"));
        }
    }
}