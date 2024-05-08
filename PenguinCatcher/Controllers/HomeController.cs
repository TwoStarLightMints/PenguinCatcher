using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenguinCatcher.DataAccess;
using PenguinCatcher.Models;
using PenguinCatcher.Models.DomainModels;
using System.Diagnostics;

namespace PenguinCatcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PenguinContext _penguinContext;

        public HomeController(PenguinContext context, ILogger<HomeController> logger)
        {
            _penguinContext = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var recentCatches = _penguinContext.Posts.OrderByDescending(p => p.DatePosted).Include(p => p.User).Take(3).ToList();

            if (recentCatches != null)
                ViewBag.RecentCatches = recentCatches;
            else
                recentCatches = new List<Post>();

            return View(recentCatches);
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
