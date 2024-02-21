using HotelBooking.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelBooking.Context
{
    public class BookingContext : DbContext
    {
        public BookingContext() : base("Name=HBConstring") { }

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
        public DbSet<BookedRoom> BookedRoom
        {
            get;
            set;
        }
    }
}