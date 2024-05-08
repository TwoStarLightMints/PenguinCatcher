using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PenguinCatcher.Areas.User.Models.ViewModels;
using PenguinCatcher.DataAccess;
using PenguinCatcher.Models.DomainModels;
using PenguinCatcher.Models.IdentityModels;
using System.Security.Claims;

namespace PenguinCatcher.Areas.User.Controllers
{
    [Area("User")]
    public class CreationController : Controller
    {
        private readonly PenguinContext _context;
        private readonly UserManager<PenguinCatcherUser> _userManager;

        public CreationController(PenguinContext context, UserManager<PenguinCatcherUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        [HttpGet]
        public IActionResult CreateDistro()
        {
            return View(new DistroViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDistro(DistroViewModel distribution)
        {
            if (ModelState.IsValid)
            {
                Distribution newDistro = new Distribution { DistroName = distribution.DistroName, Developer = distribution.Developer, DistroURL = distribution.DistroUrl, ReleaseCycle = distribution.ReleaseCycle };
                
                if (_context.Distributions.Where(d => d.DistroName == newDistro.DistroName).ToList().Count == 0)
                    _context.Add(newDistro);
                
                await _context.SaveChangesAsync();

                return Redirect("/User/Dashboard/Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult CreatePost()
        {
            ViewBag.AvailableDistros = _context.Distributions.ToList();

            return View(new PostViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                Post newPost = new Post();

                newPost.Content = post.Content;

                newPost.Distribution = _context.Distributions.Where(d => d.DistroName == post.DistroName).First();

                var currUser = (await _userManager.GetUserAsync(HttpContext.User))!;
                newPost.User = currUser;

                newPost.DatePosted = DateTime.Now;

                _context.Add(newPost);
                await _context.SaveChangesAsync();

                if (await _userManager.IsInRoleAsync(currUser, "Admin"))
                    return Redirect("/Admin/Dashboard/Index");
                else if (await _userManager.IsInRoleAsync(currUser, "Catcher"))
                    return Redirect("/User/Dashboard/Index");
            }

            return View();
        }
    }
}
