
using HotelBooking.Model;
using HotelBooking.Context;
using HotelBooking.Repository.Interface;
using HotelBooking.Repository.Implementation;
using System.Web.Http;
using System.Collections.Generic;

namespace HotelBooking.Controllers
{
    
    public class CompanyInfoController : ApiController
    {

        private ICompany _companyRepository;
         
        public CompanyInfoController(ICompany cmp)
        { 
            _companyRepository = cmp;
        }
        public CompanyInfoController()
        {
            _companyRepository =new CompanyRepository();
        }

        [HttpGet]
        [Route("api/CompanyInfo/GetCompanyDetails")]
        public IEnumerable<Company> GetCompanyDetails()
        {
            return _companyRepository.GetCompanies();


        }

        [HttpGet]
        [Route("api/CompanyInfo/GetCompanyDetails/{compnayId}")]
        public Company GetCompanyDetails(int compnayId)
        {
            return _companyRepository.GetCompanyById(compnayId);
            
            
        }

        [HttpPost]
        [Route("api/CompanyInfo/AddCompanyDetails")]
        public int AddCompanyDetails(Company compnayEntity)
        {
            return _companyRepository.AddCompany(compnayEntity);


        }
    }
}
