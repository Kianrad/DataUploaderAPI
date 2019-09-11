using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataUploadAPI.Business.ApiModels;
using DataUploadAPI.Data.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace DataUploadAPI.Business.Services
{
    public partial class DataUploadService
    {
        public async Task<CategoryApiModel> AddCategoryAsync(CategoryApiModel categoryApiModel, CancellationToken ct = default)
        {
            var category = _mapper.Map<Category>(categoryApiModel);
            category = await _categoryRepository.AddAsync(category, ct);
            return _mapper.Map<CategoryApiModel>(category); 
        }
        
        
        public async Task<CategoryApiModel> GetCategoryByIdAsync(string id, CancellationToken ct = default)
        {
            var category = _cache.Get<Category>(id);
            if (category != null)
            {
                return _mapper.Map<CategoryApiModel>(category);
            }
            else
            {
                category = await _categoryRepository.GetByIdAsync(id, ct);
                if (category != null)
                {
                    var cacheEntryOptions =
                        new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                    _cache.Set(category.Id, category, cacheEntryOptions);
                }

                return _mapper.Map<CategoryApiModel>(category);
            }
        }
        
        
    }
}