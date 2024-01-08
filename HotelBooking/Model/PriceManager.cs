using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("PriceManager")]
    public class PriceManager
    {
        [Key]
        public int PriceManageId { get; set; }
        public string Title { get; set; }
         
        public string RoomTypeTitle { get; set; }
        public int RoomTypeId { get; set; }
        public decimal SUN   { get; set; }
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
