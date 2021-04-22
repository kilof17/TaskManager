using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class InitializeDb
    {
        private const string AdminEmail = "task.manager@interia.pl";
        private const string AdminPassword = "$uper$ecretPa$$word123!";
        private const string AdminLogin = "kilof17";

        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole {Name = "Admin", NormalizedName = "ADMIN"},
            new IdentityRole {Name = "User", NormalizedName = "USER"}
        };

        public static void SeedDatabase(TaskManagerDbContext context,
                                        UserManager<ApplicationUser> userManager,
                                        RoleManager<IdentityRole> roleManager)
        {
            context.Database.Migrate();

            List<Task> tasks = new List<Task>
            {
                Task.Run(() => AddUserAsync(userManager)),
                Task.Run(() => AddRolesAsync(roleManager))
            };

            Task allTasks = Task.WhenAll(tasks);
            allTasks.Wait();
            AddUserRoleAsync(userManager).Wait();
        }

        private static async Task AddRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Roles)
            {
                var exsist = await roleManager.RoleExistsAsync(role.Name);

                if (!exsist)
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }

        private static async Task AddUserAsync(UserManager<ApplicationUser> userManager)
        {
            var admin = await userManager.FindByNameAsync(AdminLogin);
            if (admin == null)
            {
                var user = new ApplicationUser
                {
                    UserName = AdminLogin,
                    Email = AdminEmail,
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(user, AdminPassword);
            }
        }

        private static async Task AddUserRoleAsync(UserManager<ApplicationUser> userManager)
        {
            var user = userManager.FindByEmailAsync(AdminEmail).GetAwaiter().GetResult();

            foreach (var role in Roles)
            {
                var inRole = await userManager.IsInRoleAsync(user, role.Name);

                if (!inRole)
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }
            }
        }
    }
}