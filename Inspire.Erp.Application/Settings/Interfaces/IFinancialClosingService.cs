using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Settings.Interface
{
   public interface IFinancialClosingService
    {
        Task<Response<GridWrapperResponse<List<FinancialClosingResponse>>>> GetFinancialClosingAsset(GenericGridViewModel model);
        Task<Response<GridWrapperResponse<List<FinancialClosingResponse>>>> GetFinancialClosingLiability(GenericGridViewModel model);
        Task<Response<GridWrapperResponse<List<FinancialClosingResponse>>>> GetFinancialClosingEquity(GenericGridViewModel model);
    }
}
