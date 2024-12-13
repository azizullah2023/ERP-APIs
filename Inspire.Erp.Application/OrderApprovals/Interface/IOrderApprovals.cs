using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.OrderApprovals.Interface
{
    public interface IOrderApprovals
    {
        Task<Response<List<Domain.Entities.OrderApprovals>>> InsertOrderApproval(Domain.Entities.OrderApprovals model);
        Task<Response<List<Domain.Entities.OrderApprovals>>> UpdateOrderApproval(Domain.Entities.OrderApprovals model);
        Task<Response<List<Domain.Entities.OrderApprovals>>> DeleteOrderApproval(Domain.Entities.OrderApprovals model);
        Task<Response<List<Domain.Entities.OrderApprovals>>> GetOrderApprovals();
        Task<Response<List<Domain.Entities.OrderApprovals>>> GetOrderApprovalsByVoucherNo(string voucherNO);
        Task<Response<List<Domain.Entities.OrderApprovals>>> GetOrderApprovalsByVoucherType(string voucherType);
    }
}
