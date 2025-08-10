
using Ecommerce.domain.constants;
using Ecommerce.infrastructure.Identity;
using Ecommerce.infrastructure.percistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eCommerce_dpei.infrastructure.percistence
{
    public class DBSeeder
    {
        public static async Task SeedDataAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplcationDbContext>();

            var logeer = scope.ServiceProvider.GetRequiredService<ILogger<DBSeeder>>();
            if (dbContext.Database.GetPendingMigrations().Count() > 0)
            {
                dbContext.Database.Migrate();
            }

            try
            {
                var usermanger = scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();
                var RoleManger = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();
                if (!usermanger.Users.Any())
                {
                    var user = new ApplicationUser
                    {
                        FirstName = "osama",
                        LastName = "ahmed",
                        UserName = "admin5016@gmail.com",
                        Email = "admin5016@gmail.com",
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                    };
                    if (!await RoleManger.RoleExistsAsync(Roles.Admin))
                    {
                        var roleresult = await RoleManger.CreateAsync(new
                            IdentityRole(Roles.Admin));
                        if (roleresult.Succeeded == false)
                        {
                            var roleerrors = roleresult.Errors.Select(e =>
                            e.Description);
                            logeer.LogError($"Failed to create admin role. Errors : {string.Join(",", roleerrors)}");

                            return;
                        }
                        logeer.LogInformation("Admin Role is Created");
                    }
                    var CreatedUserResult = await usermanger.
                    CreateAsync(user: user, password: "Admin123@");

                    if (CreatedUserResult.Succeeded == false)
                    {
                        var errors = CreatedUserResult.Errors.Select(e => e.Description);
                        logeer.LogError(
                            $"Failed to Create Admin User. Errors : {string.Join(",", errors)}"
                            );
                        return;
                    }
                    var addUserToRoleAsync = await usermanger
                        .AddToRoleAsync(user: user, role: Roles.Admin);
                    if (addUserToRoleAsync.Succeeded == false)
                    {
                        var errors = addUserToRoleAsync.Errors.Select(e => e.Description);
                        logeer.LogError(
                            $"Failed to Add Admin Role To User. Errors : {string.Join(",", errors)}"
                            );
                        return;
                    }
                    logeer.LogInformation("Admin User Is Created");
                }
            }
            catch (Exception ex)
            {
                logeer.LogError(ex.Message);
            }
        }
    }
}
