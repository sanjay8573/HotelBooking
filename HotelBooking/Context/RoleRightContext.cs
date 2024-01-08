using HotelBooking.Model;
using System.Data.Entity;

namespace HotelBooking.Context
{
    public class RoleRightContext : DbContext
    {
        public RoleRightContext() : base("Name=HBConstring")
        {
        }

        public DbSet<ModuleMaster> ModuleMaster
        {
            get;
            set;
        }
        public DbSet<Rights> Rights
        {
            get;
            set;
        }

        public DbSet<RoleMaster> Roles
        {
            get;
            set;
        }


    }
}
