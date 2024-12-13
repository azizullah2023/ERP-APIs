using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Accounts
{
    public class OpeningBalanceVoucherViewModel
    {
        public int? OpeningVoucherMasterId { get; set; }
        public string OpeningVoucherMasterAccNo { get; set; }
        public DateTime? OpeningVoucherMasterOpBDate { get; set; }
        public double? OpeningVoucherMasterTotalDebit { get; set; }
        public double? OpeningVoucherMasterTotalCredit { get; set; }
        public string OpeningVoucherMasterRemarks { get; set; }
        public int? OpeningVoucherMasterCurrencyId { get; set; }
        public int? OpeningVoucherMasterUserId { get; set; }
        public DateTime? OpeningVoucherMasterLastUpdateTime { get; set; }
        public int? OpeningVoucherMasterFsno { get; set; }
        public bool? OpeningVoucherMasterDelStatus { get; set; }

        public List<AccountTransactionsViewModel> AccountsTransactions { get; set; }
        public List<OpeningBalanceVoucherDetailsViewModel> openingVoucherDetails { get; set; }

    }
}
