using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataUploadAPI.Data.Contracts;
using DataUploadAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataUploadAPI.Data.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly DataUploadContext _context;
        public ProductRepository(DataUploadContext context)
        {
            _context = context;
        }
        
        private async Task<bool> ProductExists(string id, CancellationToken ct = default) =>
            await _context.Products.AnyAsync(g => g.Key == id, ct);

        public void Dispose() => _context.Dispose();

        
        public async Task<List<Product>> GetAllAsync(CancellationToken ct = default) => await _context.Products.ToListAsync(ct);
        
        public async Task<Product> GetByIdAsync(string id, CancellationToken ct = default)
        {
            var dbProduct = await _context.Products.FindAsync(id);
            return dbProduct;
        }
        
        public async Task<Product> AddAsync(Product newProduct, CancellationToken ct = default)
        {
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync(ct);
            return newProduct;
        }
        
        public async Task<Product> AddOrUpdateAsync(Product newProduct, CancellationToken ct = default)
        {
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync(ct);
            return newProduct;
        }
        
        public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        {
            if (!await ProductExists(id, ct))
                return false;
            var toRemove = _context.Products.Find(id);
            _context.Products.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> UpdateAsync(Product product, CancellationToken ct = default)
        {
            if (!await ProductExists(product.Key, ct))
                return false;
            _context.Products.Update(product);
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}