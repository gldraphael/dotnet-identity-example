using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Web.Models
{
    public class ApplicationUser : IdentityUser
	{
		[Required]
		public string FullName { get; set; }
	}
}
