using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Build the application host
            var host = BuildWebHost(args);

            // Seed the database
            // TODO: Refactor this
            using(var scope = host.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                // Get the list of the roles in the enum
                Role[] roles = (Role[])Enum.GetValues(typeof(Role));
                foreach(var r in roles)
                {
                    // Create an identity role object out of the enum value
                    var identityRole = new IdentityRole {
                        Id = r.GetRoleName(),
                        Name = r.GetRoleName()
                    };

                    // Create the role if it doesn't already exist
                    if(!await roleManager.RoleExistsAsync(roleName: identityRole.Name))
                    {
                        var result = await roleManager.CreateAsync(identityRole);
                        if(!result.Succeeded)
                        {
                            // FIXME: Do not throw an Exception object
                            throw new Exception("Creating role failed");
                        }
                    }
                }

                // Our default user
                ApplicationUser user = new ApplicationUser {
                    FullName = "Jane Doe",
                    Email = "janedoe@example.com",
                    UserName = "janedoe@example.com",
                    LockoutEnabled = false
                };

                // Add the user to the database if it doesn't already exist
                if(await userManager.FindByEmailAsync(user.Email) == null)
                {
                    // WARNING: Do NOT check in credentials of any kind into source control
                    var result = await userManager.CreateAsync(user, password: "5ESTdYB5cyYwA2dKhJqyjPYnKUc&45Ydw^gz^jy&FCV3gxpmDPdaDmxpMkhpp&9TRadU%wQ2TUge!TsYXsh77Qmauan3PEG8!6EP");

                    if(!result.Succeeded)
                    {
                        // FIXME: Do not throw an Exception object
                        throw new Exception("Creating user failed");
                    }
                    
                    // Assign all roles to the default user
                    result = await userManager.AddToRolesAsync(user, roles.Select(r => r.GetRoleName()));
                    // If you add a role to the enumafter the user is created,
                    // the role will not be assigned to the user as of now

                    if(!result.Succeeded)
                    {
                        // FIXME: Do not throw an Exception object
                        throw new Exception("Adding user to role failed");
                    }
                }
            }

            // Run the application
            host.Run();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
