using HotelBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface ITaxMaster
    {
        IEnumerable<TaxMaster> GetAllTax(int branchId);
        bool AddTax(TaxMaster taxMasterEntity);
        TaxMaster GetTaxForBranch(int branchId);
        bool DeleteTax(int taxId);

    }
}
