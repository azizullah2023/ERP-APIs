using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Sales;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Sales.Interface
{
   public interface ISalesmanwiseSalesReport
    {
        Task<Response<List<DropdownResponse>>> GetSalesManMaster();
        Task<Response<List<DropdownResponse>>> GetCustomerMasterDropdownFromSalesman(string filter);
        Task<Response<GetSalesmanOutstandingReportResponse>> AccountTransactionsSalesmanWiseOutstandingReportDetail(GenericGridViewModel model);
        Task<Response<GetSalesmanOutstandingReportResponse>> AccountTransactionsSalesmanWiseOutstandingReportSummary(GenericGridViewModel model);
    }
}
