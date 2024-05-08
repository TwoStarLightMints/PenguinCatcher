using Microsoft.AspNetCore.Identity;

namespace PenguinCatcher.Models.IdentityModels
{
    public class PenguinCatcherRole: IdentityRole<Guid>
    {
        public string Description { get; set; } = string.Empty;
    }
}
