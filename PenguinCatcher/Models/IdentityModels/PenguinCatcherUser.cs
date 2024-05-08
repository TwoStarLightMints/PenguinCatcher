using Microsoft.AspNetCore.Identity;
using PenguinCatcher.Models.DomainModels;

namespace PenguinCatcher.Models.IdentityModels
{
    public class PenguinCatcherUser: IdentityUser
    {
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
