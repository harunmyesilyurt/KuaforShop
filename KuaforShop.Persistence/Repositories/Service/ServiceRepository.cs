using KuaforShop.Core.Entities;
using KuaforShop.Core.Enumertaions;
using KuaforShop.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KuaforShop.Persistence.Repositories.Service
{
    public class ServiceRepository : Repository<Services>, IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Services>> GetBySaloonAsync(Guid saloonId)
        {
            return await GetWhere(x => x.SaloonId == saloonId).ToListAsync();
        }

        public async Task<List<Services>> GetByPriceRangeAsync(double minPrice, double maxPrice)
        {
            return await GetWhere(x => x.Price >= minPrice && x.Price <= maxPrice).ToListAsync();
        }

        public async Task<List<Services>> GetByDurationAsync(int maxDuration, enmDurationType durationType)
        {
            return await GetWhere(x => x.Duration <= maxDuration && x.DurationType == durationType).ToListAsync();
        }
    }
}