using System.Diagnostics;
using KuaforShop.Models;
using KuaforShop.Application.Services;
using KuaforShop.Core.Enumertaions;
using Microsoft.AspNetCore.Mvc;

namespace KuaforShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppointmentService _appointmentService;

        public HomeController(
            ILogger<HomeController> logger,
            IAppointmentService appointmentService)
        {
            _logger = logger;
            _appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAllAsync();

            var viewModel = new DashboardViewModel
            {
                TotalAppointments = appointments.Count,
                PendingAppointments = appointments.Count(a => a.Status == enmAppointmentStatus.Waiting),
                CompletedAppointments = appointments.Count(a => a.Status == enmAppointmentStatus.Approved),
                CancelledAppointments = appointments.Count(a => a.Status == enmAppointmentStatus.Cencelled),
                TotalRevenue = appointments
                    .Where(a => a.Status == enmAppointmentStatus.Approved)
                    .Sum(a => (decimal)a.ServicePrice),
                UpcomingAppointments = appointments
                    .Where(a => a.Date > DateTime.Now && a.Status != enmAppointmentStatus.Cencelled)
                    .Select(a => new AppointmentViewModel
                    {
                        Id = a.Id,
                        CustomerName = a.UserName,
                        ServiceName = a.ServiceName,
                        EmployeeName = a.EmployeeName,
                        AppointmentDate = a.Date,
                        Status = a.Status,
                        Price = (decimal)a.ServicePrice
                    })
                    .Take(5)
                    .ToList(),
                TodaysAppointments = appointments
                    .Where(a => a.Date.Date == DateTime.Today)
                    .Select(a => new AppointmentViewModel
                    {
                        Id = a.Id,
                        CustomerName = a.UserName,
                        ServiceName = a.ServiceName,
                        EmployeeName = a.EmployeeName,
                        AppointmentDate = a.Date,
                        Status = a.Status,
                        Price = (decimal)a.ServicePrice
                    })
                    .ToList()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
