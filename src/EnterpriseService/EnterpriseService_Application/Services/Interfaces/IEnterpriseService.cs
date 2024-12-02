using EnterpriseService_Application.DTOs;
using EnterpriseService_Domain.Entities;

namespace EnterpriseService_Application.Services.Interfaces
{
    public interface IEnterpriseService
    {
        public Task Add(EnterpriseDTO enterpriseDto);
        public Task Update(EnterpriseDTO enterpriseDto);
        public Task Delete(int id);
        public Task<Enterprise> Get(int id);
        public Task<bool> EnterpriseExists(int id);
    }
}
