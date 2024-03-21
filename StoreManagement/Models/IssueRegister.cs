using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StoreManagement.Models
{
    [Table("IssueRegister")]
    public class IssueRegister
    {
        [Key]
        public int IssueRegisterId { get; set; }
        public string LotNumber { get; set; }
        public int OpeningStock  { get; set; }
        public int IssueQuantity { get; set; }
        public int IssueToId { get; set; }
        public string IssueToName { get; set; }
        public int BalanceQuantity { get; set; }
        public bool IsInward { get; set; }//inward=1,outward=0

    }
}