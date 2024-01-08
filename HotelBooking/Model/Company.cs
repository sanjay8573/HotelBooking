using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    [Table("Company")]
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
       
        public string FaxNumber { get; set;}
         public string WebSite { get; set; }

        public string Eamail { get; set; }
        public string ServiceTaxNumber { get; set; }

        public string ATOLNumber { get; set; }
       

        public string PANNumber { get; set; }
        public string TANNumber { get; set; }
        public string ABTANumner { get; set; }

        public string CompanyLog { get; set; }
        public string XHostLog { get; set; }
        public Boolean isAddressANDLogoUsedForDocs { get; set; }
        public Boolean isBranchAddressANDLogoUsedForDocs { get; set; }
        public Boolean isSAddressANDLogoUsedForB2bDocs { get; set; }

        public RegionalSettings CRS { get; set; }
        public TNC CTnC { get; set; }



    }
    
}
