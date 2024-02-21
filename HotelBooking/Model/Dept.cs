using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("Department")]
    public class Dept
    {
        public int Id { get; set;    }
        public int BranchId { get; set; }
        public string DepartmentName { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
}
