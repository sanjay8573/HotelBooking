using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("RightsMaster")]
    public class Rights
    {
     public int Id { get; set; }
    public string RightsDescription { get; set; }
    public string RightsCode { get; set; }
    public int ModuleId { get; set; }
    [NotMapped]
    public bool isSelected { get; set; } = false;
    }
}
