using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("Designation")]
    public class Designation
    {
        public int Id { get; set; }
        public string DesignationName { get; set; }    
        public string DesignationCode { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
}
