using KuaforShop.Core.Entities;
using KuaforShop.Core.Enumertaions;
using KuaforShop.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KuaforShop.Persistence.Repositories.Employee
{
    public class EmployeeRepository : Repository<Employees>, IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Employees>> GetAvailableEmployeesAsync(DateTime date)
        {
            var timeOfDay = TimeOnly.FromTimeSpan(date.TimeOfDay);
            return await GetWhere(x =>
                x.BeginTime <= timeOfDay &&
                x.EndTime >= timeOfDay)
                .ToListAsync();
        }

        public async Task<List<Employees>> GetByRoleAsync(enmRoles role)
        {
            return await GetWhere(x => x.Role == role).ToListAsync();
        }

        public async Task<List<Employees>> GetBySaloonAsync(Guid saloonId)
        {
            return await GetWhere(x => x.SaloonId == saloonId).ToListAsync();
        }

        public async Task<Employees> GetByUsernameAsync(string username)
        {
            return await GetSingleAsync(x => x.Username == username);
        }
    }
}