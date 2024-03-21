using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Inventory
{
    [Table("ItemMaster")]
    public class ItemMaster
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; } = string.Empty;
        public int ItemCategory { get; set; } = 1;
        public int Quantity { get; set; }
        public int QuantityAvailable { get; set; }
        public int ReorderPoint { get; set; }
        public DateTime date_created { get; set; } = DateTime.Now;
        public int BranchId { get; set; }
        public decimal Price {  get; set; }
        public decimal TotalAmount { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
}