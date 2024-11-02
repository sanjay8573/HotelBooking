using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Model.Reatraurant;
using HotelBooking.Repository.Interface;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Xml;

namespace HotelBooking.Repository.Implementation
{
    public class RestaurantRepository : IRestaurant
    {
        private readonly CompanyContext _context;

        public RestaurantRepository()
        {
            _context = new CompanyContext();

        }
        public IEnumerable<RestaurantModel> GetAllRestaurant(int BranchId)
        {
            return _context.Restaurant.Where(b=>b.BranchId == BranchId).ToArray();
        }

        public IEnumerable<RestaurantTables> GetAllRestaurantTables(int RestaurantId)
        {
            return _context.RestaurantTables.Where(b=>b.RestaurantId == RestaurantId).ToArray();
        }

        public IEnumerable<RestaurantRoomService> GetAllRestaurantRoomServices(int RestaurantId,int branchId)
        {
            IEnumerable<RestaurantRoomService> rrs= _context.RestaurantRoomService.Where(b => b.RestaurantId == RestaurantId).ToArray();
            if (rrs.Count() <=0)
            {
                IEnumerable<BookedRoom> bkRooms = _context.BookedRoom.Where(b => b.isCheckout == false && b.BranchId == branchId).ToArray();
                foreach(var bkr in bkRooms)
                {
                    RestaurantRoomService RRR= new RestaurantRoomService
                    {
                        BranchId = bkr.BranchId,
                        RestaurantId= RestaurantId,
                        RoomId=bkr.RoomId,
                        isOrdered=false,
                        RoomNumber=bkr.RoomNumber,
                        isActive=true,
                        isDeleted=false
                    };
                    _context.RestaurantRoomService.Add(RRR);
                    _context.SaveChanges();
                }
            }
            return _context.RestaurantRoomService.Where(b => b.RestaurantId == RestaurantId).ToArray();
        }
        public IEnumerable<ImageMaster> GetAllRestaurantImages(int RestaurantId)
        {
            return _context.ImageMaster.Where(b => b.RefId == RestaurantId && b.ImageTypeId==2).ToArray();
        }

        public RestaurantModel GetRestaurant(int BranchId, int RestaurantId)
        {
            return _context.Restaurant.Where(b => b.BranchId == BranchId && b.RestaurantId == RestaurantId).SingleOrDefault();
        }

        public bool SaveRestaurant(RestaurantModel RestaurantEntity)
        {
            bool rtnVal = false;
            try
            {
                RestaurantModel tmptbl = _context.Restaurant.Find(RestaurantEntity.RestaurantId);
                if (tmptbl != null)
                {
                    tmptbl.Name=RestaurantEntity.Name;
                    tmptbl.Area=RestaurantEntity.Area;
                    tmptbl.NoOfTable=RestaurantEntity.NoOfTable;
                    tmptbl.isBarAvailable = RestaurantEntity.isBarAvailable;
                    tmptbl.isBreakfastServe = RestaurantEntity.isBreakfastServe;
                    tmptbl.FloorId = RestaurantEntity.FloorId;
                    tmptbl.FloorName = RestaurantEntity.FloorName;
                    tmptbl.OpeningTime = RestaurantEntity.OpeningTime;
                    tmptbl.ClosingTime = RestaurantEntity.ClosingTime;
                    tmptbl.lastOrderAcceptedTill = RestaurantEntity.lastOrderAcceptedTill;
                }
                else
                {
                    _context.Restaurant.Add(RestaurantEntity);
                }
                
                _context.SaveChanges();



                int RestroId = RestaurantEntity.RestaurantId;
                if (RestaurantEntity.AvailableTables.Count > 0)
                {
                    _context.Database.ExecuteSqlCommand("Delete from RestaurantTables where  RestaurantId=" + RestroId);

                    foreach (var restaurantTables in RestaurantEntity.AvailableTables)
                    {
                        restaurantTables.RestaurantId = RestroId;
                        rtnVal = insertUpdateTables(restaurantTables);
                    }
                }
                
                if (RestaurantEntity.ImageData.Count > 0)
                {
                    foreach (var rimage in RestaurantEntity.ImageData)
                    {
                        rimage.RefId = RestroId;
                        rtnVal = insertImages(rimage);
                    }
                }

                rtnVal = true;
            }
            catch (Exception)
            {

                rtnVal = false;
            }


            return rtnVal;
        }

