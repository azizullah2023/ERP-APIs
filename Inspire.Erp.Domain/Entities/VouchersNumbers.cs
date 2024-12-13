using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inspire.Erp.Domain.Entities
{
    public partial class VouchersNumbers
    {
        public VouchersNumbers()
        {
            AccountsTransactions = new HashSet<AccountsTransactions>();
            BankPaymentVoucherDetails = new HashSet<BankPaymentVoucherDetails>();
            BankReceiptVoucherDetails = new HashSet<BankReceiptVoucherDetails>();
            //CustomerQuotationDetails = new HashSet<CustomerQuotationDetails>();
            EnquiryDetails = new HashSet<EnquiryDetails>();
            JournalInvoiceDetails = new HashSet<JournalInvoiceDetails>();
            OpeningVoucherDetails = new HashSet<OpeningVoucherDetails>();
            PaymentVoucherDetails = new HashSet<PaymentVoucherDetails>();
            ReceiptVoucherDetails = new HashSet<ReceiptVoucherDetails>();
            //StockRegisters = new HashSet<StockRegister>();

        }

        public decimal VouchersNumbersVsno { get; set; }
        public string VouchersNumbersVNo { get; set; }
        public decimal VouchersNumbersVNoNu { get; set; }
        public string VouchersNumbersVType { get; set; }
        public decimal? VouchersNumbersFsno { get; set; }
        public string VouchersNumbersStatus { get; set; }
        public decimal? VouchersNumbersLocationId { get; set; }
        public DateTime? VouchersNumbersVoucherDate { get; set; }
        public bool? VouhersNumbersDelStatus { get; set; }

        //public virtual BankPaymentVoucher BankPaymentVoucher { get; set; }
        //public virtual BankReceiptVoucher BankReceiptVoucher { get; set; }
        //public int CustomerPurchaseOrderId { get; set; }        
        [JsonIgnore]
        public virtual CustomerPurchaseOrder CustomerPurchaseOrder { get; set; }
        public virtual PaymentVoucher PaymentVoucher { get; set; }
        public virtual ICollection<AccountsTransactions> AccountsTransactions { get; set; }
        public virtual ICollection<BankPaymentVoucherDetails> BankPaymentVoucherDetails { get; set; }
        public virtual ICollection<BankReceiptVoucherDetails> BankReceiptVoucherDetails { get; set; }
        //public virtual ICollection<CustomerQuotationDetails> CustomerQuotationDetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<EnquiryDetails> EnquiryDetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<JournalInvoiceDetails> JournalInvoiceDetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<OpeningVoucherDetails> OpeningVoucherDetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<PaymentVoucherDetails> PaymentVoucherDetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<ReceiptVoucherDetails> ReceiptVoucherDetails { get; set; }
        //public virtual ICollection<StockRegister> StockRegisters { get; set; }
    }
}
