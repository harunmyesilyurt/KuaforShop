using KuaforShop.Core.Entities;

namespace KuaforShop.Persistence.Repositories.Saloon
{
    public interface ISaloonRepository : IRepository<Saloons>
    {
        Task<List<Saloons>> GetBySaloonAsync(Guid saloonId);
        Task<List<Saloons>> GetByWorkDaysAsync(int workDays);
    }
}