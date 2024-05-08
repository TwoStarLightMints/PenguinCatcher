using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenguinCatcher.DataAccess;
using PenguinCatcher.Models.ViewModels;
using PenguinCatcher.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using PenguinCatcher.Models.IdentityModels;
using System.Security.Claims;
using PenguinCatcher.Models;

namespace PenguinCatcher.Controllers
{
    public class PostsController : Controller
    {
        private readonly PenguinContext _context;
        private readonly UserManager<PenguinCatcherUser> _userManager;

        public PostsController(PenguinContext context, UserManager<PenguinCatcherUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Page(int? pageNum)
        {
            ViewBag.Pages = await PaginatedPage<Post>.CreateAsync(_context.Posts.Include(p => p.User).Include(p => p.Distribution), pageNum ?? 1, 5);
            return View("PostsPage");
        }

        public IActionResult Post(int postId)
        {
            ViewBag.CurrUserID = _userManager.GetUserId(HttpContext.User);

            return View(_context.Posts.Where(p => p.PostID == postId).Include(p => p.Distribution).Include(p => p.User).Include(p => p.Likes).Include(p => p.Comments).ThenInclude(c => c.User).First());
        }

        [HttpGet]
        public async Task<IActionResult> Like(int postId, string goodBad)
        {
            if (goodBad.Equals("like"))
            {
                var post = _context.Posts.Where(p => p.PostID == postId).First();

                Like newLike = new Like();
                newLike.Post = post;

                newLike.User = (await _userManager.GetUserAsync(HttpContext.User))!;

                newLike.Post = post;

                post.Likes.Add(newLike);
            }
            else if (goodBad.Equals("dislike"))
            {
                var post = _context.Posts.Where(p => p.PostID == postId).Include(p => p.Likes).First();

                var like = post.Likes.Where(l => l.UserId == _userManager.GetUserId(HttpContext.User)).First();

                post.Likes.Remove(like);
            }
            

            _context.SaveChanges();

            return Redirect($"/Posts/Post?postId={postId}");
        }

        [HttpGet]
        public IActionResult Comment(int postId)
        {
            return View(new CommentViewModel { PostId = postId });
        }

        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel cvm)
        {
            var newComment = new Comment();

            Console.WriteLine(cvm.PostId);

            newComment.Post = _context.Posts.Where(p => p.PostID == cvm.PostId).First();

            newComment.User = (await _userManager.GetUserAsync(HttpContext.User))!;

            newComment.Content = cvm.Content;

            _context.Comments.Add(newComment);

            _context.SaveChanges();

            return Redirect($"/Posts/Post?postId={cvm.PostId}");
        }
    }
}
