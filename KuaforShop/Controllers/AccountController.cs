using Microsoft.AspNetCore.Mvc;
using KuaforShop.Application.DTOs.UserDTOs;
using KuaforShop.Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace KuaforShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return View(loginDTO);

            // Admin kontrolü
            if (loginDTO.Username == "g211210089@sakarya.edu.tr" && loginDTO.Password == "sau")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()), // Admin için sabit bir GUID de atayabilirsiniz
                    new Claim(ClaimTypes.Name, loginDTO.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Admin");
            }

            // Normal kullanıcı kontrolü
            var user = await _userService.ValidateUserAsync(loginDTO);
            if (user == null)
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre");
                return View(loginDTO);
            }

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "User")
            };

            var userClaimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userAuthProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(userClaimsIdentity),
                userAuthProperties);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
                return View(createUserDTO);

            createUserDTO.Role = Core.Enumertaions.enmRoles.User;
            var result = await _userService.CreateAsync(createUserDTO);

            if (!result)
            {
                ModelState.AddModelError("", "Kayıt işlemi başarısız oldu");
                return View(createUserDTO);
            }

            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}