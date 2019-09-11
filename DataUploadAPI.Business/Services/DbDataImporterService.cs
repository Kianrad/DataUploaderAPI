using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using DataUploadAPI.Business.ApiModels;
using DataUploadAPI.Data.Contracts;
using DataUploadAPI.Data.Entities;
using DataUploadAPI.Business.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataUploadAPI.Business.Services
{
    public class DbDataImporterService : IDataImportHandler
    {
        private readonly IDataUploadService _dataUploadService;
        
        public DbDataImporterService(IDataUploadService dataUploadService)
        {
            _dataUploadService = dataUploadService;
        }

        public async Task SaveAllAsync(IEnumerable<ProductApiModel> products, CancellationToken ct = default)
        {
            await _dataUploadService.SaveAllProductsAsync(products,ct);
        } 
        
    }
}