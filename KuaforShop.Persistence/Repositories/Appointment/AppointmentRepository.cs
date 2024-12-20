using KuaforShop.Core.Entities;
using KuaforShop.Core.Enumertaions;
using KuaforShop.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KuaforShop.Persistence.Repositories.Appointment
{
    public class AppointmentRepository : Repository<Appointments>, IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Appointments>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await GetWhere(x => x.Date >= startDate && x.Date <= endDate).ToListAsync();
        }

        public async Task<List<Appointments>> GetByEmployeeAsync(Guid employeeId)
        {
            return await GetWhere(x => x.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<List<Appointments>> GetByStatusAsync(enmAppointmentStatus status)
        {
            return await GetWhere(x => x.Status == status).ToListAsync();
        }

        public async Task<List<Appointments>> GetByUserAsync(Guid userId)
        {
            return await GetWhere(x => x.UserId == userId).ToListAsync();
        }

        public async Task<bool> HasOverlappingAppointmentAsync(Guid employeeId, DateTime startTime, DateTime endTime)
        {
            return await GetWhere(x =>
                x.EmployeeId == employeeId &&
                x.Status != enmAppointmentStatus.Cancelled &&
                x.Date < endTime &&
                x.Date.AddMinutes(30) > startTime)
                .AnyAsync();
        }
    }
}