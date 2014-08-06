using System.Threading.Tasks;
using TenantURL.Models.Entity;
using System.Data.Entity;

namespace TenantURL.Models
{
    public class DefaultConnection : DbContext
    {

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<TenantControl> TenantControl { get; set; }
    }
}