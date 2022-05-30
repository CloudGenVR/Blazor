using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationOldStyle.Models;

namespace WebApplicationOldStyle.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new ModelClass() {  CurrentDate = DateTime.UtcNow });
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

    public class ModelClass
    {
        public DateTime CurrentDate { get; set; }
    }
}