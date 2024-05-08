using Microsoft.EntityFrameworkCore;
using PenguinCatcher.Models.IdentityModels;

namespace PenguinCatcher.Models.DomainModels
{
    public class Like
    {
        public int LikeID { get; set; }
        public int PostID { get; set; }
        public Post Post { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
        public PenguinCatcherUser User { get; set; } = null!;
    }
}
