using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.MIS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.MIS.Interfaces
{
   public interface IProfitLossService
    {
        Task<Response<GetFinancialYearResponse>> GetFinancialYearResponse();
        Task<Response<ProfitLossWrapper>> GetAccountTransactionsProfitLoss(GenericGridViewModel model);
        Task<Response<ProfitAndLossWrapper>> GetAccountTransactionsProfitAndLoss(GenericGridViewModel model);
        Task<Response<WrapperProfitLossPrintSimpleResponse>> GetAccountTransactionsProfitLossPrintSimple(GenericGridViewModel model);
        Task<Response<WrapperProfitLossPrintMonthWiseResponse>> GetAccountTransactionsProfitLossPrintMonthWise(GenericGridViewModel model);
        Task<Response<WrapperProfitLossPrintSummaryResponse>> GetAccountTransactionsProfitLossPrintSummary(GenericGridViewModel model);
    }
}
