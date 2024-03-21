using StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace StoreManagement.Repository.Data
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
