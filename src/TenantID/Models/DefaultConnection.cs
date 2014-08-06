using System.Threading.Tasks;
using TenantID.Models.Entity;
using System.Data.Entity;

namespace TenantID.Models
{
    public class DefaultConnection : DbContext
    {

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<TenantControl> TenantControl { get; set; }
    }
}