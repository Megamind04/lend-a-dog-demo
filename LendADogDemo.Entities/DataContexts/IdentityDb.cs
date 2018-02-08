using LendADogDemo.Entities.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LendADogDemo.Entities.DataContexts
{
    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public static IdentityDb Create()
        {
            return new IdentityDb();
        }
    }
}
