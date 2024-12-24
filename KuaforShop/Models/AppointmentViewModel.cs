using KuaforShop.Core.Enumertaions;

namespace KuaforShop.Models
{
    public class AppointmentViewModel
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string ServiceName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public enmAppointmentStatus Status { get; set; }
        public decimal Price { get; set; }
        public bool CanCancel => Status != enmAppointmentStatus.Cancelled;
        public bool IsPending => Status == enmAppointmentStatus.Waiting;
    }
}