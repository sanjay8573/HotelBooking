using StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement
{
    public interface IStore
    {
        IEnumerable<ItemMaster> GetItems();
        
    }
}
