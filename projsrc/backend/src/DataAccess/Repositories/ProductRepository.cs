using Datalayer.Models;
using DataObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Datalayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyProjectContext _ctx;

        public ProductRepository(MyProjectContext context)
        {
            _ctx = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _ctx.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _ctx.Products.FindAsync(id);
        }

        public async Task CreateProductAsync(Product product)
        {
            _ctx.Products.Add(product);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _ctx.Products.Update(product);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _ctx.Products.FindAsync(id);
            if (product != null)
            {
                _ctx.Products.Remove(product);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
