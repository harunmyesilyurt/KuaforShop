using KuaforShop.Core.Entities;
using KuaforShop.Core.Enumertaions;

namespace KuaforShop.Persistence.Repositories.Appointment
{
    public interface IAppointmentRepository : IRepository<Appointments>
    {
        Task<List<Appointments>> GetByUserAsync(Guid userId);
        Task<List<Appointments>> GetByEmployeeAsync(Guid employeeId);
        Task<List<Appointments>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<Appointments>> GetByStatusAsync(enmAppointmentStatus status);
        Task<bool> HasOverlappingAppointmentAsync(Guid employeeId, DateTime startTime, DateTime endTime);
    }
}