using EnterpriseService_Application.DTOs;
using EnterpriseService_Application.Services.Interfaces;
using EnterpriseService_Domain.Entities;
using EnterpriseService_Infraestructure.Repositories.Interfaces;

namespace EnterpriseService_Application.Services
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IEnterpriseRepository _enterpriseRepository;
        public EnterpriseService(IEnterpriseRepository enterpriseRepository)
        {
            _enterpriseRepository = enterpriseRepository;
        }

        public async Task Add(EnterpriseDTO enterpriseDto)
        {
            var enterprise = new Enterprise(enterpriseDto.Id, enterpriseDto.Name, enterpriseDto.CNPJ, enterpriseDto.Description);
            await _enterpriseRepository.Add(enterprise);
        }

        public async Task Delete(int id)
        {
            var enterprise = await Get(id);

            await _enterpriseRepository.Delete(enterprise);
        }

        public async Task<Enterprise> Get(int id)
        {
            var enterprise = await _enterpriseRepository.Get(id);
            if(enterprise is null)
                throw new ArgumentNullException(nameof(enterprise));

            return enterprise;
        }

        public async Task Update(EnterpriseDTO enterpriseDto)
        {
            var enterprise = await Get(enterpriseDto.Id);

            enterprise = new Enterprise(enterprise.Id, enterpriseDto.Name, enterpriseDto.CNPJ, enterpriseDto.Description);

            await _enterpriseRepository.Update(enterprise);
        }
    }
}
