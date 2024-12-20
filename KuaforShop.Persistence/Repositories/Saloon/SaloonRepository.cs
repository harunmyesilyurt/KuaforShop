using KuaforShop.Core.Entities;
using KuaforShop.Core.Enumertaions;
using KuaforShop.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KuaforShop.Persistence.Repositories.Saloon
{
    public class SaloonRepository : Repository<Saloons>, ISaloonRepository
    {
        public SaloonRepository(AppDbContext context) : base(context) { }

        public async Task<List<Saloons>> GetBySaloonAsync(Guid saloonId)
        {
            return await GetWhere(x => x.Id == saloonId).ToListAsync();
        }

        public async Task<List<Saloons>> GetByWorkDaysAsync(int workDays)
        {
            var workDaysEnum = (enmWorkDays)workDays;
            return await GetWhere(x => x.WorkDays == workDaysEnum).ToListAsync();
        }
    }
}