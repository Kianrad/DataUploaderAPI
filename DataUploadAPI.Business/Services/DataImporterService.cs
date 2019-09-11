using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataUploadAPI.Business.ApiModels;
using DataUploadAPI.Business.Contracts;

namespace DataUploadAPI.Business.Services
{
    public class DataImporterService : IDataImporterService
    {
         private List<IDataImportHandler> _repositories;
         
        public DataImporterService(List<IDataImportHandler> repositories)
        {
            _repositories = repositories;
        }
        
        public async Task SaveAllProductsAsync(IEnumerable<ProductApiModel> products, CancellationToken ct = default)
        {
            foreach (var repo in _repositories)
            { 
                 await repo.SaveAllAsync(products,ct);
            }
        } 
    }
}