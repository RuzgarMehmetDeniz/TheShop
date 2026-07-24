using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheShop.Identity.Entities;

namespace TheShop.Identity.Context
{
    public class IdentityContext:IdentityDbContext<AppUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext>options) : base(options)
        {
        }
    }
}
