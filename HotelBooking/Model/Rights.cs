using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("RightsMaster")]
    public class Rights
    {
     public int Id { get; set; }
    public string RightsDescription { get; set; }
    public string RightsCode { get; set; }
    public bool canAdd { get; set; }
    public bool canEdit { get; set; }
    public bool canDelete{ get; set; }
    public int ModuleId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int BranchId { get; set; }
        [NotMapped]
    public bool isSelected { get; set; } = false;
    }
}
