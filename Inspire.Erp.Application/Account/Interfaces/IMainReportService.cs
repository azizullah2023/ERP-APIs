using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IMainReportService
    {

        //public IEnumerable<ReportAccountsTransactions> MainReport_GetReportAccountsTransactions(string AccNo, int Location, int Job, int CostCenter, DateTime FromDate, DateTime ToDate, Boolean HideOpeningBal, string Narration, string Description,string TypeList);
        public IEnumerable<ReportAccountsTransactions> MainReport_GetReportAccountsTransactions(string AccNo, int Location, int Job, int CostCenter);
        public List<ReportAccountsTransactions> MainReport_GetReportAccountsTransactions_PARAM_CLASS(AccountStatementParameters accountStatementParameters
            //, List<ViewAccountsTransactionsVoucherType> viewAccountsTransactionsVoucherType
            );
        public IEnumerable<ReportStockRegister> MainReport_GetReportStockRegister();
        public IEnumerable<ViewAccountsTransactionsVoucherType> MainReport_GetAccountsTransactions_VoucherType();
        public IEnumerable<ReportStockBaseUnit> MainReport_GetReportStockLedger_PARAM_CLASS(StockLedgerParameters stockLedgerParameters, List<ViewStockTransferType> viewStockTransferType);
        public IEnumerable<ReportStockBaseUnit> MainReport_GetReportStockLedger_Location_PARAM_CLASS(StockLedgerParameters stockLedgerParameters, List<ViewStockTransferType> viewStockTransferType);
        public IEnumerable<ItemMaster> MainReport_GetAllItemMaster();
        public IEnumerable<ItemMaster> MainReport_GetAllItemMaster_Group();
        public IEnumerable<ReportSalesVoucher> MainReport_GetReportSalesVoucher_PARAM_CLASS(SalesvoucherreportParameters salesvoucherreportParameters, List<ViewStockTransferType> viewStockTransferType);
        public IEnumerable<ReportPurchaseVoucher> MainReport_GetReportPurchaseVoucher_PARAM_CLASS(PurchasevoucherreportParameters purchasevoucherreportParameters, List<ViewStockTransferType> viewStockTransferType);
        public IEnumerable<ReportPurchaseOrder> MainReport_GetReportPurchaseOrder_PARAM_CLASS(PurchaseOrderReportParameters purchaseOrderreportParameters, List<ViewStockTransferType> viewStockTransferType);
        public IEnumerable<ReportPurchaseRequisition> MainReport_GetReportPurchaseRequisition_PARAM_CLASS(PurchaserequisitionReportParameters purchaserequisitionReportParameters, List<ViewStockTransferType> viewStockTransferType);

    }
}
