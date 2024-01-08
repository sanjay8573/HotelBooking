using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("SpecialPriceManager")]
    public class SPM
    {
        
        [Key]
        public int SpecialPriceManageId { get; set; }
        public string Title { get; set; }
        public string DateRange { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeTitle { get; set; }
        public decimal SUN { get; set; }
        public decimal MON { get; set; }
        public decimal TUE { get; set; }
        public decimal WED { get; set; }
        public decimal THUR { get; set; }

        public decimal FRI { get; set; }
        public decimal SAT { get; set; }
        public bool isActive { get; set; }
        public int BranchId { get; set; }
    }
}
