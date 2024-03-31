using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBooking.Repository.Implementation
{
    public class StaffRepository : IStaff
    {

        private readonly StaffContext _context;
        //public StaffRepository(StaffContext context)
        //{
        //    _context = context;
        //}
        public StaffRepository()
        {
            _context = new StaffContext();

        }
        public Staff GetStaff(int staffId)
        {
            return _context.Staff.Where(s => s.Id == staffId).SingleOrDefault();
         }

        public IEnumerable<VM_Staff> GetStaffs(int branchId)
        {
            List<VM_Staff> s2 = new List<VM_Staff>();
            Staff[] s0; 
            s0 = _context.Staff.Where(b => b.BranchId == branchId && b.isActive==true).ToArray();
            foreach(Staff s1 in s0 )
            {
                VM_Staff tmpS = new VM_Staff();
                tmpS.StaffId = s1.Id;
                tmpS.StaffName = s1.StaffName;
                tmpS.Email = s1.Email;
                tmpS.Phone1 = s1.Phone1;
                tmpS.Phone2 = s1.Phone2;
                tmpS.Name = s1.StaffName;
                tmpS.LoginName = _context.StaffLogin.Where(s => s.StaffId == s1.Id).Select(n => n.LoginName).SingleOrDefault();
                tmpS.Branch= _context.Branch.Where(b => b.Id == s1.BranchId).Select(n => n.BranchName).SingleOrDefault();
                tmpS.Designation = _context.Designation.Where(d => d.Id == s1.DesignationId).Select(n => n.DesignationName).SingleOrDefault();
                tmpS.RegionId = s1.RegionId;
                tmpS.BranchId = s1.BranchId;
                tmpS.DepartmentId = s1.DepartmentId;
                tmpS.DesignationId = s1.DesignationId;
                tmpS.TeamId = s1.TeamId;
                tmpS.PrimaryRoleID = s1.PrimaryRoleID;
                tmpS.SecondryRoleID = s1.SecondryRoleID;
                s2.Add(tmpS);
            }

            return s2.ToArray();
        }

        public int AddStaff(Staff staffEntity)
        {
            int result = -1;
            
            if (staffEntity != null)
            {
                staffEntity.CompanyId = getCompanyIdByBarnchId(staffEntity.BranchId);
                if (staffEntity.Id > 0)
                {
                    Staff stf = _context.Staff.Find(staffEntity.Id);
                    stf.StaffName = staffEntity.StaffName;
                    stf.NickName=staffEntity.NickName;
                    stf.Phone1=staffEntity.Phone1;  
                    stf.Phone2=staffEntity.Phone2;
                    stf.DepartmentId=staffEntity.DepartmentId;
                    stf.DesignationId = staffEntity.DesignationId;
                    stf.TeamId = staffEntity.TeamId;
                    stf.Email = staffEntity.Email;
                }
                else
                {
                    _context.Staff.Add(staffEntity);
                   
                }
                _context.SaveChanges();
                result = staffEntity.Id;
            }
            return result;
        }

        public int SetupStaffLogin(StaffLogin stfLogin)
        {
            int result = -1;
            if (stfLogin != null)

            {
               
                    StaffLogin  s=null;
                    s = _context.StaffLogin.Where(b => b.StaffId == stfLogin.StaffId).SingleOrDefault();
                    if (s!=null && s.StaffId>0)
                    {
                        if (_context.StaffLogin.Find(s.Id) is StaffLogin found)
                        {
                            found.LoginName = stfLogin.LoginName;
                            found.Password = stfLogin.Password;
                            found.ConfirmPassword= stfLogin.ConfirmPassword;

                            _context.SaveChanges();
                        }
                        
                    }
                    else
                    {
                        _context.StaffLogin.Add(stfLogin);
                        _context.SaveChanges();
                    }
                    

                
                
                result = stfLogin.Id;
            }

            return result;
        }

        public int ValidateLogin(LoginRequestModel model)
        {
            int rtnVal = 0;
            if (model != null) {

             StaffLogin stf= _context.StaffLogin.Where(p => p.LoginName == model.Username && p.Password.ToLower() == model.Password.ToLower()).SingleOrDefault<StaffLogin>();
                if (stf != null)
                {
                    rtnVal = stf.StaffId;
                }

            }
            return rtnVal;

        }

        public IEnumerable<StaffTier> GetStaffTiers(int branchId)
        {
            return _context.StaffTier.Where(s => s.BranchId== branchId && s.isDeleted == false);
        }
        private int getCompanyIdByBarnchId(int BranchId)
        {
            int rtnVal = 0;
            if (BranchId > 0)
            {
                rtnVal= _context.Branch.Where(b => b.Id == BranchId).SingleOrDefault().CompanyId;
            }

            return rtnVal;
        }

        private string getEmailforStaff(int staffId)
        {
            string  rtnVal = string.Empty;
            if (staffId > 0)
            {
                rtnVal = _context.Staff.Where(b => b.Id == staffId).SingleOrDefault().Email;
            }

            return rtnVal;
        }
        public StaffLogin GetStaffLogin(int staffId)
        {
            StaffLogin stfLogin = new StaffLogin();
            stfLogin = _context.StaffLogin.Where(s => s.StaffId == staffId).SingleOrDefault();
            if (stfLogin == null)
            {
                stfLogin= new StaffLogin();
                stfLogin.LoginName = getEmailforStaff(staffId);
            }
           
            return stfLogin;
        }
    }
}
