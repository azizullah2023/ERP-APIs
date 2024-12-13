using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Sales;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Inspire.Erp.Application.Sales.Interfaces
{
    public interface ICustomerPurchaseOrderService
    {

        public CustomerPurchaseOrderModel InsertSalesOrder(CustomerPurchaseOrder salesVoucher, List<AccountsTransactions> accountsTransactions, List<CustomerPurchaseOrderDetails> salesOrderDetails
            , List<StockRegister> stockRegister
            );
        public CustomerPurchaseOrderModel UpdateSalesOrder(CustomerPurchaseOrder salesVoucher, List<AccountsTransactions> accountsTransactions, List<CustomerPurchaseOrderDetails> salesVoucherDetails
            , List<StockRegister> stockRegister
            );
        public int DeleteSalesOrder(CustomerPurchaseOrder salesVoucher, List<AccountsTransactions> accountsTransactions, List<CustomerPurchaseOrderDetails> salesVoucherDetails
            , List<StockRegister> stockRegister
            );
        public IEnumerable<AccountsTransactions> GetAllTransaction();
        public CustomerPurchaseOrderModel GetSavedCustomerPurchaseOrderDetails(string pvno);
        public IEnumerable<CustomerPurchaseOrder> GetCustomerPurchaseOrders();
        //public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);
        public VouchersNumbers GetVouchersNumbers(string pvno);
        //public IQueryable GetSalesVoucherDetailsByMasterNo(string SalesVoucherNo);

        public Task<Response<List<GetCustomerPurchaseOrderTrackingResponse>>> GetCustomerPurchaseOrderTracking(CustPurchOrderFilterModel model);

        public Task<Response<List<GetCustomerPurchaseOrderTrackingResponse>>> GetCustomerPOStatuses(CustPurchOrderFilterModel model);
        public Task<Response<List<CustomerSalesQuotation>>> GetCustomerQuotationStatuses(CustQuotationFilterModel model);

    }
}
