using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace HotelBooking.Model
{
    [Table("CurrencyExchange")]
    public class CurrencyExchange
    {
        [Key]
        public int CurrExchangeId { get; set; }
        public int BaseCurrencyId { get; set; }
        public string BaseCurrencyName { get; set; }
        public int ExchangeCurrencyId { get; set; }
        public string ExchangeCurrencyName { get; set;}
        public decimal ExchangeValue { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }

       
    }
}