using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inspire.Erp.Domain.DTO.Customer_Enquiry;

namespace Inspire.Erp.Application.Sales.Interfaces
{
    public interface ICustomerEnquiryService
    {
        //public PaymentVoucher InsertPaymentVoucher(PaymentVoucher paymentVoucher,
        //  List<PaymentVoucherDetails> paymentVoucherDetails);

        public CustomerEnquiryModel InsertCustomerEnquiry(EnquiryMaster EnquiryMaster, List<EnquiryDetails> customerEnquiryDetails);
        public CustomerEnquiryModel UpdateCustomerEnquiry(EnquiryMaster EnquiryMaster, List<EnquiryDetails> customerEnquiryDetails);
        public int DeleteCustomerEnquiry(EnquiryMaster EnquiryMaster, List<EnquiryDetails> customerEnquiryDetails);
        public CustomerEnquiryModel GetSavedCustEnquiryDetails(string Qtno);
        public IEnumerable<EnquiryMaster> GetCustomerEnquiry();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);
         
        public VouchersNumbers GetVouchersNumbers(string Qtno);
        public Task<Response<List<CustomerEnquiryReportResponse>>> GetCustomerEnquirySearch(CustomerEnquirySearchFilter filter);
        public Task<Response<List<CustomerEnquiryReportResponse>>> getCustomerEnquirybyStatuss(CustomerEnquirySearchFilterStatus filter);

        public Task<IEnumerable<EnquiryNoDto>> GetAllEnquiryNo();


    }

}



