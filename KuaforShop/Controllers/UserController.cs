using Microsoft.AspNetCore.Mvc;
using KuaforShop.Application.DTOs.UserDTOs;
using KuaforShop.Application.Services;
using KuaforShop.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KuaforShop.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISaloonService _saloonService;

        public UserController(IUserService userService, ISaloonService saloonService)
        {
            _userService = userService;
            _saloonService = saloonService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
                return View(createUserDTO);

            await _userService.CreateAsync(createUserDTO);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            var createUserDTO = new CreateUserDTO
            {
                Username = user.Username,
                Sex = user.Sex,
                Role = user.Role
            };

            return View(createUserDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CreateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid)
                return View(updateUserDTO);

            var result = await _userService.UpdateAsync(id, updateUserDTO);
            if (!result)
            {
                ModelState.AddModelError("", "Güncelleme işlemi başarısız oldu.");
                return View(updateUserDTO);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userService.GetByIdAsync(userId);

            var viewModel = new UserProfileViewModel
            {
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Sex = user.Sex,
                NotificationEnabled = user.NotificationEnabled,
                PreferredSaloonId = user.PreferredSaloonId
            };

            ViewBag.Saloons = new SelectList(await _saloonService.GetAllAsync(), "Id", "Name");

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Profile(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Saloons = new SelectList(await _saloonService.GetAllAsync(), "Id", "Name");
                return View(model);
            }

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Profile));
            }

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _userService.ChangePasswordAsync(userId, model.CurrentPassword, model.NewPassword);

            if (result)
            {
                TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirildi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Şifre değiştirilirken bir hata oluştu.";
            }

            return RedirectToAction(nameof(Profile));
        }
    }
}