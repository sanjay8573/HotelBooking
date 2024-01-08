using HotelBooking.Model;
using System.Data.Entity;

namespace HotelBooking.Context
{
    public class DeptContext :DbContext
    {
        public DeptContext(): base("Name=HBConstring")
        {
        }

        public DbSet<Dept> Dept
        {
            get;
            set;
        }
        public DbSet<Designation> Designation
        {
            get;
            set;
        }
        public DbSet<Team> Team
        {
            get;
            set;
        }
    }
}
