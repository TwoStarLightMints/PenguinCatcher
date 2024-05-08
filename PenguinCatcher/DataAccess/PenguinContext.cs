using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PenguinCatcher.Models.IdentityModels;
using PenguinCatcher.Models.DomainModels;
using Microsoft.Identity;

namespace PenguinCatcher.DataAccess
{
    public class PenguinContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Distribution> Distributions { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PenguinCatcherUser> PenguinCatchers { get; set; }

        public PenguinContext(DbContextOptions<PenguinContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var passwordHasher = new PasswordHasher<PenguinCatcherUser>();
            builder.Entity<PenguinCatcherUser>().HasData(
                new PenguinCatcherUser { Id = "1", UserName = "PenguinWrangler", PasswordHash = passwordHasher.HashPassword(null, "P@ssw0rd123") },
                new PenguinCatcherUser { Id = "2", UserName = "PenguinEnjoyer", PasswordHash = passwordHasher.HashPassword(null, "MyB1g$ecret") }
                );


            builder.Entity<Distribution>().HasData(
                new Distribution { ReleaseCycle = "Point Release", Developer = "Debian Project", DistributionID = 1, DistroName = "Debian", DistroURL = "https://www.debian.org" },
                new Distribution { ReleaseCycle = "Rolling Release", Developer = "Allan McRae, Anatol Pomozov, Andreas Radke, et al.", DistributionID = 2, DistroName = "Arch", DistroURL = "https://archlinux.org" },
                new Distribution { ReleaseCycle = "Point Release", Developer = "BBC & Canonical", DistributionID = 3, DistroName = "Ubuntu", DistroURL = "https://ubuntu.com" }
                );

            builder.Entity<Post>().HasData(
                new Post
                    {
                        PostID = 1,
                        Content = "Arch Linux is a rolling realese Linux distribution."
                        + " Arch provides a minimal environment to allow the user upmost control over the programs installed on their hardware."
                        + " Furthermore, the Arch repositories provide extremely up to date software, yet this can sometimes lead to instability"
                        + " with a user's system if they are not vigilant during updates and when installing software. In addition to the official"
                        + " Arch repositories, there exists the Arch user repository (AUR) which can provide a wider variety of software and potentially"
                        + " even more up to date software than the official repository. This too carries the risk of versioning conflicts, breaking changes"
                        + " between updates, and possibly unmaintained software. All this to say, for the vigilant and ready to maintain their operating systems,"
                        + " Arch Linux can be an excellent daily driver.",
                        DistributionID = 2,
                        DatePosted = DateTime.Now,
                        UserId = "1",
                    }
                );

            builder.Entity<Comment>().HasData(
                new Comment { CommentId = 1, Content = "Wow, I didn't know that Arch Linux existed. Thanks for sharing!", PostID = 1, UserId = "2" }
                );

            builder.Entity<Like>().HasData(
                new Like { LikeID = 1, PostID = 1, UserId = "2" }
                );

            base.OnModelCreating(builder);
        }
    }
}
