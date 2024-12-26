using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KuaforShop.Application.DTOs.AppointmentDTOs;
using KuaforShop.Application.Services;
using KuaforShop.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KuaforShop.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Admin")]
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

        //public async Task<IActionResult> Create(Guid? saloonId)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = await _userService.GetByIdAsync(Guid.Parse(userId));

        //    ViewBag.UserId = userId;

        //    var employees = saloonId.HasValue ?
        //        await _employeeService.GetBySaloonAsync(saloonId.Value) :
        //        await _employeeService.GetAllAsync();
        //    var services = saloonId.HasValue ?
        //        await _serviceService.GetBySaloonAsync(saloonId.Value) :
        //        await _serviceService.GetAllAsync();

        //    ViewBag.Employees = new SelectList(employees, "Id", "Name");
        //    ViewBag.Services = new SelectList(services, "Id", "Name");

        //    return View();
        //}
        public async Task<IActionResult> Create(Guid? saloonId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetByIdAsync(Guid.Parse(userId));

            ViewBag.UserId = userId;

            var employees = saloonId.HasValue ?
                await _employeeService.GetBySaloonAsync(saloonId.Value) :
                await _employeeService.GetAllAsync();
            var services = saloonId.HasValue ?
                await _serviceService.GetBySaloonAsync(saloonId.Value) :
                await _serviceService.GetAllAsync();

            ViewBag.ServicePrices = services.ToDictionary(s => s.Id.ToString(), s => s.Price);

            ViewBag.Employees = new SelectList(employees, "Id", "Name");
            ViewBag.Services = new SelectList(services, "Id", "Name");

            // Hizmet fiyatlarını JSON formatında ViewBag'e ekleyin
            ViewBag.ServicePrices = services.ToDictionary(s => s.Id.ToString(), s => s.Price);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentDTO createAppointmentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await LoadSelectLists();
                    return View(createAppointmentDTO);
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                createAppointmentDTO.UserId = Guid.Parse(userId);

                var result = await _appointmentService.CreateAsync(createAppointmentDTO);
                if (!result)
                {
                    ModelState.AddModelError("", "Randevu oluşturulamadı. Lütfen seçtiğiniz tarih ve saati kontrol edin.");
                    await LoadSelectLists();
                    return View(createAppointmentDTO);
                }

                return RedirectToAction(nameof(MyAppointments));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                await LoadSelectLists();
                return View(createAppointmentDTO);
            }
        }

        public async Task<IActionResult> MyAppointments()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var appointments = await _appointmentService.GetByUserAsync(userId);

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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(Guid id, int status)
        {
            await _appointmentService.UpdateStatusAsync(id, (Core.Enumertaions.enmAppointmentStatus)status);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cancel(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var appointment = await _appointmentService.GetByIdAsync(id);

            if (appointment == null || appointment.UserId != userId)
            {
                return Forbid();
            }

            await _appointmentService.CancelAsync(id);
            return RedirectToAction(nameof(MyAppointments));
        }

        public async Task<IActionResult> GetAvailableTimeSlots(Guid employeeId, DateTime date)
        {
            var slots = await _appointmentService.GetAvailableTimeSlotsAsync(employeeId, date);
            return Json(slots);
        }

        private async Task LoadSelectLists(Guid? saloonId = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;

            var employees = saloonId.HasValue ?
                await _employeeService.GetBySaloonAsync(saloonId.Value) :
                await _employeeService.GetAllAsync();
            var services = saloonId.HasValue ?
                await _serviceService.GetBySaloonAsync(saloonId.Value) :
                await _serviceService.GetAllAsync();

            ViewBag.Employees = new SelectList(employees, "Id", "Name");
            ViewBag.Services = new SelectList(services, "Id", "Name");
        }
    }
}