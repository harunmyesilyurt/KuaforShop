using KuaforShop.Application.DTOs.SaloonDTOs;
using KuaforShop.Application.Services;
using KuaforShop.Application.Services.Saloon;
using Microsoft.AspNetCore.Mvc;

namespace KuaforShop.Controllers
{
    public class SaloonController : Controller
    {
        private readonly ISaloonService _saloonService;

        public SaloonController(ISaloonService saloonService)
        {
            _saloonService = saloonService;
        }

        public async Task<IActionResult> Index()
        {
            var saloons = await _saloonService.GetAllAsync();
            return View(saloons);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSaloonDTO createSaloonDTO)
        {
            if (!ModelState.IsValid)
                return View(createSaloonDTO);

            await _saloonService.CreateAsync(createSaloonDTO);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var saloon = await _saloonService.GetByIdAsync(id);
            if (saloon == null)
                return NotFound();

            return View(saloon);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CreateSaloonDTO updateSaloonDTO)
        {
            if (!ModelState.IsValid)
                return View(updateSaloonDTO);

            await _saloonService.UpdateAsync(id, updateSaloonDTO);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _saloonService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}