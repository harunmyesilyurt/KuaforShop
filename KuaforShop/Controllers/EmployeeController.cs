using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KuaforShop.Application.DTOs.EmployeeDTOs;
using KuaforShop.Application.Services;
using KuaforShop.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using KuaforShop.Application.DTOs.SaloonDTOs;
using KuaforShop.Core.Entities;

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
        public async Task<IActionResult> Edit(Guid id, EmployeeDTO updateEmployeeDTO)
        {
            if (id != updateEmployeeDTO.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(updateEmployeeDTO);

            var createDTO = new CreateEmployeeDTO
            {
                Name = updateEmployeeDTO.Name,
                Surname = updateEmployeeDTO.Surname,
                Sex = updateEmployeeDTO.Sex,
                SaloonId = updateEmployeeDTO.SaloonId,
                BeginTime = updateEmployeeDTO.BeginTime,
                EndTime = updateEmployeeDTO.EndTime,
                WorkDays = updateEmployeeDTO.WorkDays,
                Username = updateEmployeeDTO.Username, // Bu satırı ekleyin
                                                       // Expertise = updateEmployeeDTO.Expertise
            };

            await _employeeService.UpdateAsync(id, createDTO);
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