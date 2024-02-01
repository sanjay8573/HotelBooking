using System.Collections.Generic;

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
        public List<Rights> ModuleRights { get; set; }
        public VM_Module() {
            ModuleRights= new List<Rights>();
        }
        
    }
}
