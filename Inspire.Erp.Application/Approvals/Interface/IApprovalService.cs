using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models;
using Inspire.Erp.Domain.Models.Approvals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Approvals.Interface
{
    public interface IApprovalService
    {
        Task<Response<GridWrapperResponse<List<ApprovalResponse>>>> GetApprovalsRecords(GenericGridViewModel model);                
       public List<Permissionapproval> GetPermissionapById(long a, int level);
       public List<Permissionapproval> InsertPermissionApproval(List<Permissionapproval> Perm);

   

    }
}
