using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KuaforShop.Application.DTOs.ServiceDTOs;
using KuaforShop.Application.Services;
using KuaforShop.Models;

namespace KuaforShop.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly ISaloonService _saloonService;

        public ServiceController(IServiceService serviceService, ISaloonService saloonService)
        {
            _serviceService = serviceService;
            _saloonService = saloonService;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _serviceService.GetAllAsync();
            return View(services);
        }

        public async Task<IActionResult> Create()
        {
            await LoadSaloonSelectList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceDTO createServiceDTO)
        {
            if (!ModelState.IsValid)
            {
                await LoadSaloonSelectList();
                return View(createServiceDTO);
            }

            await _serviceService.CreateAsync(createServiceDTO);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null)
                return NotFound();

            await LoadSaloonSelectList();
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CreateServiceDTO updateServiceDTO)
        {
            if (!ModelState.IsValid)
            {
                await LoadSaloonSelectList();
                return View(updateServiceDTO);
            }

            await _serviceService.UpdateAsync(id, updateServiceDTO);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _serviceService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadSaloonSelectList()
        {
            var saloons = await _saloonService.GetAllAsync();
            ViewBag.Saloons = new SelectList(saloons, "Id", "Name");
        }
    }
}