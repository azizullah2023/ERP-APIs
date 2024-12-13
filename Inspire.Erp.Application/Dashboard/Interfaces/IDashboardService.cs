using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Dashboard.Interfaces
{
   public interface IDashboardService
    {
         Task<Response<List<GetSummaryResponse>>> GetSummaryChart(DashboardFilterModel model);
         Task<Response<List<GetCustomerSupplierResponse>>> GetCustomerSupplier(DashboardFilterModel model);
         Task<Response<List<GetSalesResponse>>> GetDasboardPurchaseGroupAccount(DashboardFilterModel model);
         Task<Response<List<GetPDCResponse>>> GetDashboardPDCIssueRecieved(DashboardFilterModel model);
         Task<Response<List<GetIncomeExpenseResponse>>> GetDasboardIncomeExpense(DashboardFilterModel model);
        Task<Response<PDCCountResponse>> GetDasboardIssueCount(DashboardFilterModel model);
        Task<Response<PDCCountResponse>> GetDashboardBankBalance(DashboardFilterModel model);
    }
}
