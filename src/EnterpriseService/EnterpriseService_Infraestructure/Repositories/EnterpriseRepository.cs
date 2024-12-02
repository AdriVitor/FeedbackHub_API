using EnterpriseService_Domain.Entities;
using EnterpriseService_Infraestructure.ContextDb;
using EnterpriseService_Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseService_Infraestructure.Repositories
{
    public class EnterpriseRepository : IEnterpriseRepository
    {
        private readonly Context _contextDb;
        public EnterpriseRepository(Context contextDb)
        {
            _contextDb = contextDb;
        }

        public async Task Add(Enterprise enterprise)
        {
            _contextDb.Enterprises.Add(enterprise);
            await SaveChanges();
        }

        public async Task Delete(Enterprise enterprise)
        {
            _contextDb.Enterprises.Remove(enterprise);
            await SaveChanges();
        }

        public async Task<Enterprise> Get(int id)
        {
            var enterprise = await _contextDb
                                    .Enterprises
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.Id == id);

            return enterprise;
        }

        public async Task Update(Enterprise enterprise)
        {
            _contextDb.Enterprises.Update(enterprise);
            await SaveChanges();
        }

        public async Task<bool> EnterpriseExists(int id)
        {
            var enterpriseExists = await _contextDb.Enterprises.AnyAsync(e => e.Id == id);
            return enterpriseExists;
        }

        private async Task SaveChanges()
        {
            await _contextDb.SaveChangesAsync();
        }
    }
}
