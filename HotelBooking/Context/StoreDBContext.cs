using HotelBooking.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelBooking.Context
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext() : base("Name=HBConstring")
        {


        }
        public DbSet<ItemMaster> ItemMaster
        {
            get;
            set;
        }
        public DbSet<IssueRegister> IssueRegister
        {
            get;
            set;
        }
    }
}