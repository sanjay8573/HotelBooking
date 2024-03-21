using HotelBooking.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface IItemMaster
    {
        IEnumerable<ItemMaster> GetItems(int branchId);
        bool AddItem(ItemMaster item);
        bool RemoveItem(int itemId);
        bool AddItems(IEnumerable<ItemMaster> items);

    }
}
