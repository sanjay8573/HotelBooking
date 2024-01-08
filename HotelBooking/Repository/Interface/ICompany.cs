using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelBooking.Model;
namespace HotelBooking.Repository.Interface
{
    public interface ICompany
    {
        IEnumerable<Company> GetCompanies();
        Company  GetCompanyById(int CompanyId);
        int AddCompany(Company companyEntity);
        int UpdateCompany(Company CompanyEntity);
        void DeleteCompany(int CompanyId);
    }
}
