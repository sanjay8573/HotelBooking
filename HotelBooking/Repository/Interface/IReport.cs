using HotelBooking.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface IReport
    {
        VM_SalesReport getSalesReport(SalesReportRequest srr);
        VM_TourSalesReport TourSalesReport(TourSalesReportRequest srr);
        VM_CommissionReport getCommissionReport(CommissionReportRequest crr);
        VM_StoreINOUTReport getStoreInoutReport(StoreINOUTReportRequest req);

        VM_RestaurantSalesReport getRestaurantSalesReport(RestaurantSalesReportRequest req);
        VM_ItemInShort getItemShortReport(ItemInShortRequest req);
        VM_CurrencyExchangeReport GetCurrencyExchangeReports(CurrencyExchangeReportRequest req);


    }
}
