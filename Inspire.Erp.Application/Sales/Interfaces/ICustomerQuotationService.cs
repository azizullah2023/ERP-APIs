using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Modals.Sales;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Stock;

namespace Inspire.Erp.Application.Sales.Interfaces
{
    public interface ICustomerQuotationService
    {
        //public PaymentVoucher InsertPaymentVoucher(PaymentVoucher paymentVoucher,
        //  List<PaymentVoucherDetails> paymentVoucherDetails);

        public CustomerQuotation InsertCustomerQuotation(CustomerQuotation customerQuotation, List<CustomerQuotationDetails> customerQuotationDetails );
        public CustomerQuotation UpdateCustomerQuotation(CustomerQuotation customerQuotation, List<CustomerQuotationDetails> customerQuotationDetails);
        public int DeleteCustomerQuotation(CustomerQuotation customerQuotation);
        public CustomerQuotation GetSavedCustQuotationDetails(int id); 
        public IEnumerable<CustomerQuotation> GetCustomerQuotation();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);

        public VouchersNumbers GetVouchersNumbers(string Qtno);

        public Task<Response<List<GetCustQuotationForSaleOrderResponse>>> GetCustomerQuotationForSaleOrder();

        public Task<Response<List<GetCustomerQuotationDetail>>> GetCustomerQuotationDetail(QuotationFilterModel FilterModel);




    }
}
