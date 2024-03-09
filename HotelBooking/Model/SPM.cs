using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("SpecialPriceManager")]
    public class SPM
    {
        
        [Key]
        public int SpecialPriceManageId { get; set; }
        [Column("Title")]
        public string Title1 { get; set; }
        public string DateRangeFrom { get; set; }
        public string DateRangeTo { get; set; }
        [Column("RoomTypeId")]
        public int RoomTypeId1 { get; set; }
        [Column("RoomTypeTitle")]
        public string RoomTypeTitle1 { get; set; }
        [Column("SUN")]
        public decimal SUN1 { get; set; }
        [Column("MON")]
        public decimal MON1 { get; set; }
        [Column("TUE")]
        public decimal TUE1 { get; set; }
        [Column("WED")]
        public decimal WED1 { get; set; }
        [Column("THUR")]
        public decimal THUR1 { get; set; }
        [Column("FRI")]

        public decimal FRI1 { get; set; }
        [Column("SAT")]
        public decimal SAT1 { get; set; }
        [Column("isActive")]
        public bool isActive1 { get; set; }
        public bool isDeleted { get; set; }
        [Column("BranchId")]
        public int BranchId1 { get; set; }
    }
}
