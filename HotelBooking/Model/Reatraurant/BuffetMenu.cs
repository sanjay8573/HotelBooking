using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace HotelBooking.Model.Reatraurant
{
    [Table("BuffetMenuDetails")]
    public class BuffetMenuDetails
    {

        [Key]
        public int BuffetMenuId { get; set; }
        public string BuffetMenuCategory { get; set; }
        public int MenuId { get; set; }
        public int CategoryId { get; set; }
        public int MenuHeadingId { get; set; }
       
        public int MenuOption { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }

        [NotMapped]
        public IEnumerable<BuffetMenuItem> ItemDetails { get; set; }

       
       

    }
    [NotMapped]
    public class BuffetMenuMaster
    {
        public BuffetMenuMaster() {
            BuffetMenu= new restaurantBuffetMenu();
            BuffetMenuDetails = new List<BuffetMenuDetails>();

        }
        public restaurantBuffetMenu BuffetMenu { get; set; }
        public List<BuffetMenuDetails> BuffetMenuDetails { get; set; }
    }
    [Table("BuffetMenuItem")]
    public class BuffetMenuItem
    {
        [Key]
        public int BuffetMenuItemId { get; set; }
        public int ItemId { get; set; }
        public int HeadingId { get; set; }
        public string ItemName { get; set; }
        public int BuffetMenuId { get; set; }
    }
    [Table("restaurantBuffetMenu")]
    public class restaurantBuffetMenu
    {
        public restaurantBuffetMenu()
        {
            MenuHeading = new List<RestaurantMenuHeading>();
        }
        [Key]
        public int RestaurantMenuId { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantMenuName { get; set; }
        [NotMapped]
        public List<RestaurantMenuHeading> MenuHeading { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public double PPCost { get; set; }
        public bool isTaxInclusive { get; set; }
        public double Tax { get; set; }
        public double TaxAmount { get; set; }
        public double TotalCost { get; set; }

        public string MenuType { get; set; }
        public int BranchId { get; set; }
    }
}