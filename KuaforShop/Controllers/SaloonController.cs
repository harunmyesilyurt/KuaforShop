using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KuaforShop.Application.DTOs.SaloonDTOs;
using KuaforShop.Application.Services;
using KuaforShop.Models;
using Microsoft.AspNetCore.Authorization;
using KuaforShop.Web.Infrastructure;
using KuaforShop.Application.Models;
using KuaforShop.Core.Enumertaions;

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
        public async Task<IActionResult> Edit(Guid id, SaloonDTO updateSaloonDTO)
        {
            if (id != updateSaloonDTO.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(updateSaloonDTO);

            var createDTO = new CreateSaloonDTO
            {
                Name = updateSaloonDTO.Name,
                Address = updateSaloonDTO.Address,
                Phone = updateSaloonDTO.Phone,
                OpenTime = updateSaloonDTO.OpenTime,
                CloseTime = updateSaloonDTO.CloseTime,
                WorkDays = updateSaloonDTO.WorkDays,
                SaloonType = updateSaloonDTO.SaloonType
            };

            await _saloonService.UpdateAsync(id, createDTO);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _saloonService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var saloons = new SaloonsService().GetSaloons();
            var ssaloons = await _saloonService.GetAllAsync();
            return View(MapSaloons(saloons));
        }

        private List<SaloonDTO> MapSaloons(List<SaloonResponseModel> saloons)
        {
            List<SaloonDTO> saloonDTOs = new();

            foreach (var saloon in saloons)
            {
                saloonDTOs.Add(new()
                {
                    Address = saloon.Address,
                     CloseTime = TimeOnly.FromTimeSpan(saloon.CloseTime),
                     Id = saloon.Id,
                     Name = saloon.Name,
                     OpenTime = TimeOnly.FromTimeSpan(saloon.OpenTime),
                     Phone = saloon.Phone,
                     SaloonType = (enmSaloonType)saloon.SaloonType,
                     WorkDays = (enmWorkDays)saloon.WorkDays
                });
            }
            return saloonDTOs;
        }
    }
}