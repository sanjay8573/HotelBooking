using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    [Table("ExchangeTransaction")]
    public class ExchangeTransaction
    {
        [Key]
        public int ExchangeTransId { get; set; }
        public string CustomerName { get; set; }
        public string RoomNumber { get; set; }
        public bool isInternal { get; set; }
        public int ExchangeCurrencyId { get; set; }
        public string ExchangeCurrencyName { get; set; }
        public double CurrentValue { get; set; }
        public double fromValue { get; set; }
        public double toValue { get; set; }
        public string Remarks { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime TransDate { get; set; }
    }
}