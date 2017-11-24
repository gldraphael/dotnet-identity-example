using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Route("dev/seed")]
    public class SeedController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public SeedController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
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
                if(!await _roleManager.RoleExistsAsync(roleName: identityRole.Name))
                {
                    var result = await _roleManager.CreateAsync(identityRole);

                    // Return 500 if it fails
                    if(!result.Succeeded)
                        return StatusCode(StatusCodes.Status500InternalServerError);
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
            if(await _userManager.FindByEmailAsync(user.Email) == null)
            {
                // WARNING: Do NOT check in credentials of any kind into source control
                var result = await _userManager.CreateAsync(user, password: "5ESTdYB5cyYwA2dKhJqyjPYnKUc&45Ydw^gz^jy&FCV3gxpmDPdaDmxpMkhpp&9TRadU%wQ2TUge!TsYXsh77Qmauan3PEG8!6EP");

                if(!result.Succeeded) // Return 500 if it fails
                    return StatusCode(StatusCodes.Status500InternalServerError);

                // Assign all roles to the default user
                result = await _userManager.AddToRolesAsync(user, roles.Select(r => r.GetRoleName()));
                // If you add a role to the enumafter the user is created,
                // the role will not be assigned to the user as of now

                if(!result.Succeeded) // Return 500 if it fails
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // All good, 200 OK!            
            return Ok();
        }
    }
}
