using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataUploadAPI.Business.ApiModels;
using DataUploadAPI.Data.Entities;

namespace DataUploadAPI.Business.Contracts
{
    public interface IDataImporterService
    {
        Task SaveAllProductsAsync(IEnumerable<ProductApiModel> products, CancellationToken ct = default);
    }
}