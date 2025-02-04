using ProductManagementApp.Models;
using System.Threading.Tasks;

namespace ProductManagementApp.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<Dictionary<string, decimal>> GetAveragePriceByCategoryAsync();
        Task<string> GetHighestStockCategoryAsync();
    }
}
