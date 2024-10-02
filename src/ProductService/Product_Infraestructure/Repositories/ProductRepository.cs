using Microsoft.EntityFrameworkCore;
using Product_Domain.Entities;
using Product_Infraestructure.Context;
using Product_Infraestructure.Repositories.Interfaces;

namespace Product_Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ContextDb _context;
        public ProductRepository(ContextDb context)
        {
            _context = context;
        }

        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> Get(int id, int idEnterprise)
        {
            var product = await _context
                                .Products
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == id && p.IdEnterprise == idEnterprise);

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByEnterprise(int idEnterprise)
        {
            var products = _context
                           .Products
                           .Where(p => p.IdEnterprise == idEnterprise);

            return products;
        }

        public async Task Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
