using System.Diagnostics;
using KuaforShop.Models;
using KuaforShop.Application.Services;
using KuaforShop.Core.Enumertaions;
using Microsoft.AspNetCore.Mvc;
using KuaforShop.Application.Utils;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace KuaforShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;

        public HomeController(
            ILogger<HomeController> logger,
            IAppointmentService appointmentService,
            IUserService userService)
        {
            _logger = logger;
            _appointmentService = appointmentService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel();

            if (User.IsInRole("Admin"))
            {
                var appointments = await _appointmentService.GetAllAsync();

                viewModel.TotalUsers = await _userService.GetTotalUsersAsync();
                viewModel.TotalAppointments = await _appointmentService.GetTotalAppointmentsAsync();
                viewModel.TodaysAppointments = await _appointmentService.GetTodaysAppointmentsCountAsync();
                viewModel.TotalRevenue = await _appointmentService.GetTotalRevenueAsync();
                viewModel.RecentAppointments = (await _appointmentService.GetRecentAppointmentsAsync(5))
                    .Select(a => new AppointmentViewModel
                    {
                        Id = a.Id,
                        CustomerName = a.UserName,
                        ServiceName = a.ServiceName,
                        EmployeeName = a.EmployeeName,
                        AppointmentDate = a.Date,
                        Status = a.Status,
                        Price = (decimal)a.ServicePrice
                    }).ToList();
            }

            return View(viewModel);
        }

        ////[HttpPost]
        //[HttpPost]
        //public async Task<IActionResult> Tetikle(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest("Geçersiz dosya!");
        //    }

        //    try
        //    {
        //        using var memoryStream = new MemoryStream();
        //        await file.CopyToAsync(memoryStream);
        //        var fileBytes = memoryStream.ToArray();

        //        // Burada API'ye POST isteði gönderiyoruz
        //        var client = new HttpClient();
        //        var content = new MultipartFormDataContent
        //{
        //    { new ByteArrayContent(fileBytes), "file", file.FileName }
        //};

        //        var apiResponse = await client.PostAsync("https://api-url/your-endpoint", content);

        //        if (apiResponse.IsSuccessStatusCode)
        //        {
        //            var apiResult = await apiResponse.Content.ReadAsStringAsync();
        //            return Ok(new { message = "Baþarýlý", result = apiResult });
        //        }
        //        else
        //        {
        //            var error = await apiResponse.Content.ReadAsStringAsync();
        //            return StatusCode((int)apiResponse.StatusCode, error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Sunucu hatasý: {ex.Message}");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                ModelState.AddModelError("", "Lütfen bir resim yükleyin.");
                return View("Index"); 
            }

            using var httpClient = new HttpClient();
            using var formData = new MultipartFormDataContent();

            // Resmi ekle
            using var stream = new MemoryStream();
            await image.CopyToAsync(stream);
            var content = new ByteArrayContent(stream.ToArray());
            content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            formData.Add(content, "image", image.FileName);

            // API isteði gönder
            var response = await httpClient.PostAsync("http://node-js-faceshape.onrender.com/api/ai", formData);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return View("Result", jsonResponse); 
            }
            else
            {
                ModelState.AddModelError("", "API isteði baþarýsýz oldu.");
                return View("Index"); 
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
