using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Procurement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Procurement.Interface
{
   public interface IApprovalService
    {
        Task<Response<List<DropdownResponse>>> GetDistinctPOApprovalStatus();
        Task<Response<List<PurchaseOrder>>> GetPOApprovals(GenericGridViewModel model);
        Task<Response<bool>> ApprovedPOApproval(List<int> ids);

        Task<Response<List<DropdownResponse>>> GetDistinctPRApprovalStatus();
        Task<Response<List<GetPRApprovalResponse>>> GetPRApprovals(GenericGridViewModel model);
        Task<Response<bool>> ApprovedPRApproval(List<int> ids);
    }
}
