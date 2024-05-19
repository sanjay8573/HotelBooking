using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers.TaxApi
{
    public class TaxController : ApiController
    {
        private readonly ITaxMaster _tax;
        public TaxController()
        {
            _tax= new TaxMasterRespository();
        }
        public TaxController(ITaxMaster itx)
        {
            _tax = itx;
        }

        [Route("api/Tax/GetAllTax/{BranchId}")]
        [HttpGet]
        public IEnumerable<TaxMaster> GetAllTax(int BranchId)
        {
            return _tax.GetAllTax(BranchId);
        }
        [Route("api/Tax/GetTaxforBranch/{BranchId}")]
        [HttpGet]
        public TaxMaster GetTaxforBranch(int BranchId)
        {
            return _tax.GetTaxForBranch(BranchId);
        }
        [Route("api/Tax/AddTax")]
        [HttpPost]
        public bool AddTax(TaxMaster TaxEntity)
        {
            return _tax.AddTax(TaxEntity);
        }
        [Route("api/Tax/DeleteTax/{BranchId}/{TaxId}")]
        [HttpDelete]
        public void DeleteTax(int BranchId, int TaxId)
        {
            _tax.DeleteTax(TaxId);
        }

    }
}
