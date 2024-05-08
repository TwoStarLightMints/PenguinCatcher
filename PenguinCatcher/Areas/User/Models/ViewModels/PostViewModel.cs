using Microsoft.EntityFrameworkCore;
using PenguinCatcher.Models.DomainModels;

namespace PenguinCatcher.Areas.User.Models.ViewModels
{
    public class PostViewModel
    {
        public string Content { get; set; } = string.Empty;
        public string DistroName { get; set; } = string.Empty;
    }
}
