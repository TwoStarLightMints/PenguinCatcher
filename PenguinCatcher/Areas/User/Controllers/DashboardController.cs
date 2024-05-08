using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PenguinCatcher.DataAccess;
using PenguinCatcher.Models.IdentityModels;
using System.Security.Claims;

namespace PenguinCatcher.Areas.NormalUser.Controllers
{
    [Authorize(Roles = "Catcher")]
    [Area("User")]
    public class DashboardController : Controller
    {
        private readonly PenguinContext _context;
        private readonly UserManager<PenguinCatcherUser> _userManager;

        public DashboardController(PenguinContext context, UserManager<PenguinCatcherUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_context.Posts.Where(p => p.User.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).OrderByDescending(p => p.DatePosted).Reverse().Take(3).ToList());
        }
    }
}
