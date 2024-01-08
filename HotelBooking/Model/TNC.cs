using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("TNCs")]
    public class TNC
    {
        
        [Key]
        public int Id { get; set; }
        public string Language { get; set; }
        public string CTNC { get; set; }
        public int companyId { get; set; }
    }
}
