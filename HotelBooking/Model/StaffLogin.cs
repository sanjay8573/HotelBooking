namespace HotelBooking.Model
{
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
