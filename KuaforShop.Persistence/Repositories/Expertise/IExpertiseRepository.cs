using KuaforShop.Core.Entities;

namespace KuaforShop.Persistence.Repositories.Expertise
{
    public interface IExpertiseRepository : IRepository<Core.Entities.Expertise>
    {
        Task<bool> AddRangeForEmployeeAsync(Guid employeeId, List<string> expertise);
        Task<List<Core.Entities.Expertise>> GetByEmployeeAsync(Guid employeeId);
    }
}