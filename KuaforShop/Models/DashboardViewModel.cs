using KuaforShop.Application.DTOs.AppointmentDTOs;

namespace KuaforShop.Models
{
    public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalAppointments { get; set; }
        public int TodaysAppointments { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<AppointmentViewModel> RecentAppointments { get; set; }

        public DashboardViewModel()
        {
            RecentAppointments = new List<AppointmentViewModel>();
        }
    }
}