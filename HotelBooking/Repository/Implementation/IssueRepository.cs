using HotelBooking.Context;
using HotelBooking.Model.Inventory;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class IssueRepository : IIssueRegister
    {
        private readonly StoreDBContext _contex;

        public IssueRepository(StoreDBContext contex)
        {
            _contex = contex;
        }
        public IssueRepository()
        {
            _contex = new StoreDBContext();
        }
        public bool AddEntry(IssueRegister item)
        {
            bool rtnVal = false;
            try
            {
                if (item != null)
                {
                    if (item.IssueRegisterId > 0)//udpate
                    {
                        IssueRegister tmpItem = _contex.IssueRegister.Find(item.IssueRegisterId);
                        if (tmpItem != null)
                        {
                            tmpItem.OpeningQuantity = item.OpeningQuantity;
                            tmpItem.IssueQuantity = item.IssueQuantity;
                            tmpItem.BalanceQuantity = item.BalanceQuantity;
                            tmpItem.Action = item.Action;
                            tmpItem.IssueDate = item.IssueDate;
                            tmpItem.isActive = item.isActive;


                        }
                    }
                    else//add
                    {
                        _contex.IssueRegister.Add(item);
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

        public IEnumerable<IssueRegister> GetIssuedItems(int branchId)
        {
            return _contex.IssueRegister.Where(i => i.BranchId == branchId).ToArray();
        }

        public bool RemoveEntry(int issueId)
        {
            bool rtnVal = false;
            try
            {
               
                    if (issueId > 0)
                    {
                        IssueRegister tmpItem = _contex.IssueRegister.Find(issueId);
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
        public bool AddEntries(IEnumerable<IssueRegister> items)
        {
            bool rtnVal = false;
            try
                {
                    foreach(var it in items)
                    {
                        it.IssueDate = DateTime.Now;
                    }
                    _contex.IssueRegister.AddRange(items);
                    foreach (var itm in items)
                    {
                        ItemMaster it = _contex.ItemMaster.Find(itm.ItemId);
                        it.QuantityAvailable = itm.BalanceQuantity;
                    }
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