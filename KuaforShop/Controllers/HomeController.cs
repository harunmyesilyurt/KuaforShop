using System.Diagnostics;
using KuaforShop.Models;
using KuaforShop.Application.Services;
using KuaforShop.Core.Enumertaions;
using Microsoft.AspNetCore.Mvc;
using KuaforShop.Application.Utils;

namespace KuaforShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;

        public HomeController(
            ILogger<HomeController> logger,
            IAppointmentService appointmentService,
            IUserService userService)
        {
            _logger = logger;
            _appointmentService = appointmentService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel();

            if (User.IsInRole("Admin"))
            {
                var appointments = await _appointmentService.GetAllAsync();

                viewModel.TotalUsers = await _userService.GetTotalUsersAsync();
                viewModel.TotalAppointments = await _appointmentService.GetTotalAppointmentsAsync();
                viewModel.TodaysAppointments = await _appointmentService.GetTodaysAppointmentsCountAsync();
                viewModel.TotalRevenue = await _appointmentService.GetTotalRevenueAsync();
                viewModel.RecentAppointments = (await _appointmentService.GetRecentAppointmentsAsync(5))
                    .Select(a => new AppointmentViewModel
                    {
                        Id = a.Id,
                        CustomerName = a.UserName,
                        ServiceName = a.ServiceName,
                        EmployeeName = a.EmployeeName,
                        AppointmentDate = a.Date,
                        Status = a.Status,
                        Price = (decimal)a.ServicePrice
                    }).ToList();
            }

            return View(viewModel);
        }

        public IActionResult Tetikle()
        {
            var a = new AIHelper();
            var b = Task.Run(async () => await a.CreateHair("")).Result;
            return View("ok");
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
