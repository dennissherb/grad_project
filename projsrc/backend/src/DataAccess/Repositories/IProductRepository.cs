using Datalayer.Models;

namespace Datalayer.Repositories
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task UpdateProductAsync(Product product);
    }
}