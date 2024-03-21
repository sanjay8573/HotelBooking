using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Inventory
{
    [Table("IssueRegister")]
    public class IssueRegister
    {
        [Key]
        public int IssueRegisterId { get; set; }
        public string LotNumber { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int OpeningQuantity  { get; set; }
        public int IssueQuantity { get; set; }
        public int BalanceQuantity { get; set; }
        public bool IsInward { get; set; }//inward=1,outward=0
        public DateTime IssueDate { get; set; }
        public int IssuedBy { get; set; } //logged in userId
        public string IssuedByName { get; set; }//logged in Name
        public int issuedTo { get; set; }
        public string issuedToName { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }

    }
}