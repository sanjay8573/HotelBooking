using HotelBooking.Context;
using HotelBooking.Model.Inventory;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class ItemRepository  : IItemMaster
    {
        private readonly StoreDBContext _contex;

        public ItemRepository(StoreDBContext contex)
        {
            _contex = contex;
        }
        public ItemRepository()
        {
            _contex = new StoreDBContext();
        }

        public bool AddItem(ItemMaster item)
        {
            bool rtnVal = false;
            try
            {
                if(item != null) {
                    if (item.ItemId > 0)//udpate
                    {
                        ItemMaster tmpItem = _contex.ItemMaster.Find(item.ItemId);
                        if (tmpItem != null)
                        {
                            tmpItem.ItemName = item.ItemName;
                            tmpItem.ItemDescription = item.ItemDescription;
                            tmpItem.Quantity = item.Quantity;
                            tmpItem.QuantityAvailable = item.QuantityAvailable;
                            tmpItem.ReorderPoint = item.ReorderPoint;
                            tmpItem.isActive = item.isActive;


                        }
                    }
                    else//add
                    {
                        _contex.ItemMaster.Add(item);
                    }
                    _contex.SaveChanges();
                    rtnVal = true;

                }
            }
            catch (Exception)
            {

                rtnVal = false;
            }
            return rtnVal;
        }

        public IEnumerable<ItemMaster> GetItems(int branchId)
        {
            IEnumerable<ItemMaster> items = _contex.ItemMaster.Where(b=>b.BranchId== branchId).ToArray();
            return items;

        }

        

        public bool RemoveItem(int itemId)
        {
            bool rtnVal = false;
            try
            {
                if (itemId > 0)//udpate
                {
                    ItemMaster tmpItem = _contex.ItemMaster.Find(itemId);
                    if (tmpItem != null)
                    {
                        tmpItem.isDeleted = true;

                        _contex.SaveChanges();
                        rtnVal = true;
                    }
                }
            }
            catch (Exception)
            {

                rtnVal = false;
            }
            return rtnVal;
        }
        public bool AddItems(IEnumerable<ItemMaster> items)
        {
            bool rtnVal = false;
            try
            {
                foreach(var it in items)
                {
                    it.date_created = DateTime.Now;
                }
                _contex.ItemMaster.AddRange(items);
                _contex.SaveChanges();
                rtnVal = true;

            }
            catch (Exception)
            {

                rtnVal = false;
            }
            return rtnVal;
        }
    }
}