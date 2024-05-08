using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenguinCatcher.DataAccess;
using PenguinCatcher.Models;
using PenguinCatcher.Models.DomainModels;
using PenguinCatcher.Models.IdentityModels;

namespace PenguinCatcher.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdministrationController : Controller
    {
        private readonly PenguinContext _context;
        private readonly UserManager<PenguinCatcherUser> _userManager;

        public AdministrationController(PenguinContext context, UserManager<PenguinCatcherUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> ViewUsers(int? pageNum)
        {
            ViewBag.CurrUserId = _userManager.GetUserId(HttpContext.User);
            var users = _context.PenguinCatchers.ToList();

            List<(PenguinCatcherUser, bool)> usersAndIsAdmin = new List<(PenguinCatcherUser, bool)>();

            // Cannot use the foreach method due to niceties with asynchronous lambda method
            foreach (var u in users)
            {
                bool isAdmin = await _userManager.IsInRoleAsync(u, "Admin");
                usersAndIsAdmin.Add((u, isAdmin));
            }

            ViewBag.Pages = PaginatedPage<(PenguinCatcherUser, bool)>.CreateFromList(usersAndIsAdmin, pageNum ?? 1, 5);

            return View();
        }

        [HttpGet]
        public IActionResult DeleteUser(string userId)
        {
            var userToDelete = _context.Users.Where(u => u.Id == userId).First();

            _context.Remove(userToDelete);

            _context.SaveChanges();

            return RedirectToAction("ViewUsers", "Administration");
        }

        [HttpGet]
        public async Task<IActionResult> ViewPosts(int? pageNum)
        {
            return View(await PaginatedPage<Post>.CreateAsync(_context.Posts.Include(p => p.User).Include(p => p.Distribution), pageNum ?? 1, 5));
        }

        [HttpGet]
        public IActionResult DeletePost(int postId)
        {
            var postToDelete = _context.Posts.Where(p => p.PostID == postId).First();

            _context.Posts.Remove(postToDelete);

            _context.SaveChanges();

            return RedirectToAction("ViewPosts", "Administration");
        }

        [HttpGet]
        public async Task<IActionResult> ViewComments(int? pageNum)
        {
            return View(await PaginatedPage<Comment>.CreateAsync(_context.Comments.Include(c => c.User), pageNum ?? 1, 5));
        }

        [HttpGet]
        public IActionResult DeleteComment(int commentId)
        {
            var commentToDelete = _context.Comments.Where(c => c.CommentId == commentId).First();

            _context.Comments.Remove(commentToDelete);

            _context.SaveChanges();

            return RedirectToAction("ViewComments", "Administration");
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel rvm, bool isAdmin)
        {
            var newUser = new PenguinCatcherUser { UserName = rvm.UserName };

            var result = await _userManager.CreateAsync(newUser, rvm.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, isAdmin ? "Admin" : "Catcher");
            }

            return RedirectToAction("CreateUser", "Administration");
        }
    }
}
