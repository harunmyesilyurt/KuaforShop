using KuaforShop.Application.DTOs.AppointmentDTOs;
using KuaforShop.Core.Enumertaions;

namespace KuaforShop.Application.Services
{
    public interface IAppointmentService
    {
        Task<AppointmentDTO> GetByIdAsync(Guid id);
        Task<List<AppointmentDTO>> GetAllAsync();
        Task<List<AppointmentDTO>> GetByUserAsync(Guid userId);
        Task<List<AppointmentDTO>> GetByEmployeeAsync(Guid employeeId);
        Task<List<AppointmentDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<AppointmentDTO>> GetByStatusAsync(enmAppointmentStatus status);
        Task<bool> CreateAsync(CreateAppointmentDTO createAppointmentDTO);
        Task<bool> UpdateStatusAsync(Guid id, enmAppointmentStatus status);
        Task<bool> CancelAsync(Guid id);
        Task<bool> IsEmployeeAvailableAsync(Guid employeeId, DateTime date, int duration);
        Task<List<DateTime>> GetAvailableTimeSlotsAsync(Guid employeeId, DateTime date);
        Task<int> GetTotalAppointmentsAsync();
        Task<int> GetTodaysAppointmentsCountAsync();
        Task<decimal> GetTotalRevenueAsync();
        Task<List<AppointmentDTO>> GetRecentAppointmentsAsync(int count);
    }
}