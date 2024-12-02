using EnterpriseService_Domain.Entities;

namespace EnterpriseService_Infraestructure.Repositories.Interfaces
{
    public interface IEnterpriseRepository
    {
        public Task Add(Enterprise enterprise);
        public Task Update(Enterprise enterprise);
        public Task Delete(Enterprise enterprise);
        public Task<Enterprise> Get(int id);
        public Task<bool> EnterpriseExists(int id);
    }
}
