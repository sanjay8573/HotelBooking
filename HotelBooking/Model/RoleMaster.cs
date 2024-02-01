using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Serializable]
    [Table("RoleMaster")]
    public class RoleMaster
    {
    public int Id { get; set; }
    public string RoleName {get;set;}
    public string RoleCode { get; set; }
    public string Roles { get; set; }
    public int RightId { get; set; }
   public int BranchId { get; set; }
        [NotMapped]
        public List<VM_Module> ModuleRights { get; set; }
        public RoleMaster()
        {
            ModuleRights = new List<VM_Module>();
        }
    }
}
