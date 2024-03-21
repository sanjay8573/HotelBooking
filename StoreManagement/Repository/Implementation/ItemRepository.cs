using StoreManagement.Models;
using StoreManagement.Repository.Data;
using StoreManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Repository.Implementation
{
    public class ItemRepository : IItemMaster
    {
        private readonly StoreDBContext _contex;

        public ItemRepository(StoreDBContext contex)
        {
            _contex = contex;
        }
        public IEnumerable<ItemMaster> GetItems(){ 
             IEnumerable<ItemMaster>  items = _contex.ItemMaster.ToArray();
            return items;

        }

    };
    
}
