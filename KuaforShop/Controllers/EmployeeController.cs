using KuaforShop.Application.DTOs.EmployeeDTOs;
using KuaforShop.Application.Services;
using KuaforShop.Application.Services.Employee;
using KuaforShop.Application.Services.Saloon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KuaforShop.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ISaloonService _saloonService;

        public EmployeeController(IEmployeeService employeeService, ISaloonService saloonService)
        {
            _employeeService = employeeService;
            _saloonService = saloonService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllAsync();
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            await LoadSaloonSelectList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDTO createEmployeeDTO)
        {
            if (!ModelState.IsValid)
            {
                await LoadSaloonSelectList();
                return View(createEmployeeDTO);
            }

            await _employeeService.CreateAsync(createEmployeeDTO);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            await LoadSaloonSelectList();
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CreateEmployeeDTO updateEmployeeDTO)
        {
            if (!ModelState.IsValid)
            {
                await LoadSaloonSelectList();
                return View(updateEmployeeDTO);
            }

            await _employeeService.UpdateAsync(id, updateEmployeeDTO);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _employeeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadSaloonSelectList()
        {
            var saloons = await _saloonService.GetAllAsync();
            ViewBag.Saloons = new SelectList(saloons, "Id", "Name");
        }
    }
}