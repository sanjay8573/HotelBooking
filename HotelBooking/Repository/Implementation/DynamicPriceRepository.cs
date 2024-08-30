using HotelBooking.Context;
using HotelBooking.Model.DynamicPrice;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class DynamicPriceRepository : IDynamicPrice
    {
        private readonly CompanyContext _ctx;
        public DynamicPriceRepository() {
            this._ctx = new CompanyContext();
        }
        public IEnumerable<DynamicPriceModel> GetAllDynamicPrice(int banchId)
        {
           return  _ctx.DynamicPrice.Where(b=>b.BranchId == banchId).ToArray();
        }

        public bool UpdateDynamicPrice(IEnumerable<DynamicPriceModel> dynamicPriceEntity)
        {
            int branchId = dynamicPriceEntity.Select(b => b.BranchId).FirstOrDefault();
            bool rtnVal = false;
            if(dynamicPriceEntity!=null) {
                if(_ctx.DynamicPrice.Count()>0) {
                    IEnumerable<DynamicPriceModel> tmpdnp = _ctx.DynamicPrice.Where(b=>b.BranchId== branchId).ToArray();
                    _ctx.DynamicPrice.RemoveRange(tmpdnp);
                }
                
                _ctx.DynamicPrice.AddRange(dynamicPriceEntity);
                _ctx.SaveChanges();
                rtnVal=true;
            }

            return rtnVal;
        }
    }
}