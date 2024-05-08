using Microsoft.AspNetCore.Identity;
using PenguinCatcher.Models.IdentityModels;

namespace PenguinCatcher.Models
{
    public class ConfigureIdentity
    {
        public static async Task CreateAdminUserAsync(IServiceProvider provider)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = provider.GetRequiredService<UserManager<PenguinCatcherUser>>();

            //string username = "PenguinWrangler";
            //string password = "P@ssw0rd123";
            //string rolename = "Admin";

            //string normalUsername = "PenguinEnjoyer";
            //string normalPassword = "MyB1g$ecret";
            //string normalRolename = "Catcher";

            //if (await roleManager.FindByNameAsync(rolename) == null)
            //    await roleManager.CreateAsync(new IdentityRole(rolename));

            //if (await roleManager.FindByNameAsync(normalRolename) == null)
            //    await roleManager.CreateAsync(new IdentityRole(normalRolename));

            //if (await userManager.FindByNameAsync(username) == null)
            //{
            //    PenguinCatcherUser newUser = new PenguinCatcherUser
            //    {
            //        UserName = username
            //    };

            //    var result = await userManager.CreateAsync(newUser, password);

            //    if (result.Succeeded)
            //    {
            //        await userManager.AddToRoleAsync(newUser, rolename);
            //    }
            //}

            //if (await userManager.FindByNameAsync(normalUsername) == null)
            //{
            //    PenguinCatcherUser newUser = new PenguinCatcherUser
            //    {
            //        UserName = normalUsername,
            //    };

            //    var result = await userManager.CreateAsync(newUser, normalPassword);

            //    if (result.Succeeded)
            //    {
            //        await userManager.AddToRoleAsync(newUser, normalRolename);
            //    }
            //}

            string adminRole = "Admin";
            string userRole = "Catcher";

            if (await userManager.FindByNameAsync("PenguinWrangler") == null)
                Console.WriteLine("It couldn't find the wrangler");

            if (await roleManager.FindByNameAsync(adminRole) == null)
                await roleManager.CreateAsync(new IdentityRole(adminRole));

            if (await roleManager.FindByNameAsync(userRole) == null)
                await roleManager.CreateAsync(new IdentityRole(userRole));

            if (await userManager.FindByIdAsync("1") != null)
            {
                var adminUser = await userManager.FindByIdAsync("1");

                await userManager.UpdateSecurityStampAsync(adminUser);
                await userManager.UpdateNormalizedUserNameAsync(adminUser);
                await userManager.AddToRoleAsync(adminUser, adminRole);

                Console.WriteLine("Updated Admin User");
            }

            var normalUser = await userManager.FindByIdAsync("2");

            if (await userManager.FindByIdAsync("2") != null)
            {
                await userManager.UpdateSecurityStampAsync(normalUser);
                await userManager.UpdateNormalizedUserNameAsync(normalUser);
                await userManager.AddToRoleAsync(normalUser, userRole);

                Console.WriteLine("Updated Normal User");
            }

            if (await userManager.FindByNameAsync("PenguinWrangler") == null)
                Console.WriteLine("It couldn't find the wrangler");
            else
                Console.WriteLine("It could now find the wrangler!");
        }
    }
}
