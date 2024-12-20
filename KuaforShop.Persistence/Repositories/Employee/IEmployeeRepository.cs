using KuaforShop.Core.Entities;
using KuaforShop.Core.Enumertaions;

namespace KuaforShop.Persistence.Repositories.Employee
{
    public interface IEmployeeRepository : IRepository<Employees>
    {
        Task<List<Employees>> GetBySaloonAsync(Guid saloonId);
        Task<List<Employees>> GetByRoleAsync(enmRoles role);
        Task<Employees> GetByUsernameAsync(string username);
        Task<List<Employees>> GetAvailableEmployeesAsync(DateTime date);
    }
}