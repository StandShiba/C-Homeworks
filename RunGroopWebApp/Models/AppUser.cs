using Microsoft.AspNetCore.Identity;

namespace RunGroopWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public int? Pace {  get; set; }
        public Address Address { get; set; }

    }
}
