using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    public class UserLoginResponse
    {
        public int UserId { get; set; }//staffId
        public string UserName { get; set; }//staff Name
        public string Email { get; set; }//staffemail
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLog { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchCurrencyName { get; set; }
        public string BranchCurrencyCode { get; set; }
        public string BranchCurrencySymbol { get; set; }
        public double BranchTaxPercentage { get; set; }

        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public int PrimaryRoleID { get; set; }
        public int SecondryRoleID { get; set; }
        public string PrimaryRoles { get; set; }
        public string SecondryRoles { get; set; }
        public string ResponseMessage { get; set; }
        public IEnumerable<VM_Module> Modules { get; set; }
        public IEnumerable<TaxMaster> TaxDetails { get; set; }
    }
}