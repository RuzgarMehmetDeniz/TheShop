using Microsoft.AspNetCore.Identity;

namespace TheShop.Identity.Entities
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
