using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KuaforShop.Application.Services;
using KuaforShop.Models;

namespace KuaforShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;
        private readonly IServiceService _serviceService;

        public AdminController(
            IAppointmentService appointmentService,
            IUserService userService,
            IServiceService serviceService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
            _serviceService = serviceService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AdminDashboardViewModel
            {
                TotalUsers = await _userService.GetTotalUsersAsync(),
                TotalAppointments = await _appointmentService.GetTotalAppointmentsAsync(),
                TodaysAppointments = await _appointmentService.GetTodaysAppointmentsCountAsync(),
                TotalRevenue = await _appointmentService.GetTotalRevenueAsync(),
                RecentAppointments = await _appointmentService.GetRecentAppointmentsAsync(5)
            };

            return View(viewModel);
        }
    }
}