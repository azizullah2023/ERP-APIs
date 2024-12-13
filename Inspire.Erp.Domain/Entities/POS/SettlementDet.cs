using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities.POS
{
    public partial class POS_SettlementDet
    {
        public int SettlementDetId { get; set; }
        public long? OrderId { get; set; }
        public int PaymentmethodId { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
        public DateTime? Date { get; set; }
        public int? UserId { get; set; }
        public long? CurrencyId { get; set; }
        public decimal? CurrencyConValue { get; set; }
        public decimal? LocalCurrencyValue { get; set; }
        public string CreditCardNumber { get; set; }
        public long? CreditCardTypeId { get; set; }
        public string TransactionCode { get; set; }
        public string CounterName { get; set; }
        public long? InvoiceNo { get; set; }
        public DateTime? SettlementTime { get; set; }
        public int? ShiftId { get; set; }
        public int? MealPlanCategoryId { get; set; }
        public string GSTNo { get; set; }
        public int? Remarks { get; set; }
        public int? SortOrder { get; set; }

    }
}
