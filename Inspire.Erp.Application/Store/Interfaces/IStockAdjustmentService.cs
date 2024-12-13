using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inspire.Erp.Domain.Modals.Stock;

namespace Inspire.Erp.Application.Store.Interfaces
{
    public interface IStockAdjustmentService
    {
        public IEnumerable<StockAdjustmentVoucher> GetStockAdjustment();
        public Task<StockAdjustmentVoucher> InsertStockAdjustmentVoucher(StockAdjustmentVoucher stockAdjustmentVoucher, List<StockAdjustmentVoucherDetails> stockAdjustmentVoucherDetails);
        public Task<StockAdjustmentVoucher> UpdateStockAdjustmentVoucher(StockAdjustmentVoucher stockAdjustmentVoucher, List<StockAdjustmentVoucherDetails> stockAdjustmentVoucherDetails);
        public StockAdjustmentVoucher GetSavedStockAdjustmentferDetails(string sano);
        public int DeleteStockAdjustmentVoucher(StockAdjustmentVoucher stockAdjustmentVoucher, List<StockAdjustmentVoucherDetails> stockAdjustmentVoucherDetails);

        Task<Response<List<StockAdjustmentReportResponse>>> StockAdjustmentReport(StockAdjustmentReportFilter model);
    }
}
