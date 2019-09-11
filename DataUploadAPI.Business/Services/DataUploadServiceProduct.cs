using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataUploadAPI.Business.ApiModels;
using DataUploadAPI.Data.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace DataUploadAPI.Business.Services
{
    public partial class DataUploadService
    {
        
        public async Task<ProductApiModel> GetProductByIdAsync(string id, CancellationToken ct = default)
        {
                var product = await _productRepository.GetByIdAsync(id, ct);
                if (product == null)
                {
                    return null;
                }else
                    return _mapper.Map<ProductApiModel>(product);
        }

        
        public async Task<bool> SaveAllProductsAsync(IEnumerable<ProductApiModel> products,CancellationToken ct = default)
        {
            foreach (var product in products)
            {
                CategoryApiModel categoryApiModel = await GetCategoryByIdAsync(product.CategoryId,ct);
                if (categoryApiModel == null)
                    await AddCategoryAsync(product.Category,ct);

                await AddOrUpdateProductAsync(product, ct);
            }
            
            return true;
        }
        
        
        public async Task<ProductApiModel> AddOrUpdateProductAsync(ProductApiModel newProductApiModel,
            CancellationToken ct = default)
        {
            var product = await GetProductByIdAsync(newProductApiModel.Key,ct);
            if (product != null)
            {
                await UpdateProductAsync(newProductApiModel,ct);
            }
            else
            {
                newProductApiModel = await AddProductAsync(newProductApiModel,ct);                
            }
            
            return newProductApiModel;
        }
        
        public async Task<ProductApiModel> AddProductAsync(ProductApiModel newProductApiModel,
            CancellationToken ct = default)
        {
            var product = _mapper.Map<Product>(newProductApiModel);
            product.Category = null;
            product = await _productRepository.AddAsync(product, ct);
            
            return newProductApiModel;
        }

        public async Task<bool> UpdateProductAsync(ProductApiModel productApiModel,
            CancellationToken ct = default)
        {
            var product = await _productRepository.GetByIdAsync(productApiModel.Key, ct);

            if (product is null) return false;
            product.CategoryId = productApiModel.CategoryId;
            product.Color = productApiModel.Color;
            product.Price = productApiModel.Price;
            product.Q1 = productApiModel.Q1;
            product.Size = productApiModel.Size;
            product.DeliveredIn = product.DeliveredIn;
            product.DiscountPrice = product.DiscountPrice;

            return await _productRepository.UpdateAsync(product, ct);
        }
        
        
    }
}