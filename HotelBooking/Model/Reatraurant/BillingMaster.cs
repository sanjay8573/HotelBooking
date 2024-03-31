using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Reatraurant
{
    [Table("Billingmaster")]
    public class BillingMaster
    {
        public BillingMaster()
        {
            BillingDetails= new List<BillingDetails>();
        }
        [Key]
        public int BillingId { get; set; }
        public int RestaurantId { get; set; }
        public string TableNo_RoomNumber { get; set; }
        public double TotalAmount { get; set; }
        public double Tax { get; set; }
        public double TaxAmount { get; set; }
        public double GrantTotal { get; set; }
        public bool isPark { get; set; }
        public DateTime ParkingTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public int Tableid { get; set; }
        public bool isRoomService { get; set;}
        [NotMapped]
        public IEnumerable<BillingDetails> BillingDetails { get; set; }



    }
    [Table("BillingDetails")]
    public class BillingDetails
    {
        [Key]
        public int BillingDetailsId { get; set; }
        public int BillingMasterId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
        public double Tax { get; set; }
        public double TaxAmount { get; set; }

        
    }
}