        public bool SaveRestaurantTables(IEnumerable<RestaurantTables> resTableEntity)
        {
            bool rtnVal = false;
            try
            {

                foreach (RestaurantTables tbl in resTableEntity)
                {
                    insertUpdateTables(tbl);
                }
                rtnVal = true;
            }
            catch (Exception)
            {

                rtnVal = false;
            }


            return rtnVal;
        }
        private bool insertUpdateTables(RestaurantTables tblEntity)
        {
            bool rtnVal = false;
            try
            {
                RestaurantTables tmptbl=_context.RestaurantTables.Find(tblEntity.TableId);
                if(tmptbl!= null)
                {
                    tmptbl.NoOfSeat = tblEntity.NoOfSeat;
                    tmptbl.isActive = tblEntity.isActive;

                }
                else
                {
                    _context.RestaurantTables.Add(tblEntity);
                }
               
                _context.SaveChanges();
                 rtnVal = true;
            }
            catch (Exception)
            {

                rtnVal = false;
            }


            return rtnVal;
        }
        
         private bool insertImages(ImageMaster imgEntity)
        {
            bool rtnVal = false;
            try
            {
                ImageMaster tmptbl = _context.ImageMaster.Find(imgEntity.ImageId);
                if (tmptbl != null)
                {
                    tmptbl.ImageData = imgEntity.ImageData;
                    tmptbl.isActive = imgEntity.isActive;

                }
                else
                {
                    _context.ImageMaster.Add(imgEntity);
                }

                _context.SaveChanges();
                rtnVal = true;
            }
            catch (Exception)
            {

                rtnVal = false;
            }


            return rtnVal;
        }

