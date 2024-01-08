namespace HotelBooking.Model
{
    public class VM_Staff
    {
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string LoginName  { get; set; }
        public string Designation { get; set; }
        public string Branch { get; set; }
        public int RegionId { get; set;}
        public int BranchId { get; set; }
        public int DepartmentId { get; set; }

        public int DesignationId { get; set; }
        public int TeamId { get; set; }
        public int PrimaryRoleID { get; set; }
        public int SecondryRoleID { get; set; }
    }
}
