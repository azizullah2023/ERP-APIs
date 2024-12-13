using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Inspire.Erp.Application.Common
{
    public class OBVoucherModel
    {
        public OpeningVoucherMaster openingBalanceVoucher { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }
        public List<OpeningVoucherDetails> openingVoucherDetails { get; set; }
    }
}
