using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Stock;
using Inspire.Erp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Store.Interfaces
{
    public interface IStockLedger
    {
        public Task<Response<List<StockLedgerResponse>>> GetStockLedgerReport(StockFilterModel stockFilter);
    }
}
