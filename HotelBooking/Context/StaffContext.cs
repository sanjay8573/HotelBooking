using HotelBooking.Model;
using System.Data.Entity;
using System.Data.SqlClient;
namespace HotelBooking.Context
{
    public class StaffContext : DbContext
    {
        public StaffContext(): base("Name=HBConstring")
        {


        }
        public DbSet<Staff> Staff
        {
            get;
            set;
        }
        public DbSet<Designation> Designation
        {
            get;
            set;
        }

        public DbSet<Branch> Branch
        {
            get;
            set;
        }

        public DbSet<StaffLogin> StaffLogin
        {
            get;
            set;
        }

        public DbSet<StaffTier> StaffTier
        {
            get;
            set;
        }

    }
 }
