using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    public class VM_Module
    {
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isSelected { get; set; }
        public string cssClass { get; set; }
        public string hasChild { get; set; }
        [NotMapped]
        public string Controller { get; set; }
        [NotMapped]
        public string Action { get; set; }
        public List<Rights> ModuleRights { get; set; }
        public VM_Module() {
            ModuleRights= new List<Rights>();
        }
        
    }
}
