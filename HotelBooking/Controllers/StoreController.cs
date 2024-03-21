using HotelBooking.Model;
using HotelBooking.Model.Inventory;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    public class StoreController : ApiController
    {
        private readonly IItemMaster _items;
        private readonly IIssueRegister _ISitems;

        public StoreController(IItemMaster items,IIssueRegister ir) {
            _items = items;
            _ISitems = ir;
        }
        public StoreController()
        {
            _items = new ItemRepository();
            _ISitems = new IssueRepository();
        }
        [Route("api/ManageStore/GetItems/{BranchId}")]
        [HttpGet]
        public IEnumerable<ItemMaster> GetItems(int BranchId)
        {
            return _items.GetItems(BranchId);
        }
        [Route("api/ManageStore/AddItems")]
        [HttpGet]
        public bool AddItems(IEnumerable<ItemMaster> items)
        {
            return _items.AddItems(items);
        }
        [Route("api/ManageStore/AddRDItems")]
        [HttpGet]
        public bool AddRDItems(IEnumerable<IssueRegister> items)
        {
            return _ISitems.AddEntries(items);
        }
        [Route("api/ManageStore/GetIssuedItems")]
        [HttpGet]
        public IEnumerable<IssueRegister> GetIssuedItems(int BranchId)
        {
            return _ISitems.GetIssuedItems(BranchId);
        }

    }
}
