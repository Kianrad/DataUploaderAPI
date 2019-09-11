using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataUploadAPI.Business.ApiModels;
using DataUploadAPI.Data.Entities;

namespace DataUploadAPI.Business.Contracts
{
    public interface IDataUploadService
    {
       
        Task<ProductApiModel> AddProductAsync(ProductApiModel newProductApiModel,
            CancellationToken ct = default);
        
        
        Task<ProductApiModel> AddOrUpdateProductAsync(ProductApiModel newProductApiModel,
            CancellationToken ct = default);
        
        Task<bool> UpdateProductAsync(ProductApiModel newProductApiModel,
            CancellationToken ct = default);

        Task<bool> SaveAllProductsAsync(IEnumerable<ProductApiModel> productApiModels,
            CancellationToken ct = default);
        
        Task<ProductApiModel> GetProductByIdAsync(string id,
            CancellationToken ct = default);
        
        Task<CategoryApiModel> AddCategoryAsync(CategoryApiModel newCategoryApiModel,
            CancellationToken ct = default);

        Task<CategoryApiModel> GetCategoryByIdAsync(string id,
            CancellationToken ct = default);

       
       

        
    }
}