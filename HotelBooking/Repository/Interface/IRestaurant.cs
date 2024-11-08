﻿using HotelBooking.Model.Reatraurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface IRestaurant
    {
        IEnumerable<RestaurantModel> GetAllRestaurant(int BranchId);
        RestaurantModel GetRestaurant(int BranchId,int  RestaurantId);
        IEnumerable<RestaurantTables> GetAllRestaurantTables(int RestaurantId);
        IEnumerable<ImageMaster> GetAllRestaurantImages(int RestaurantId);
        bool SaveRestaurant(RestaurantModel RestaurantEntity);
        bool SaveRestaurantTables(IEnumerable<RestaurantTables> resTableEntity);

        

        bool SaveRestaurantMenu(RestaurantMenu menuEntity);
        IEnumerable<RestaurantMenu> GetAllRestaurantMenu(int RestaurantId);
        RestaurantMenu GetRestaurantMenu(int MenuId);
        IEnumerable<BillingDetails> getParkItems(int RetaurantId, int tableid, bool isRMS = false);
        bool ReleaseTable(int restaurantId, int tableId);
        IEnumerable<RestaurantRoomService> GetAllRestaurantRoomServices(int RestaurantId, int branchId);
        IEnumerable<BillingMaster> getCompletedOrders(int RetaurantId);
        BillingMaster GetdOrder(int OrderId);
        IEnumerable<VM_MenuHeadings> GetMenuHeadings(int BranchId);
        IEnumerable<RestaurantMenuItem> GetRestaurantMenuItems(int menuHeadingid);
        bool SaveBuffetMenu(BuffetMenuMaster BuffetMenuMEntity);
        IEnumerable<restaurantBuffetMenu> GetAllBuffetMenu(int BranchId);
        restaurantBuffetMenu GetBuffetMenu(int RestaurantMenuId);
        IEnumerable<BuffetMenuDetails> GetBuffetMenuDetaiks(int buffeyMenuId);
    }
}
