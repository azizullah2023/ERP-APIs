using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Administration;
using Inspire.Erp.Domain.Modals.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Settings.Interface
{
   public interface IReportTypeService
    {
        Task<Response<bool>> AddEditReportType(AddEditReportTypeResponse model);
        Task<Response<GridWrapperResponse<List<GetReportTypeMasterResponse>>>> GetReportTypes(GenericGridViewModel model);
        Task<Response<AddEditReportTypeResponse>> GetSpecificVoucherType(int id = 0);
        Task<Response<bool>> DeleteReportType(List<int> ids);
    }
}
