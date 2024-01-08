using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("TeamMaster")]
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public int BranchId { get; set; }
        public bool IsTeamLeader { get; set; }
   
    }
}
