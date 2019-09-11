using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataUploadAPI.Data.Entities;

namespace DataUploadAPI.Data.Contracts
{
    public interface IProductRepository : IDisposable
    {
        Task<List<Product>> GetAllAsync(CancellationToken ct = default);
        Task<Product> GetByIdAsync(string id, CancellationToken ct = default);
        Task<Product> AddAsync(Product newProduct, CancellationToken ct = default);
        Task<Product> AddOrUpdateAsync(Product product, CancellationToken ct = default);
        Task<bool> UpdateAsync(Product product, CancellationToken ct = default);
        Task<bool> DeleteAsync(string id, CancellationToken ct = default);
    }
}