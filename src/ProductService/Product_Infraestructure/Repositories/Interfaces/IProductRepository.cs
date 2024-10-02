using Product_Domain.Entities;

namespace Product_Infraestructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task Add(Product product);
        public Task Update(Product product);
        public Task Delete(Product product);
        public Task<Product> Get(int id, int idEnterprise);
        public Task<IEnumerable<Product>> GetProductsByEnterprise(int idEnterprise);
    }
}
