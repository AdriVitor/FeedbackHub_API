using Product_Application.DTOs;
using Product_Application.Services.Interfaces;
using Product_Domain.Entities;
using Product_Infraestructure.Repositories.Interfaces;
using RabbitMQ_Lib;

namespace Product_Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly RabbitMQService _rabbitmqService;
        public ProductService(IProductRepository productRepository, RabbitMQService rabbitMQService)
        {
            _productRepository = productRepository;
            _rabbitmqService = rabbitMQService;
        }

        public async Task Add(ProductDTO productDto)
        {
            var basicProperties = _rabbitmqService.ConfigureBasicProperties("testefila2");
            _rabbitmqService.SendMessage(productDto.IdEnterprise.ToString(), "teste1", "routingkey", basicProperties);

            var tupla = await _rabbitmqService.ReceiveMessage("testefila2", basicProperties.CorrelationId);

            var message = tupla.Item1;
            if (Convert.ToBoolean(message) == false)
            {
                throw new Exception("Estabelecimento não encontrado");
            }

            var product = new Product(productDto.Name, productDto.Description, productDto.IdEnterprise);
            await _productRepository.Add(product);
        }

        public async Task Delete(int id, int idEnterprise)
        {
            var product = await Get(id, idEnterprise);
            await _productRepository.Delete(product);
        }

        public async Task<Product> Get(int id, int idEnterprise)
        {
            var product = await _productRepository.Get(id, idEnterprise);

             if(product is null)
                throw new ArgumentNullException(nameof(product));

             return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByEnterprise(int idEnterprise)
        {
            var products = await _productRepository.GetProductsByEnterprise(idEnterprise);

            return products;
        }

        public async Task Update(ProductDTO productDto)
        {
            var product = await Get(productDto.Id, productDto.IdEnterprise);

            product = new Product(product.Id, productDto.Name, productDto.Description, product.IdEnterprise, productDto.IsActive);

            await _productRepository.Update(product);
        }
    }
}
