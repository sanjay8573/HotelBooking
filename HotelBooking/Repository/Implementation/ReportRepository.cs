using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Model.Report;
using HotelBooking.Repository.Interface;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;
using System.Web.UI.WebControls;

namespace HotelBooking.Repository.Implementation
{
    public class ReportRepository : IReport
        {
        private readonly CompanyContext _context;
        


        public ReportRepository() { 
        _context= new CompanyContext();
        
        }
        public VM_CommissionReport getCommissionReport(CommissionReportRequest crr)
        {
            
            string fDate = crr.fromDate.Substring(6, 4)  + "-" + crr.fromDate.Substring(3, 2) + "-" + crr.fromDate.Substring(0, 2);
            string tDate = crr.toDate.Substring(6, 4) + "-" + crr.toDate.Substring(3, 2) + "-" + crr.toDate.Substring(0, 2);


            VM_CommissionReport vM_CommReport = new VM_CommissionReport();
            string strsql = "exec sp_getCommissionReport @fromDate='" + fDate + "',@toDate='" + tDate+ "',@BookingSourceid='" + crr.BookingSourceId  + "',@BranchId=" + crr.BranchId;
            vM_CommReport.CRR = crr;
            vM_CommReport.CommissionReport = _context.Database.SqlQuery<CommissionReport>(strsql).ToList();
            return vM_CommReport;
            
        }

        public VM_CurrencyExchangeReport GetCurrencyExchangeReports(CurrencyExchangeReportRequest req)
        {
            string fDate = req.fromDate.Substring(6, 4) + "-" + req.fromDate.Substring(3, 2) + "-" + req.fromDate.Substring(0, 2);
            string tDate = req.toDate.Substring(6, 4) + "-" + req.toDate.Substring(3, 2) + "-" + req.toDate.Substring(0, 2);
            VM_CurrencyExchangeReport vM_CurrReport = new VM_CurrencyExchangeReport();
            string strsql = "exec sp_CurrencyExchangeReport @fromDate='" + fDate + "',@toDate='" + tDate+ "',@ExchangeCurrencyId=" + req.ExchangeCurrencyId + ",@BranchId=" + req.BranchId;
            vM_CurrReport.REQ = req;
            vM_CurrReport.CurrencyExchangeReport = _context.Database.SqlQuery<ExchangeTransaction>(strsql).ToList();
            return vM_CurrReport;
        }

        public VM_ItemInShort getItemShortReport(ItemInShortRequest req)
        {
            string fDate = req.fromDate.Substring(6, 4) + "-" + req.fromDate.Substring(3, 2) + "-" + req.fromDate.Substring(0, 2);
            string tDate = req.toDate.Substring(6, 4) + "-" + req.toDate.Substring(3, 2) + "-" + req.toDate.Substring(0, 2);
            VM_ItemInShort vM_ItemInShort = new VM_ItemInShort();
            string strsql = "exec sp_StoreITEMShort @fromDate='" + fDate + "',@toDate='" + tDate + "',@BranchId=" + req.BranchId;
            vM_ItemInShort.REQ = req;
            vM_ItemInShort.itemsInShorts = _context.Database.SqlQuery<ItemsInShort>(strsql).ToList();
            return vM_ItemInShort;
        }

        public IEnumerable<RestaurantSalesReport> getRestaurantSalesReport(DateTime startDate, DateTime endDate, string restaurantName)
        {
            throw new NotImplementedException();
        }

        public VM_SalesReport getSalesReport(SalesReportRequest srr)
        {
            VM_SalesReport vM_SalesReport = new VM_SalesReport();
            string strsql = "exec sp_getSalesReport @fromDate='" +  srr.fromDate + "',@toDate='" + srr.toDate + "',@BookingSourceid='" + srr.BookingSourceId + "',@BookingRef='" + srr.bookingRef + "',@BranchId=" + srr.BranchId;
            vM_SalesReport.SalesReportRequestData = srr;
            vM_SalesReport.salesReports = _context.Database.SqlQuery<SalesReport>(strsql).ToList();
            return vM_SalesReport;

        }

        public VM_StoreINOUTReport getStoreInoutReport(StoreINOUTReportRequest req)
        {
            string fDate = req.fromDate.Substring(6, 4) + "-" + req.fromDate.Substring(3, 2) + "-" + req.fromDate.Substring(0, 2);
            string tDate = req.toDate.Substring(6, 4) + "-" + req.toDate.Substring(3, 2) + "-" + req.toDate.Substring(0, 2);
            VM_StoreINOUTReport vM_StoreINOUTReport = new VM_StoreINOUTReport();
            string strsql = "exec sp_StoreINOUT @fromDate='" + fDate + "',@toDate='" + tDate + "',@action='" + req.Action + "',@BranchId=" + req.BranchId;
            vM_StoreINOUTReport.REQ = req;
            vM_StoreINOUTReport.StoreINOUTReport = _context.Database.SqlQuery<StoreINOUTReport>(strsql).ToList();
            return vM_StoreINOUTReport;
        }
    }
}