using System.Collections.Generic;
using System.Linq;
using DataUploadAPI.Data;
using DataUploadAPI.Data.Contracts;
using DataUploadAPI.Data.Repositories;
using DataUploadAPI.Business.Contracts;
using DataUploadAPI.Business.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DataUploadAPI.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IJsonReaderWriterService, JsonReaderWriterService>();
            services.AddScoped<IDataImportHandler, DbDataImporterService>();
            services.AddScoped<IDataImportHandler, JsonDataImporterService>();
            services.AddScoped<IMultiPartStreamReaderService, MultiPartFileStreamReaderService>();
            services.AddScoped<IDataImporterService>(sp =>
            {
                var scope = sp.CreateScope();
                return new DataImporterService(scope.ServiceProvider.GetServices<IDataImportHandler>().ToList());
            });

            return services;
        }

        public static IServiceCollection ConfigureDataService(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository>(sp =>
            {
                var scope = sp.CreateScope();
                return new ProductRepository(scope.ServiceProvider.GetService<DataUploadContext>());
                
            });
            services.AddScoped<ICategoryRepository>(sp =>
            {
                var scope = sp.CreateScope();
                return new CategoryRepository(scope.ServiceProvider.GetService<DataUploadContext>());
                
            });
            
            services.AddScoped<IDataUploadService, DataUploadService>();

            return services;
        }

        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
            services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials()
                        .Build());
                }
            );

        public static IServiceCollection AddLogging(this IServiceCollection services)
        {
            services.AddLogging(builder => builder
                .AddConsole()
                .AddFilter(level => level >= LogLevel.Information)
            );

            return services;
        }

        public static IServiceCollection AddCaching(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddResponseCaching();

            return services;
        }

        public static IServiceCollection AddCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://example.com")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            return services;
        }
    }
}