using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Store.Interface
{
    public interface IBalanceSheet
    {
        //public IEnumerable<BalanceSheetResponse> BalanceSheetResponse(BalanceSheetRequest obj);
        public Task<string> BalanceSheetResponse(BalanceSheetRequest obj);

    }
}
