using Microsoft.Extensions.Caching.Memory;
using ProductManagementApp.Interfaces;
using ProductManagementApp.Models;

namespace ProductManagementApp.Services
{
    public class ProductServices : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _memoryCache;

        public ProductServices(IProductRepository productRepository, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _productRepository = productRepository;
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Dictionary<string, decimal>> GetAveragePriceByCategoryAsync()
        {
            const string cacheKey = "AveragePriceByCategory";

            if (!_memoryCache.TryGetValue(cacheKey,out Dictionary<string, decimal> result))
            {
                var products = await _productRepository.GetAllAsync();
                result = products.GroupBy(p=>p.Category).
                    ToDictionary(g=>g.Key, g => g.Average(p=>p.Price));
                _memoryCache.Set(cacheKey, result,TimeSpan.FromMinutes(5));
            }

            return result;
        }

        public async Task<string> GetHighestStockCategoryAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.GroupBy(p => p.Category)
                          .OrderByDescending(g => g.Sum(p => p.Stock))
                          .FirstOrDefault()?.Key;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }
    }
}
