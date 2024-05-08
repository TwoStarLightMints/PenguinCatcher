using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenguinCatcher.DataAccess;
using PenguinCatcher.Models;
using PenguinCatcher.Models.DomainModels;
using PenguinCatcher.Models.IdentityModels;

namespace PenguinCatcher.Areas.User.Controllers
{
    [Area("User")]
    public class PostController : Controller
    {
        private readonly PenguinContext _context;
        private readonly UserManager<PenguinCatcherUser> _userManager;

        public PostController(PenguinContext context, UserManager<PenguinCatcherUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> UserPosts(int? pageNum)
        {
            var currUser = await _userManager.GetUserAsync(HttpContext.User);

            ViewBag.Pages = await PaginatedPage<Post>.CreateAsync(_context.Posts.Where(p => p.User == currUser).Include(p => p.Distribution), pageNum ?? 1, 5);

            return View("PostsPage");
        }
    }
}
