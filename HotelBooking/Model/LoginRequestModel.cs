namespace HotelBooking.Model
{
    public class LoginRequestModel
    {
        public LoginRequestModel() { }  
        public string Username { get; set; }    
        public string Password { get; set; }
        public string vToken { get; set; }
    }
}
