using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.MIS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.MIS.Interfaces
{
  public  interface IBalanceSheetService
    {
        Task<Response<GridWrapperResponse<List<GetBalanceSheetDetailResponse>>>> GetAccountTransactionsBalanceSheetDetail(GenericGridViewModel model);
        Task<Response<GridWrapperResponse<List<GetBalanceSheetSummaryResponse>>>> GetAccountTransactionsBalanceSheetSummary(GenericGridViewModel model);

        Task<Response<List<BalanceSheetResponse>>> GetAccountTransactionsBalanceSheetsSummary(GenericGridViewModel model);
        Task<Response<List<BalanceSheetResponse>>> GetAccountTransactionsBalanceSheetsDetails(GenericGridViewModel model);
        public Task<Response<ReturnPrintResponse>> BalanceSheetPrint(GenericGridViewModel model);
        //private string GetHTMLString(List<GetBalanceSheetDetailResponse> records);
        public Task<Response<ReturnPrintResponse>> BalanceSheetPrintSummary(GenericGridViewModel model);

    }
}
