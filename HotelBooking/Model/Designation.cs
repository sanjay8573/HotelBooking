using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("Designation")]
    public class Designation
    {
        public int Id { get; set; }
        public string DesignationName { get; set; }    
        public string DesignationCode { get; set; }
    }
}
