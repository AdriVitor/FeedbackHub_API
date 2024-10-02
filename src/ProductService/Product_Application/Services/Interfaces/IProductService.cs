using Product_Application.DTOs;
using Product_Domain.Entities;

namespace Product_Application.Services.Interfaces
{
    public interface IProductService
    {
        public Task Add(ProductDTO product);
        public Task Update(ProductDTO product);
        public Task Delete(int id, int idEnterprise);
        public Task<Product> Get(int id, int idEnterprise);
        public Task<IEnumerable<Product>> GetProductsByEnterprise(int idEnterprise);
    }
}
