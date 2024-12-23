using KuaforShop.Core.Enumertaions;

namespace KuaforShop.Application.DTOs.AppointmentDTOs
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public enmAppointmentStatus Status { get; set; }

        // Navigation properties için ek bilgiler
        public string UserName { get; set; }
        public string ServiceName { get; set; }
        public string EmployeeName { get; set; }
        public double ServicePrice { get; set; }
    }
}