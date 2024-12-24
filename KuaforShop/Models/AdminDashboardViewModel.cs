using KuaforShop.Application.DTOs.AppointmentDTOs;

namespace KuaforShop.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalAppointments { get; set; }
        public int TodaysAppointments { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<AppointmentDTO> RecentAppointments { get; set; }
    }
}