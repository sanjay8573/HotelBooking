using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    public class CurrencyController : ApiController
    {
        private readonly ICurrency _CURR;
        public CurrencyController() {
            _CURR = new CurrencyRepository();
        }

        [Route("api/Currency/AvailableCurrency/{BranchId}")]
        [HttpGet]
        public IEnumerable<AvailableCurrency> AvailableCurrency(int BranchId)
        {
            return _CURR.GetAvailableCurrency(BranchId);
        }
        [Route("api/Currency/AddNewCurrency")]
        [HttpPost]
        public bool AddNewCurrency(AvailableCurrency curr)
        {
            return _CURR.SaveNewCurrency(curr);
        }
        [Route("api/Currency/SetExchnageRates")]
        [HttpPost]
        public bool SetExchnageRates(IEnumerable<CurrencyExchange> exCurr)
        {
            return _CURR.SetExchange(exCurr);
        }

        [Route("api/Currency/getExchnageRates/{BranchId}")]
        [HttpGet]
        public IEnumerable<CurrencyExchange> getExchnageRates(int BranchId)
        {
            return _CURR.GetExchangeList(BranchId);
        }

        [Route("api/Currency/SaveExchangeTrans")]
        [HttpPost]
        public bool SaveExchangeTrans(ExchangeTransaction exTans)
        {
            return _CURR.SaveExchangeCurrency(exTans);
        }

        [Route("api/Currency/GetExchangeTrans")]
        [HttpPost]
        public IEnumerable<ExchangeTransaction> GetExchangeTrans(int branchId)
        {
            return _CURR.GetExchangeTransList(branchId);
        }

    }
}
