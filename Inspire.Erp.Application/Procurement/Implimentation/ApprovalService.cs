using Inspire.Erp.Application.Procurement.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Procurement;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Procurement.Implementation
{
    public class ApprovalService : IApprovalService
    {
        private IRepository<PurchaseOrder> _purchaseOrderRepository;
        private IRepository<PurchaseRequisition> _purchaseReqRepository;
        private IRepository<StationMaster> _locationMaster;
        private IRepository<SuppliersMaster> _supplierMaster;
        private IRepository<JobMaster> _jobMaster;


        public ApprovalService(IRepository<PurchaseOrder> purchaseOrderRepository, IRepository<SuppliersMaster> supplierMaster,
            IRepository<StationMaster> locationMaster, IRepository<JobMaster> jobMaster, IRepository<PurchaseRequisition> purchaseReqRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _locationMaster = locationMaster;
            _supplierMaster = supplierMaster;
            _jobMaster = jobMaster;
            _purchaseReqRepository = purchaseReqRepository;
        }
        #region PO Approval
        public async Task<Response<List<DropdownResponse>>> GetDistinctPOApprovalStatus()
        {
            try
            {
                var response = new List<DropdownResponse>();
                response.Add(new DropdownResponse()
                {
                    Value = "",
                    Name = "All"
                });
                response.AddRange(await _purchaseOrderRepository.GetAsQueryable().
                    Select(x => x.PurchaseOrderApproveStatus).Distinct().Select(x => new DropdownResponse
                    {
                        Name = x,
                        Value = x
                    }).ToListAsync());
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<PurchaseOrder>>> GetPOApprovals(GenericGridViewModel model)
        {
            try
            {
                List<PurchaseOrder> gridModel = new List<PurchaseOrder>();
                //   var location = await _locationMaster.GetAsQueryable().FirstOrDefaultAsync();
                var result = await _purchaseOrderRepository.GetAsQueryable().Where(model.Filter).Select(x => new PurchaseOrder
                {
                    PurchaseOrderId = x.PurchaseOrderId,
                    PurchaseOrderNo = x.PurchaseOrderNo,
                    PurchaseOrderDate = x.PurchaseOrderDate,
                    // sup = _supplierMaster.GetAsQueryable().FirstOrDefault(c => c.SuppliersMasterSupplierId == x.purchaseOrderSupplierId) != null ?
                    // _supplierMaster.GetAsQueryable().FirstOrDefault(c => c.SuppliersMasterSupplierId == x.purchaseOrderSupplierId).SuppliersMasterSupplierName : "",
                    purchaseOrderJobName = _jobMaster.GetAsQueryable().FirstOrDefault(c => c.JobMasterJobId == x.PurchaseOrderJobId) != null ?
                    _jobMaster.GetAsQueryable().FirstOrDefault(c => c.JobMasterJobId == x.PurchaseOrderJobId).JobMasterJobName : "",
                    PurchaseOrderNetAmount = x.PurchaseOrderNetAmount,
                    PurchaseOrderTotalActualAmount = x.PurchaseOrderTotalActualAmount,
                    PurchaseOrderApproveStatus = x.PurchaseOrderApproveStatus,
                    PurchaseOrderApproveDate = x.PurchaseOrderApproveDate,
                    PurchaseOrderApproveUserId = x.PurchaseOrderApproveUserId,
                }).ToListAsync();
                gridModel.AddRange(result);
                return Response<List<PurchaseOrder>>.Success(gridModel, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<PurchaseOrder>>.Fail(new List<PurchaseOrder>(), ex.Message);
            }
        }
        public async Task<Response<bool>> ApprovedPOApproval(List<int> ids)
        {
            try
            {
                var results = await _purchaseOrderRepository.GetAsQueryable().Where(x => ids.Contains((int)x.PurchaseOrderId)).ToListAsync();
                results.ForEach((x) =>
                {
                    x.PurchaseOrderApproveDate = DateTime.Now;
                    x.PurchaseOrderApproveUserId = 0;
                    x.PurchaseOrderApproveStatus = "Approved";
                });
                _purchaseOrderRepository.SaveChangesAsync();
                return Response<bool>.Success(true, "Data found");
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(false, ex.Message);
            }
        }
        #endregion
        #region PR Approval
        public async Task<Response<List<DropdownResponse>>> GetDistinctPRApprovalStatus()
        {
            try
            {
                var response = new List<DropdownResponse>();
                response.Add(new DropdownResponse()
                {
                    Value = "",
                    Name = "All"
                });
                response.AddRange(await _purchaseReqRepository.GetAsQueryable().
                    Select(x => x.PurchaseRequisitionStatus).Distinct().Select(x => new DropdownResponse
                    {
                        Name = x,
                        Value = x
                    }).ToListAsync());
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<GetPRApprovalResponse>>> GetPRApprovals(GenericGridViewModel model)
        {
            try
            {
                List<GetPRApprovalResponse> gridModel = new List<GetPRApprovalResponse>();
                //   var location = await _locationMaster.GetAsQueryable().FirstOrDefaultAsync();
                var result = await _purchaseReqRepository.GetAsQueryable().Where(model.Filter).Select(x => new GetPRApprovalResponse
                {
                    PurchaseRequisitionPurchaseRequesId =(int) x.PurchaseRequisitionPurchaseRequestId,
                    PurchaseRequisitionStaffCode = x.PurchaseRequisitionStaffCode,
                    PurchaseRequisitionRequisitionDate = x.PurchaseRequisitionRequisitionDate,
                    PurchaseRequisitionJobName = _jobMaster.GetAsQueryable().FirstOrDefault(c => c.JobMasterJobId == x.PurchaseRequisitionJobId) != null ?
                    _jobMaster.GetAsQueryable().FirstOrDefault(c => c.JobMasterJobId == x.PurchaseRequisitionJobId).JobMasterJobName : "",
                    PurchaseRequisitionApprovedDate = x.PurchaseRequisitionApprovedDate,
                    PurchaseRequisitionApprovedBy = x.PurchaseRequisitionApprovedBy,
                }).ToListAsync();
                gridModel.AddRange(result);
                return Response<List<GetPRApprovalResponse>>.Success(gridModel, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<GetPRApprovalResponse>>.Fail(new List<GetPRApprovalResponse>(), ex.Message);
            }
        }
        public async Task<Response<bool>> ApprovedPRApproval(List<int> ids)
        {
            try
            {
                var results = await _purchaseReqRepository.GetAsQueryable().Where(x => ids.Contains((int)x.PurchaseRequisitionPurchaseRequestId)).ToListAsync();
                results.ForEach((x) =>
                {
                    x.PurchaseRequisitionApprovedDate = DateTime.Now;
                    x.PurchaseRequisitionApprovedBy = 0;
                    x.PurchaseRequisitionStatus = "Approved";
                    });
                _purchaseOrderRepository.SaveChangesAsync();
                return Response<bool>.Success(true, "Data found");
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(false, ex.Message);
            }
        }
        #endregion
    }
}
