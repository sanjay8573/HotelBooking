using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("ModuleMaster")]
    public class ModuleMaster
    {
        public int Id { get; set; } 
        public string ModuleName { get; set; }
        public string ModuleDescription { get; set; }
        public bool isDeleted { get; set; } = false;
        //public List<Rights> ModuleRights { get; set; }
        //public ModuleMaster() {
        //    ModuleRights = new List<Rights>();
        //}
    }
}
