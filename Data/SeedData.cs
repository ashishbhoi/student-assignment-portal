using Microsoft.AspNetCore.Identity;
using StudentClassworkPortal.Areas.Identity.Data;

namespace StudentClassworkPortal.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Teacher", "Student" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var teacherUsername = "teacheradmin";
            var teacherPassword = "P@ssw0rd";

            if (await userManager.FindByNameAsync(teacherUsername) == null)
            {
                var teacher = new ApplicationUser
                {
                    UserName = teacherUsername,
                    Email = "teacheradmin@example.com", // Email is still required
                    FirstName = "Teacher",
                    LastName = "Admin",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(teacher, teacherPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(teacher, "Teacher");
                }
            }
        }
    }
}
