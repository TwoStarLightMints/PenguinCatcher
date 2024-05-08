using Microsoft.EntityFrameworkCore;
using PenguinCatcher.Models.IdentityModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PenguinCatcher.Models.DomainModels
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [ForeignKey("PenguinCatcherUser")]
        [MaxLength(450)]
        public string UserId { get; set; } = string.Empty;
        public PenguinCatcherUser User { get; set; } = null!;
        public string Content { get; set; } = string.Empty;
        public int PostID { get; set; }
        public Post Post { get; set; } = null!;
    }
}
