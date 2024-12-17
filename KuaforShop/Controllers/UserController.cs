using KuaforShop.Application.DTOs.UserDTOs;
using KuaforShop.Application.Services;
using KuaforShop.Application.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace KuaforShop.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CreateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid)
                return View(updateUserDTO);

            await _userService.UpdateAsync(id, updateUserDTO);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}