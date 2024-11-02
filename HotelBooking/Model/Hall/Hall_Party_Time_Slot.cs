using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Hall
{
    [Table("Hall_Party_Time_Slot")]
    public class Hall_Party_Time_Slot
    {
        [Key]
        public int SlotId { get; set; }
        public string SlotName { get; set; }
        [NotMapped]
        public double Rate { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }



    }                       
}                           	
                          
                            
                            