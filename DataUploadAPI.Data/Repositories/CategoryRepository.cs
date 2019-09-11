using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataUploadAPI.Data.Contracts;
using DataUploadAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataUploadAPI.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataUploadContext _context;
        public CategoryRepository(DataUploadContext context)
        {
            _context = context;
        }
        
        private async Task<bool> CategoryExists(string id, CancellationToken ct = default) =>
            await _context.Categories.AnyAsync(g => g.Id == id, ct);

        public void Dispose() => _context.Dispose();

        
        public async Task<List<Category>> GetAllAsync(CancellationToken ct = default) => await _context.Categories.ToListAsync(ct);
        
        public async Task<Category> GetByIdAsync(string id, CancellationToken ct = default)
        {
            _context.Database.EnsureCreated();
            var dbCategory = await _context.Categories.FindAsync(id);
            return dbCategory;
        }
        
        public async Task<Category> AddAsync(Category newCategory, CancellationToken ct = default)
        {
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync(ct);
            return newCategory;
        }
        
        public async Task<Category> AddOrUpdateAsync(Category newCategory, CancellationToken ct = default)
        {
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync(ct);
            return newCategory;
        }
        
        public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        {
            if (!await CategoryExists(id, ct))
                return false;
            var toRemove = _context.Categories.Find(id);
            _context.Categories.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> UpdateAsync(Category category, CancellationToken ct = default)
        {
            if (!await CategoryExists(category.Id, ct))
                return false;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(ct);
            return true;
        }
        
    }
}