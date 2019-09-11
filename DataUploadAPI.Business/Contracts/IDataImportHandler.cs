using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataUploadAPI.Business.ApiModels;

namespace DataUploadAPI.Business.Contracts
{
    public interface IDataImportHandler
    {
        Task SaveAllAsync(IEnumerable<ProductApiModel> products, CancellationToken ct = default);
    }
}