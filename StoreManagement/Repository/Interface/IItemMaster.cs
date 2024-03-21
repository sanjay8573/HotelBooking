using StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Repository.Interface
{
    public interface IItemMaster
    {
        IEnumerable<ItemMaster> GetItems();
        //IEnumerable<IssueRegister> Issues { get; }
    }
}
