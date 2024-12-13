using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Interfaces
{
   public interface IAccountStatementMultiAccountService
    {
        Task<Response<MultiAccountDetailResponse>> GetAccountTransactionsMultiAccountDetail(GenericGridViewModel model);
        Task<Response<GridWrapperResponse<List<GetMultiAccountSummaryResponse>>>> GetAccountTransactionsMultiAccountSummary(GenericGridViewModel model);
        Task<Response<List<DropdownResponse>>> GetAccountMaster(string filter);
        Task<Response<List<DropdownResponse>>> GetAccountMasterGroup();
        public Task<Response<ReturnPrintResponse>> VoucherPrint(GenericGridViewModel model);

        Task<Response<List<StatementOfAccountFCDetailResponse>>> GetAccountTransactionsForeignCurrencyDetail(GenericGridViewModel model);

        Task<Response<StatementOfAccountFCSummResponse>> GetAccountTransactionsForeignCurrencySummary(GenericGridViewModel model);
    }
}
