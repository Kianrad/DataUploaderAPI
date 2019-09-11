using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataUploadAPI.Data.Entities;

namespace DataUploadAPI.Data.Contracts
{
    public interface ICategoryRepository : IDisposable
    {
        Task<List<Category>> GetAllAsync(CancellationToken ct = default);
        Task<Category> GetByIdAsync(string id, CancellationToken ct = default);
        Task<Category> AddAsync(Category newCategory, CancellationToken ct = default);
        Task<Category> AddOrUpdateAsync(Category category, CancellationToken ct = default);
        Task<bool> UpdateAsync(Category category, CancellationToken ct = default);
        Task<bool> DeleteAsync(string id, CancellationToken ct = default);
    }
}