using HotelBooking.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface IIssueRegister
    {
        IEnumerable<IssueRegister> GetIssuedItems(int branchId);
        bool AddEntry(IssueRegister item);
        bool RemoveEntry(int issueId);
        bool AddEntries(IEnumerable<IssueRegister> items);
    }
}
