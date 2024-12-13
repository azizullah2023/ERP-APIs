using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Procurement.Interfaces
{
    public interface IStockRequisitionService
    {

        public StockRequisitionModel InsertStockRequisition(StockRequisition StockRequisition, List<AccountsTransactions> accountsTransactions, List<StockRequisitionDetails> StockRequisitionDetails
            );
        public StockRequisitionModel UpdateStockRequisition(StockRequisition StockRequisition, List<AccountsTransactions> accountsTransactions, List<StockRequisitionDetails> StockRequisitionDetails
            );
        public int DeleteStockRequisition(StockRequisition StockRequisition, List<AccountsTransactions> accountsTransactions, List<StockRequisitionDetails> StockRequisitionDetails
            );
        public IEnumerable<AccountsTransactions> GetAllTransaction();
        public StockRequisitionModel GetSavedStockRequisitionDetails(string pvno);
        public IEnumerable<StockRequisition> GetStockRequisition();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);
        public VouchersNumbers GetVouchersNumbers(string pvno);
    }
}
