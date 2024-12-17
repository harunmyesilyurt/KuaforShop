using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KuaforShop.Application.DTOs.AppointmentDTOs;
using KuaforShop.Application.Services;
using KuaforShop.Models;

namespace KuaforShop.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;
        private readonly IServiceService _serviceService;

        public AppointmentController(
            IAppointmentService appointmentService,
            IUserService userService,
            IEmployeeService employeeService,
            IServiceService serviceService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
            _employeeService = employeeService;
            _serviceService = serviceService;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAllAsync();
            var viewModels = appointments.Select(a => new AppointmentViewModel
            {
                Id = a.Id,
                CustomerName = a.UserName,
                ServiceName = a.ServiceName,
                EmployeeName = a.EmployeeName,
                AppointmentDate = a.Date,
                Status = a.Status,
                Price = (decimal)a.ServicePrice
            }).ToList();

            return View(viewModels);
        }

        public async Task<IActionResult> Create()
        {
            await LoadSelectLists();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentDTO createAppointmentDTO)
        {
            if (!ModelState.IsValid)
            {
                await LoadSelectLists();
                return View(createAppointmentDTO);
            }

            // Çalışanın müsaitlik kontrolü
            var isAvailable = await _appointmentService.IsEmployeeAvailableAsync(
                createAppointmentDTO.EmployeeId,
                createAppointmentDTO.Date,
                30); // Varsayılan süre

            if (!isAvailable)
            {
                ModelState.AddModelError("", "Seçilen tarih ve saatte çalışan müsait değil.");
                await LoadSelectLists();
                return View(createAppointmentDTO);
            }

            await _appointmentService.CreateAsync(createAppointmentDTO);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cancel(Guid id)
        {
            await _appointmentService.CancelAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid id, int status)
        {
            await _appointmentService.UpdateStatusAsync(id, (Core.Enumertaions.enmAppointmentStatus)status);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetAvailableTimeSlots(Guid employeeId, DateTime date)
        {
            var slots = await _appointmentService.GetAvailableTimeSlotsAsync(employeeId, date);
            return Json(slots);
        }

        private async Task LoadSelectLists()
        {
            var users = await _userService.GetAllAsync();
            var employees = await _employeeService.GetAllAsync();
            var services = await _serviceService.GetAllAsync();

            ViewBag.Users = new SelectList(users, "Id", "Username");
            ViewBag.Employees = new SelectList(employees, "Id", "Name");
            ViewBag.Services = new SelectList(services, "Id", "Name");
        }
    }
}