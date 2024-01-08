
using HotelBooking.Model;
using System.Data.Entity;

namespace HotelBooking.Context
{
    public class CompanyContext : DbContext
    {
        public CompanyContext() : base("Name=HBConstring")
        {
           

        }
        
        public DbSet<Company> Company
        {
            get;
            set;
        }
        public DbSet<RegionalSettings> RegionalSettings
        {
            get;
            set;
        }

        public DbSet<TNC> TNC
        {
            get;
            set;
        }
        public DbSet<Branch> Branch
        {
            get;
            set;
        }
        public DbSet<Amenities> Amenities
        {
            get;
            set;
        }
        public DbSet<RoomType> RoomType
        {
            get;
            set;
        }
        public DbSet<Floor> Floors
        {
            get;
            set;
        }
        public DbSet<PriceManager> PriceManager
        {
            get;
            set;
        }
        public DbSet<RoomeTypeImages> RoomeTYpeImages
        {
            get;
            set;
        }
        public DbSet<Room> Rooms
        {
            get;
            set;
        }
        public DbSet<PriceType> PriceType
        {
            get;
            set;
        }
        public DbSet<PaidServices> PaidServices
        {
            get;
            set;
        }

        public DbSet<Coupon> Coupon
        {
            get;
            set;
        }

        public DbSet<Booking> Booking
        {
            get;
            set;
        }
        public DbSet<BookingCost> BookingCost
        {
            get;
            set;
        }
        public DbSet<SPM> SpecialPrice
        {
            get;
            set;
        }

        public DbSet<BookedRoom> BookedRoom
        {
            get;
            set;
        }


    }
}
