using AutoMapper;
using DataUploadAPI.Business.Contracts;
using DataUploadAPI.Data.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace DataUploadAPI.Business.Services
{
    public partial class DataUploadService : IDataUploadService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public DataUploadService(IProductRepository productRepository,ICategoryRepository categoryRepository,
            IMemoryCache memoryCache,IMapper mapper
        )
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _cache = memoryCache;
            _mapper = mapper;
        }
    }
}