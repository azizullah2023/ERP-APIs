using Inspire.Erp.Application.Procurement.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models.Procurement;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Inspire.Erp.Application.Procurement.Implimentation
{
    public class PurchaseRequisitionStatusReport : IPurchaseRequisitionStatusReport
    {


        private IRepository<PurchaseOrder> _pOrder;
        private IRepository<PurchaseOrderDetails> _poDetails;
        private IRepository<PurchaseRequisition> _pRequisition;
        private IRepository<PurchaseRequisitionDetails> _pRDetails;
        private IRepository<AvIssueVoucher> _avIssueVoucher;
        private IRepository<AvIssueVoucherDetails> _avIssueDetails;
        private IRepository<ItemMaster> _itemMaster;
        private IRepository<UnitMaster> _unitMaster;
        private IRepository<StockRegister> _stockRegister;

        public PurchaseRequisitionStatusReport(IRepository<PurchaseOrderDetails> poDetails, IRepository<PurchaseRequisition> pRequisition, IRepository<ItemMaster> itemMaster, IRepository<UnitMaster> unitMaster, IRepository<PurchaseOrder> pOrder, IRepository<AvIssueVoucherDetails> avIssueDetails, IRepository<AvIssueVoucher> avIssueVoucher, IRepository<StockRegister> stockRegister, IRepository<PurchaseRequisitionDetails> pRDetails)
        {
            _poDetails = poDetails;
            _pRequisition = pRequisition;
            _itemMaster = itemMaster;
            _unitMaster = unitMaster;
            _pOrder = pOrder;
            _avIssueDetails = avIssueDetails;
            _avIssueVoucher = avIssueVoucher;
            _stockRegister = stockRegister;
            _pRDetails = pRDetails;
        }

        public async Task<Response<List<PurchaseRequisitionStatus>>> GetPurchaseRequisitionStatusReport(ReportFilter model)
        {
            try
            {

                var res45 = _pRequisition.GetAll().ToList();
                var res1 = (from pd in _pRDetails.GetAsQueryable()
                            join pm in _pRequisition.GetAsQueryable() on pd.PurchaseRequisitionId equals pm.PurchaseRequisitionId
                            join im in _itemMaster.GetAsQueryable() on pd.PurchaseRequisitionDetailsMatId equals im.ItemMasterItemId
                            join um in _unitMaster.GetAsQueryable() on pd.PurchaseRequisitionDetailsUnitId equals um.UnitMasterUnitId
                            select new { pd, pm, im, um }).ToList();
                var res2 = (from d in _poDetails.GetAsQueryable()
                            join po in _pOrder.GetAsQueryable() on d.PurchaseOrderId equals po.PurchaseOrderId
                            where po.PurchaseOrderStatus.Trim() != null
                            group d by new { d.PurchaseOrderDetailsMatId, d.PurchaseOrderDetailsPrId, po.PurchaseOrderStatus } into g
                            select new
                            {
                                ItemId = g.Key.PurchaseOrderDetailsMatId,
                                PReqId = g.Key.PurchaseOrderDetailsPrId,
                                Quantity = g.Sum(x => x.PurchaseOrderDetailsQuantity),
                                PoStatus = g.Key.PurchaseOrderStatus
                            }).ToList();
                var res3 = (from d in _avIssueDetails.GetAll()
                            join i in _avIssueVoucher.GetAll() on d.AvIssueVoucherDetailsAvSvNo equals i.AvIssueVoucherAvsvNo
                            join s in _stockRegister.GetAll() on d.AvIssueVoucherDetailsMaterialId equals s.StockRegisterMaterialID
                            group new { d, i, s } by d.AvIssueVoucherDetailsMaterialId into g
                            select new
                            {
                                ImId = (int)g.Key,
                                issueQty = g.Sum(x => x.d.AvIssueVoucherDetailsSoldQuantity),
                                BalToIssue = g.Sum(x => (x.s.StockRegisterSIN - x.s.StockRegisterSout) / 1 - (decimal)x.d.AvIssueVoucherDetailsSoldQuantity),
                                stock = g.Sum(x => x.s.StockRegisterSIN - x.s.StockRegisterSout)
                            }).ToList();


                //var res4 = from a in res1.Select(x=>x.pd).ToList()
                //           join b in res2 on new
                //           {
                //               a.PurchaseRequisitionDetailsId,
                //               a.PurchaseRequisitionDetailsMatId
                //           } equals new { b.PReqId, b.ItemId } into grp

                //var result = await (res1
                //                    join pod in (

                //                        on new { PReqId = pd.PurchaseRequisitionDetailsPrdId, ItemId = pd.PurchaseRequisitionDetailsMatId } equals new { pod.PReqId, pod.ItemId } into podGroup
                //                    from pod in podGroup.DefaultIfEmpty()
                //                    join vd in (


                //                    ) on im.ItemMasterItemId equals vd.ImId into vdGroup
                //                    from vd in vdGroup.DefaultIfEmpty()
                //                    where pd.PurchaseRequisitionDetailsReqStatus == "OPEN"
                //                    select new PurchaseRequisitionStatus()
                //                    {
                //                        IssueQty = vd.issueQty ?? 0,
                //                        BalanceToIssueQty = vd.BalToIssue ?? 0,
                //                        StockQty = vd.stock ?? 0,
                //                        PurchaseRequisition = pm,
                //                        PurchaseRequisitionDetails = pd,
                //                        MaterialName = im.ItemMasterItemName,
                //                        MaterailUnit = $"{um.UnitMasterUnitShortName} :: {um.UnitMasterUnitId}",
                //                        PartNo = im.ItemMasterPartNo ?? "",
                //                        PurchaseOrderQty = pod.Quantity ?? 0,
                //                        POStatus = pod.PoStatus ?? "OPEN",
                //                        BalanceToOrderQty = (decimal)vd.stock - (decimal)pod.Quantity
                //                    }).OrderBy(x => x.PurchaseRequisition.PurchaseRequisitionId).ToDynamicListAsync();


                var data = new List<PurchaseRequisitionStatus>();
                //foreach (var item in result)
                //{
                //    data.Add(item);
                //}
                return Response<List<PurchaseRequisitionStatus>>.Success(data, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<PurchaseRequisitionStatus>>.Fail(new List<PurchaseRequisitionStatus>(), ex.Message);
            }
        }
    }
}
