using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Web.Models;

namespace Web
{
    public class ApplicationDbContext  : IdentityDbContext<ApplicationUser>
    {
        
    }
}