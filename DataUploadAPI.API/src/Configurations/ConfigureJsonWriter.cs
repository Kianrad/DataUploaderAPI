using System;
using System.Runtime.InteropServices;
using DataUploadAPI.Business.Contracts;
using DataUploadAPI.Business.Services;
using DataUploadAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DataUploadAPI.API.Configurations
{
    public static class ConfigureJsonService
    {
        public static IServiceCollection ConfigureJsonWriter(this IServiceCollection services,
            IConfiguration configuration)
        {
            var folder = configuration.GetSection("FileUploadJSONPath").Value ??
                                 "JsonFiles";
            var path = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
            path += "/";
            
            services.AddScoped<IJsonReaderWriterService>(provider => 
                new JsonReaderWriterService( path + folder)
            );

            return services;
        }
    }
}