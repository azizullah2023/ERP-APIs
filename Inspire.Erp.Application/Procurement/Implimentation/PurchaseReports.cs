using Inspire.Erp.Application.Procurement.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models.Procurement;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Procurement.Implimentation
{
    public class PurchaseReports : IPurchaseReports
    {
        private IRepository<PurchaseOrder> _purchaseOrder;
        private IRepository<PurchaseOrderDetails> _poDetails;
        private IRepository<Suppliers> _supliers;
        private IRepository<JobMaster> _jobMaster;
        private IRepository<ItemMaster> _itemMaster;
        private IRepository<GrnPoDetails> _grnPODetails;

        public PurchaseReports(IRepository<PurchaseOrder> purchaseOrderRepository, IRepository<Suppliers> supliers, IRepository<JobMaster> jobMaster, IRepository<PurchaseOrderDetails> poDetails, IRepository<ItemMaster> itemMaster, IRepository<GrnPoDetails> grnPODetails)
        {
            _purchaseOrder = purchaseOrderRepository;
            _supliers = supliers;
            _jobMaster = jobMaster;
            _poDetails = poDetails;
            _itemMaster = itemMaster;
            _grnPODetails = grnPODetails;
        }

        //irfan 08 Dec 2023
        /// <summary>
        /// Name : getPurdhaseOrderReport
        /// Desc : this method is used to get data form purchase order report 
        /// </summary>
        /// <param name="model"> Need to fill only the filter property of modal
        /// model.Filter = x=>x.PurchaseOrderDate <= '' && x.PurchaseOrderDate >= '' ===>date filter
        /// model.Filter = x=>x.PurchaseOrderSupInvNo = ''===>supplier filter
        /// model.Filter = x=>x.PurchaseOrderJobId = ''===>job filter
        /// model.Filter = x=>x.PurchaseOrderApproveStatus = ''===>Po Status filter
        /// </param>
        /// <returns></returns>
        public async Task<Response<List<PurchaseOrderReport>>> getPurdhaseOrderReport(GenericGridViewModel model)
        {
            try
            {
                var result = await (from p in _purchaseOrder.GetAsQueryable().Where(model.Filter)
                              join j in _jobMaster.GetAsQueryable() on p.PurchaseOrderJobId equals j.JobMasterJobId
                              join s in _supliers.GetAsQueryable() on p.PurchaseOrderSupInvNo equals s.SuppliersInsId.ToString()
                              select new PurchaseOrderReport
                              {
                                  PONO = p.PurchaseOrderPono,
                                  Date = p.PurchaseOrderDate,
                                  Supplier = s.SuppliersInsName,
                                  JobNumner = j.JobMasterJobNo,
                                  ProjectName = j.JobMasterJobName,
                                  Remarks = p.PurchaseOrderDescription,
                                  Status = p.PurchaseOrderApproveStatus
                              }
                             
                              ).OrderBy(x=>x.Date).ToDynamicListAsync();
                List<PurchaseOrderReport> purchaseOrderReport = new List<PurchaseOrderReport>();
                if (result.Count > 0)
                {
                    foreach(var item in result)
                    {
                        purchaseOrderReport.Add(item);
                    }
                }
                return Response<List<PurchaseOrderReport>>.Fail(purchaseOrderReport, "Data Found");
            }
            catch (Exception ex)
            {
                return Response<List<PurchaseOrderReport>>.Fail(new List<PurchaseOrderReport>(), ex.Message);
            }
        }//irfan 08 Dec 2023
        /// <summary>
        /// Name : getPurdhaseOrderReport
        /// Desc : this method is used to get data form purchase order report 
        /// </summary>
        /// <param name="model"> Need to fill only the filter property of modal
        /// model.Filter = x=>x.PurchaseOrderDate <= '' && x.PurchaseOrderDate >= '' ===>date filter
        /// model.Filter = x=>x.PurchaseOrderSupInvNo = ''===>supplier filter
        /// model.Filter = x=>x.PurchaseOrderJobId = ''===>job filter
        /// model.Filter = x=>x.PurchaseOrderApproveStatus = ''===>Po Status filter
        /// </param>
        /// <returns></returns>
        /// 
        //If Cmb_Supplier.Text <> "ALL" Then
        //    CustID = Val(GetCodeFromString(Cmb_Supplier.Text))
        //    sSql = sSql & " And P.Sup_ID= " & CustID
        //End If
        //If Cmb_Job.Text <> "ALL" Then
        //    JobID = Val(GetCodeFromString(Cmb_Job.Text))
        //    sSql = sSql & " And P.JobID= " & JobID
        //End If
        //If Chk_date.Checked = True Then
        //    sSql = sSql & " And P.LpoDate Between '" & Format(DTP_DateFrom.Value, "yyyy/MMM/dd")
        //    sSql = sSql & "' AND '" & Format(DTP_DateTo.Value, "yyyy/MMM/dd") & "'"
        //End If

        public async Task<Response<List<PurchaseOrderList>>> getPurdhaseOrderList(GenericGridViewModel model)
        {
            try
            {

                //"Select P.ID,P.LpoDate,P.PONO,S.INS_Name,J.jobname,Isnull(IM.Partno,'') As PartNo,PD.MatDesc,
                //PD.Quantity,PD.Rate,PD.Amount From PurchaseOrder P 
                //Inner Join PurchaseOrderDetails PD On P.ID=PD.POID
                //Left Join Suppliers S On P.Sup_ID=S.INS_ID
                //Left Join JobMaster J On P.JobID=J.ID
                //Left Join ItemMaster IM ON IM.ItemId=PD.Mat_ID
                //Where P.Status='OPEN' And P.ID Not In (Select POID From 
                //GRN_PO_DETAILS)"
             

                 var result = await (from p in _purchaseOrder.GetAsQueryable().Where(model.Filter)
                             join pd in _poDetails.GetAsQueryable() on p.PurchaseOrderId equals pd.PurchaseOrderId
                             join s in _supliers.GetAsQueryable() on p.PurchaseOrderSupInvNo equals s.SuppliersInsId.ToString() into supplierGroup
                             from supplier in supplierGroup.DefaultIfEmpty()
                             join j in _jobMaster.GetAsQueryable() on p.PurchaseOrderJobId equals j.JobMasterJobId into jobGroup
                             from job in jobGroup.DefaultIfEmpty()
                             join im in _itemMaster.GetAsQueryable() on (long)pd.PurchaseOrderDetailsMatId equals im.ItemMasterItemId into itemGroup
                             from item in itemGroup.DefaultIfEmpty()
                             where p.PurchaseOrderStatus == "OPEN" && !_grnPODetails.GetAsQueryable().Any(g => g.GrnPoDetailsPoId == p.PurchaseOrderId)
                             select new PurchaseOrderList()
                             {
                                 ID = p.PurchaseOrderId,
                                 LPODate = p.PurchaseOrderLpodate,
                                 PONO = p.PurchaseOrderPono,
                                 SupplierName = supplier.SuppliersInsName,
                                 JobName = job.JobMasterJobName,
                                 PartNo = item != null ? item.ItemMasterPartNo: "",
                                 ItemName = pd.PurchaseOrderDetailsItemName,
                                 Quantity = pd.PurchaseOrderDetailsQuantity,
                                 Rate = pd.PurchaseOrderDetailsRate,
                                 Amount = pd.PurchaseOrderDetailsActualAmount
                             }).ToDynamicListAsync();

                List<PurchaseOrderList> data = new List<PurchaseOrderList>();
                if(result.Count > 0)
                {
                    foreach(var item  in result)
                    {
                        data.Add(item);
                    }
                }
                return Response<List<PurchaseOrderList>>.Fail(data, "Data Found");
            }
            catch (Exception ex)
            {
                return Response<List<PurchaseOrderList>>.Fail(new List<PurchaseOrderList>(), ex.Message);
            }
        }
    }
}
