using KuaforShop.Core.Entities;
using KuaforShop.Core.Enumertaions;

namespace KuaforShop.Persistence.Repositories.Service
{
    public interface IServiceRepository : IRepository<Services>
    {
        Task<List<Services>> GetBySaloonAsync(Guid saloonId);
        Task<List<Services>> GetByPriceRangeAsync(double minPrice, double maxPrice);
        Task<List<Services>> GetByDurationAsync(int maxDuration, enmDurationType durationType);
    }
}