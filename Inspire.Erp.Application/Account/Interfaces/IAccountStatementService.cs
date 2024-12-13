using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Interfaces
{
   public interface IAccountStatementService
    {
        Task<Response<List<DropdownResponse>>> GetAccountMasterDropdown();
        Task<Response<List<DropdownResponse>>> GetJobMasterDropdown();
        Task<Response<GetAccountStatementResponse>> GetAccountTransactions(GenericGridViewModel model);
        Task<Response<ReturnPrintResponse>> AccountSTatementPrint(GenericGridViewModel model);
    }
}
