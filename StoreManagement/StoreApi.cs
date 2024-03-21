using StoreManagement.Models;
using StoreManagement.Repository.Interface;


namespace StoreManagement
{
    public class StoreApi : IStore
    {
        private readonly IItemMaster _items;
        //private readonly IItemMaster _items;

        public StoreApi(IItemMaster items) {
            _items = items;
        }
        public IEnumerable<ItemMaster> GetItems()
        {
           return _items.GetItems();
            
        }
    }
}
