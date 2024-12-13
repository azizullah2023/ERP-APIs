using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities.POS
{
    public class POS_CashCardAmounts
    {
        public int Id { get; set; }
        public string VoucherType { get; set; }
        public string VoucherNumber { get; set; }

        public decimal? NetAmount { get; set; }
        public decimal? RecievedAmount { get; set; }
        public string EntryType { get; set; }
        public int? PaymentMehtod { get; set; }
        public decimal? CardAmount { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public string Remarks { get; set; }
        public string PaymentModeAccount { get; set; }
        public string CardNo { get; set; }
      
       
    }
}
