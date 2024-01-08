using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("PriceTypeMaster")]
    public class PriceType
    {
        public int PriceTypeId { get; set; }
        public string PriceTypeTitle { get; set; }
        public string PriceTypeDescription { get; set; }
        public bool isActive { get; set; }
        public int BranchId { get; set; }
    }
}
