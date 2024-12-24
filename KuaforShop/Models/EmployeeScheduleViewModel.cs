namespace KuaforShop.Models
{
    public class EmployeeScheduleViewModel
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public List<TimeSlot> AvailableSlots { get; set; }
        public List<AppointmentViewModel> DailyAppointments { get; set; }
    }

    public class TimeSlot
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAvailable { get; set; }
    }
}