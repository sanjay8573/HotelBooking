using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("Branches")]
    public class Branch
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }

        public string State { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        public string Fax  { get; set; }
        

        public string EmailID { get; set; }
       

        public string IATANo { get; set; }


        public string Bank1Name { get; set; }
        public string Bank1AcNumber { get; set; }

        public string Bank2Name { get; set; }
        public string Bank2AcNumber { get; set; }

        public string AltBankName { get; set; }
        public string AltBankAcNumber { get; set; }
       
    }
}
