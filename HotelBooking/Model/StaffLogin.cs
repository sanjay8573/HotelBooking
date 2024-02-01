using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("StaffLogin")]
    public class StaffLogin
    {
        public int Id { get; set; } 
        public int StaffId { get; set; }    
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool isActive { get; set; } = true; 


    }
}
