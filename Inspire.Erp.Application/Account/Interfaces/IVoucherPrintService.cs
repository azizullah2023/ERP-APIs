using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IVoucherPrintService
    {
        Task<Response<List<DropdownResponse>>> GetVoucherTypeDropdown();
        Task<Response<List<DropdownResponse>>> GetVoucherNoDropdown();
        Task<Response<GetVoucherPrintingResponse>> GetAccountTransactions(GenericGridViewModel model);
        Task<Response<ReturnPrintResponse>> DownloadMultiAccountPrint(GenericGridViewModel model);
    }
}
