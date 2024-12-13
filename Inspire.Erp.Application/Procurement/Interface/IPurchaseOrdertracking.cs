using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models.Procurement.PurchaseOrderTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Procurement.Interface
{
    public interface IPurchaseOrdertracking
    {
        public Task<Response<List<POTrackingDetails>>> GetPurchaseOrderTrackingDetails(ReportFilter filter);
        public Task<Response<List<POTrackingSummary>>> GetPurchaseOrderTrackingSummary(ReportFilter filter);
        public Task<Response<List<UserTrackingDetails>>> GetUserTrackingDetails(UserTrackFilter filter);
       Task<Response<List<DropdownUserVPType>>> GetTrackingUserVPType();
    }
}