        public bool SaveRestaurantMenu(RestaurantMenu menuEntity)
        {
            bool rtnVal = false;
            try
            {
                RestaurantMenu RM = _context.RestaurantMenu.Find(menuEntity.RestaurantMenuId);
                if (RM != null)
                {
                    RM.RestaurantMenuName=menuEntity.RestaurantMenuName;
                    
                }
                else
                {
                    _context.RestaurantMenu.Add(menuEntity);
                }
                
                _context.SaveChanges();
                int MenuId = menuEntity.RestaurantMenuId;
                if (menuEntity.MenuHeading.Count > 0)
                {
                    foreach(var hItem in menuEntity.MenuHeading)
                    {
                        hItem.RestaurantMenuId = MenuId;
                        SaveRestaurantMenuheading(hItem);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return rtnVal;
        }
        private bool SaveRestaurantMenuheading(RestaurantMenuHeading menuHeadingEntity)
        {
            bool rtnVal = false;
            try
            {
                RestaurantMenuHeading RMH = _context.RestaurantMenuHeading.Find(menuHeadingEntity.MenuHeadingId); 
                if (RMH != null) {
                    RMH.MenuHeadingName = menuHeadingEntity.MenuHeadingName;
                    RMH.TaxSlabId= menuHeadingEntity.TaxSlabId;
                }
                else
                {
                    _context.RestaurantMenuHeading.Add(menuHeadingEntity);
                }
                   
                    _context.SaveChanges();
                    int headingid= menuHeadingEntity.MenuHeadingId;
                    foreach(var m in menuHeadingEntity.MenuItems)
                    {
                        m.MenuHeadingId = headingid;
                        SaveRestaurantMenuIteams(m);
                    }
                   

            }
            catch (Exception)
            {

                throw;
            }

            return rtnVal;
        }
        private bool SaveRestaurantMenuIteams(RestaurantMenuItem menuItemEntity)
        {
            bool rtnVal = false;
            try
            {
                RestaurantMenuItem RMI = _context.RestaurantMenuItem.Find(menuItemEntity.MenuItemId);
                if (RMI != null)
                {
                    RMI.MenuItemName= menuItemEntity.MenuItemName;
                    RMI.ItemPrice= menuItemEntity.ItemPrice;
                    RMI.isActive = menuItemEntity.isActive;
                }
                else
                {
                    _context.RestaurantMenuItem.Add(menuItemEntity);
                }
                
                _context.SaveChanges();
                

            }
            catch (Exception)
            {

                throw;
            }

            return rtnVal;
        }

        public IEnumerable<RestaurantMenu> GetAllRestaurantMenu(int RestaurantId)
        {
            IEnumerable<RestaurantMenu> mlist= _context.RestaurantMenu.Where(m => m.RestaurantId == RestaurantId).ToArray();
            List<RestaurantMenu> mnList = new List<RestaurantMenu>();
          foreach(var mItem in mlist)
            {
                mnList.Add(GetRestaurantMenu(mItem.RestaurantMenuId));
            }
            return mnList.ToArray();
        }
        public RestaurantMenu GetRestaurantMenu(int MenuId)
        {
            RestaurantMenu rm = new RestaurantMenu();
            rm= _context.RestaurantMenu.Where(m => m.RestaurantMenuId == MenuId).SingleOrDefault();
            List<RestaurantMenuHeading> rmh = new List<RestaurantMenuHeading>();
            rmh = _context.RestaurantMenuHeading.Where(h => h.RestaurantMenuId == rm.RestaurantMenuId).ToList();
            foreach (var heading in rmh)
            {
                List<RestaurantMenuItem> menuItemList = _context.RestaurantMenuItem.Where(mn => mn.MenuHeadingId == heading.MenuHeadingId).ToList();
                double taxp = _context.TaxMaster.Where(m => m.TaxId == heading.TaxSlabId).SingleOrDefault().Value;
                heading.MenuItems= menuItemList;
                heading.TaxPercentage= taxp;
            }
            rm.MenuHeading = rmh;
            return rm;
        }

        public IEnumerable<BillingDetails> getParkItems(int RetaurantId, int tableid,bool isRMS=false)
        {
            BillingMaster BM = new BillingMaster();
            if (isRMS)
            {
                BM = _context.BillingMaster.Where(b => b.RestaurantId == RetaurantId && b.TableNo_RoomNumber == tableid && b.isPark == true).SingleOrDefault();
            }
            else
            {
                BM = _context.BillingMaster.Where(b => b.RestaurantId == RetaurantId && b.Tableid == tableid && b.isPark == true).SingleOrDefault();
            }
             
            IEnumerable<BillingDetails> BD = new List<BillingDetails>();
            if (BM != null)
            {
                BD = _context.BillingDetails.Where(b => b.BillingMasterId == BM.BillingId).ToArray();
            }
            
                
            return BD;
        }
        public bool ReleaseTable(int restaurantId,int tableId)
        {
            bool rtnVal = false;
            try
            {

                BillingMaster BM = _context.BillingMaster.Where(b => b.RestaurantId == restaurantId && b.Tableid == tableId).SingleOrDefault();
                if (BM != null) {
                    if(BM.TableNo_RoomNumber>0)
                    {
                        var bk = _context.BookedRoom.Where(b => b.RoomNumber == BM.TableNo_RoomNumber.ToString() && b.isCheckout == false).FirstOrDefault();
                        if (bk != null)
                        {
                            new BookingRepository().UpdateBookingCost(bk.BookingId, decimal.Parse(BM.TotalAmount.ToString()), decimal.Parse(BM.TaxAmount.ToString()),decimal.Parse(BM.GrantTotal.ToString()));
                        }
                    }
                    BM.isPark = false;
                }
              
                RestaurantTables restaurantTables = _context.RestaurantTables.Where(b => b.RestaurantId == restaurantId && b.TableId == tableId).SingleOrDefault();
                restaurantTables.isOccupied = false;
                _context.SaveChanges();
                rtnVal = true;
            }
            catch (Exception)
            {

                rtnVal = false;
            }


            return rtnVal;
        }
        public IEnumerable<BillingMaster> getCompletedOrders(int RetaurantId)
        {
            return _context.BillingMaster.Where(r => r.RestaurantId == RetaurantId && r.isPark == false).ToArray();
        }
        public BillingMaster GetdOrder(int OrderId)
        {
            BillingMaster BM= _context.BillingMaster.Where(o=>o.BillingId==OrderId).SingleOrDefault();
            BM.BillingDetails=_context.BillingDetails.Where(b=>b.BillingMasterId==OrderId).ToList();
            return BM;
        }
        public IEnumerable<VM_MenuHeadings> GetMenuHeadings(int BranchId)
        {
            IEnumerable<VM_MenuHeadings> vM_MenuHeadings = new List<VM_MenuHeadings>();
            vM_MenuHeadings = from R in _context.Restaurant.Where(r => r.BranchId == BranchId).ToArray()
                              join RM in _context.RestaurantMenu.ToArray()
                              on R.RestaurantId equals RM.RestaurantId
                              join RMH in _context.RestaurantMenuHeading.ToArray()
                              on RM.RestaurantMenuId equals RMH.RestaurantMenuId
                              
                              select new VM_MenuHeadings
                              {
                                  RestaurantId = R.RestaurantId,
                                  RestaurantMenuId = RM.RestaurantMenuId,
                                  MenuHeadingId = RMH.MenuHeadingId,
                                  MenuHeadingName= RMH.MenuHeadingName,
                                  
                              };
            return vM_MenuHeadings;

        }
        public IEnumerable<RestaurantMenuItem> GetRestaurantMenuItems(int menuHeadingid)
        {
            return _context.RestaurantMenuItem.Where(m => m.MenuHeadingId == menuHeadingid).ToArray();
        }
        public bool SaveBuffetMenu(BuffetMenuMaster BuffetMenuMEntity)
        {
            bool rtnval = false;
            var rm = _context.restaurantBuffetMenu.Find(BuffetMenuMEntity.BuffetMenu.RestaurantMenuId);
            if (rm != null)
            {
                rm.RestaurantMenuName = BuffetMenuMEntity.BuffetMenu.RestaurantMenuName;
                rm.isActive = BuffetMenuMEntity.BuffetMenu.isActive;
                rm.MenuType = BuffetMenuMEntity.BuffetMenu.MenuType;
                rm.PPCost = BuffetMenuMEntity.BuffetMenu.PPCost;
                rm.isTaxInclusive = BuffetMenuMEntity.BuffetMenu.isTaxInclusive;
                rm.Tax = BuffetMenuMEntity.BuffetMenu.Tax;
                rm.TaxAmount = BuffetMenuMEntity.BuffetMenu.TaxAmount;
                rm.TotalCost = BuffetMenuMEntity.BuffetMenu.TotalCost;

            }
            else
            {
                _context.restaurantBuffetMenu.Add(BuffetMenuMEntity.BuffetMenu);
            }
            _context.SaveChanges();
            foreach (BuffetMenuDetails bmd in BuffetMenuMEntity.BuffetMenuDetails)
            {
                bmd.MenuId= BuffetMenuMEntity.BuffetMenu.RestaurantMenuId;
                rtnval = AddUpdateBuffetMenu(bmd);
            }

            return rtnval;
        }
        private bool AddUpdateBuffetMenu(BuffetMenuDetails BuffetMenuDetailsEntity)
        {
            bool rtnVal=false;
            var bm = _context.BuffetMenuDetails.Find(BuffetMenuDetailsEntity.BuffetMenuId);
            if (bm != null) {
                bm.MenuId = BuffetMenuDetailsEntity.MenuId;
                bm.MenuHeadingId= BuffetMenuDetailsEntity.MenuHeadingId;
                bm.MenuOption= BuffetMenuDetailsEntity.MenuOption;
                bm.BuffetMenuCategory = BuffetMenuDetailsEntity.BuffetMenuCategory;
                bm.CategoryId= BuffetMenuDetailsEntity.CategoryId;
                bm.isActive = BuffetMenuDetailsEntity.isActive;

            }
            else
            {
                _context.BuffetMenuDetails.Add(BuffetMenuDetailsEntity);
            }
            _context.SaveChanges();
            foreach (BuffetMenuItem bmi in BuffetMenuDetailsEntity.ItemDetails)
            {
                bmi.BuffetMenuId= BuffetMenuDetailsEntity.BuffetMenuId;
                rtnVal = AddUpdateBuffetMenuItem(bmi);
            }
           
           return rtnVal;
        }
        private bool AddUpdateBuffetMenuItem(BuffetMenuItem BuffetMenuItemEntity)
        {
            bool rtnVal = false;
            var bmi = _context.BuffetMenuItem.Find(BuffetMenuItemEntity.BuffetMenuItemId);
            if (bmi != null)
            {
            _context.BuffetMenuItem.Remove(bmi);
            }
            _context.BuffetMenuItem.Add(BuffetMenuItemEntity);
            _context.SaveChanges();
            return rtnVal;
        }
        public IEnumerable<restaurantBuffetMenu> GetAllBuffetMenu(int BranchId)
        {
            return  _context.restaurantBuffetMenu.Where(b=>b.BranchId==BranchId).ToList();

            
        }
        public restaurantBuffetMenu GetBuffetMenu(int RestaurantMneuId)
        {
            return _context.restaurantBuffetMenu.Where(b => b.RestaurantMenuId == RestaurantMneuId).SingleOrDefault();
        }
        public IEnumerable<BuffetMenuDetails> GetBuffetMenuDetaiks(int buffeyMenuId)
        {
            IEnumerable<BuffetMenuDetails> bmd = _context.BuffetMenuDetails.Where(b => b.MenuId == buffeyMenuId).ToArray();

            foreach(var b in bmd)
            {
                b.ItemDetails=_context.BuffetMenuItem.Where(b1=>b1.BuffetMenuId==b.BuffetMenuId).ToArray();
            }
            return bmd;
        }
    }
}