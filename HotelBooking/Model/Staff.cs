using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("StaffMember")]
    public class Staff
    {
        public int Id { get; set; } 
        public string StaffName { get; set; }
        public string NickName { get; set; }
        public string Phone1 { get; set; } 
        public string Phone2 { get; set;}
        public string Email  { get; set;}
        public decimal DailyBookingLimit { get; set; }
        public bool isAppliedDailyBookingLimit { get; set;}
        public int BranchId { get; set; }
        public int RegionId { get; set; }

        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int TeamId { get; set; }
        public int PrimaryRoleID { get; set; }
        public int SecondryRoleID { get; set; }
        public int CompanyId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }







    }
}
