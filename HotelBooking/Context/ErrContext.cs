using HotelBooking.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelBooking.Context
{
    public class ErrContext : DbContext
    {
        public ErrContext() : base("Name=HBConstring")
        {


        }
        public DbSet<AppLogs> AppLogs
        {
            get;
            set;
        }
    }
}