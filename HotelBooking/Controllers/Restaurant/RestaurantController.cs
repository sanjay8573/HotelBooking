using HotelBooking.Model;
using HotelBooking.Model.Reatraurant;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers.Restaurant
{
    public class RestaurantController : ApiController
    {
        private readonly IRestaurant _REST;
        private readonly IBilling _billing;
        public RestaurantController() { 
            _REST= new RestaurantRepository();
            _billing= new BillingRepository();
        }

        [Route("api/restaurant/GetRestaurant/{BranchId}/{RestaurantId}")]
        [HttpGet]
        public RestaurantModel GetRestaurant(int BranchId,int RestaurantId)
        {
            return _REST.GetRestaurant(BranchId, RestaurantId);
        }
        [Route("api/restaurant/GetRestaurants/{BranchId}/{RestaurantId}")]
        [HttpGet]
        public IEnumerable<RestaurantModel> GetRestaurants(int BranchId)
        {
            return _REST.GetAllRestaurant(BranchId);
        }

        [Route("api/restaurant/SaveRestaurant")]
        [HttpPost]
        public bool SaveRestaurant(RestaurantModel restroEntity)
        {
            return _REST.SaveRestaurant(restroEntity);
        }

        [Route("api/restaurant/GetRestaurantTables/{RestaurantId}")]
        [HttpGet]
        public IEnumerable<RestaurantTables> GetRestaurantTables( int RestaurantId)
        {
            return _REST.GetAllRestaurantTables(RestaurantId);
        }

        [Route("api/restaurant/GetRestaurantImagess/{RestaurantId}")]
        [HttpGet]
        public IEnumerable<ImageMaster> GetRestaurantImagess(int RestaurantId)
        {
            return _REST.GetAllRestaurantImages(RestaurantId);
        }

        [Route("api/restaurant/SaveRestaurantMenu")]
        [HttpPost]
        public bool SaveRestaurantMenu(RestaurantMenu restroMenuEntity)
        {
            return _REST.SaveRestaurantMenu(restroMenuEntity);
        }
        [Route("api/restaurant/GetRestaurantMenus/{RestaurantId}")]
        [HttpGet]
        public IEnumerable<RestaurantMenu> GetRestaurantMenus(int RestaurantId)
        {
            return _REST.GetAllRestaurantMenu(RestaurantId);
        }

        [Route("api/restaurant/GetRestaurantMenu/{RestaurantMenuId}")]
        [HttpGet]
        public RestaurantMenu GetRestaurantMenu(int RestaurantMenuId)
        {
            return _REST.GetRestaurantMenu(RestaurantMenuId);
        }

        [Route("api/restaurant/GetParkItems/{RestaurantId}/{tableId}")]
        [HttpGet]
        public IEnumerable<BillingDetails> GetParkItems(int RestaurantId,int tableId)
        {
            return _REST.getParkItems(RestaurantId, tableId);   
        }
        [Route("api/restaurant/SaveFoodCart")]
        [HttpPost]
        public bool SaveFoodCart(BillingMaster bmEntity)
        {
            return _billing.SaveBilling(bmEntity);
        }
    }
}
