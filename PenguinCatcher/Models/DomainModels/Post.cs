using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PenguinCatcher.Models.IdentityModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PenguinCatcher.Models.DomainModels
{
    public class Post
    {
        public int PostID { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; }
        public int DistributionID { get; set; }
        public Distribution Distribution { get; set; } = null!;
        [ForeignKey("PenguinCatcherUser")]
        [MaxLength(450)]
        public string UserId { get; set; } = string.Empty;
        public PenguinCatcherUser User { get; set; } = null!;
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
