using System;
using System.Runtime.InteropServices;
using DataUploadAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DataUploadAPI.API.Configurations
{
    public static class ConfigureConnections
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("FileUploadSQLServer") ??
                                 "Server=.;Database=UploaderDB;Trusted_Connection=True;Application Name=UploaderDB";
                
            services.AddDbContextPool<DataUploadContext>(options => options.UseSqlServer(connection));

            return services;
        }
    }
}