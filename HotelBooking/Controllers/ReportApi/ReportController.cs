using HotelBooking.Model;
using HotelBooking.Model.Report;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;

namespace HotelBooking.Controllers.ReportApi
{
    public class ReportController : ApiController
    {
        private readonly IReport _rpt;

        public ReportController()
        {
            _rpt = new ReportRepository();
        }
        public ReportController(IReport rpt) {
            _rpt = rpt;
        }
        [Route("api/report/GetSalesReport")]
        [HttpGet]
        public VM_SalesReport GetSalesReport(SalesReportRequest srr)
        {
            return _rpt.getSalesReport(srr);
        }
        [Route("api/report/GetCommissionReport")]
        [HttpGet]
        public VM_CommissionReport GetCommissionReport(CommissionReportRequest crr)
        {
            return _rpt.getCommissionReport(crr);
        }

        [Route("api/report/GetStoreIOReport")]
        [HttpGet]
        public VM_StoreINOUTReport GetStoreIOReport(StoreINOUTReportRequest req)
        {
            return _rpt.getStoreInoutReport(req);
        }
        [Route("api/report/ItemInShortReport")]
        [HttpGet]
        public VM_ItemInShort ItemInShortReport(ItemInShortRequest req)
        {
            return _rpt.getItemShortReport(req);
        }

        [Route("api/report/CurrencyExchangeReport")]
        [HttpGet]
        public VM_CurrencyExchangeReport CurrencyExchangeReport(CurrencyExchangeReportRequest req)
        {
            return _rpt.GetCurrencyExchangeReports(req);
        }

        [Route("api/report/TourSalesReport")]
        [HttpGet]
        public VM_TourSalesReport TourSalesReport(TourSalesReportRequest srr)
        {
            return _rpt.TourSalesReport(srr);
        }
    }
}
