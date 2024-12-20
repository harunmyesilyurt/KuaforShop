using KuaforShop.Core.Entities;
using KuaforShop.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KuaforShop.Persistence.Repositories.Expertise
{
    public class ExpertiseRepository : Repository<Core.Entities.Expertise>, IExpertiseRepository
    {
        private readonly AppDbContext _context;

        public ExpertiseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddRangeForEmployeeAsync(Guid employeeId, List<string> expertise)
        {
            try
            {
                var expertiseList = expertise.Select(e => new Core.Entities.Expertise
                {
                    EmployeeId = employeeId,
                    Name = e
                }).ToList();

                await _context.Expertise.AddRangeAsync(expertiseList);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Core.Entities.Expertise>> GetByEmployeeAsync(Guid employeeId)
        {
            return await GetWhere(x => x.EmployeeId == employeeId).ToListAsync();
        }
    }
}