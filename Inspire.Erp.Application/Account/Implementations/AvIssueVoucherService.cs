using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.DTO;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Models.ViewModels;
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Infrastructure.Database.Repositoy;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class AvIssueVoucherService : IAvIssueVoucherService
    {
        private readonly IRepository<AvIssueVoucher> _AvIssueVoucher;
        private readonly IRepository<AvIssueVoucherDetails> _AvIssueVoucherDetails;
        private readonly IRepository<ItemMaster> _ItemMaster;
        private readonly IRepository<JobMaster> _JobMaster;
        private readonly IRepository<IssueWithStockTransfer> _IssueWithStockTransfer;
        private readonly IRepository<StockTransferDetailsJobWise> _StockTransferDetailsJobWise;
        private readonly IRepository<StockTransferJobWise> _StockTransferJobWise;
        private readonly InspireErpDBContext _context;

        public AvIssueVoucherService(IRepository<AvIssueVoucher> AvIssueVoucher, IRepository<AvIssueVoucherDetails> AvIssueVoucherDetails,
        IRepository<ItemMaster> ItemMaster, IRepository<JobMaster> JobMaster, IRepository<IssueWithStockTransfer> IssueWithStockTransfer,
        IRepository<StockTransferDetailsJobWise> StockTransferDetailsJobWise, IRepository<StockTransferJobWise> StockTransferJobWise,
        InspireErpDBContext context)
        {
            _AvIssueVoucher = AvIssueVoucher;
            _AvIssueVoucherDetails = AvIssueVoucherDetails;
            _ItemMaster = ItemMaster;
            _JobMaster = JobMaster;
            _IssueWithStockTransfer = IssueWithStockTransfer;
            _StockTransferDetailsJobWise = StockTransferDetailsJobWise;
            _StockTransferJobWise = StockTransferJobWise;
            _context = context;
        }

        public IQueryable GetDetailWiseReport(long? itemId, string partNo, long? departmentId, long? jobId, DateTime? dateFrom, DateTime? toDate)
        {
            try
            {
                var StockTransferDetailsJobWiseList = _context.StockTransferDetailsJobWise.ToList();
                var StockTransferJobWiseList = _context.StockTransferJobWise.ToList();
                var ItemMasterList = _context.ItemMaster.ToList();
                var data = (from dtl in _context.IssueVoucherDetails.ToList()
                            join hdr in _context.IssueVoucher.ToList() on dtl.IssueVoucherDetailsNo equals hdr.IssueVoucherNo
                            join itemmaster in ItemMasterList on (long)dtl.IssueVoucherDetailsMatId equals itemmaster.ItemMasterItemId
                            into itm
                            from immaster in itm.DefaultIfEmpty()
                            select new DTOIssueReport()
                            {
                                JobId = hdr.IssueVoucherJobId,
                                DepartmentId = (int?)hdr.IssueVoucherDepartmentId,
                                ItemId = immaster != null ? (long?)immaster.ItemMasterItemId : null,
                                IssueNo = hdr.IssueVoucherNo,
                                IssueDate = hdr.IssueVoucherDate,
                                PartNo = immaster != null ? immaster.ItemMasterPartNo : "",
                                ItemName = immaster != null ? immaster.ItemMasterItemName : "",
                                Quantity = (int?)dtl.IssueVoucherDetailsQuantity,
                                Rate = (int?)dtl.IssueVoucherDetailsRate,
                                Amount = (int?)dtl.IssueVoucherDetailsRate * (int?)dtl.IssueVoucherDetailsQuantity,
                            }).OrderBy(o => o.ItemName).AsQueryable();

                var data2 = (from dtl in StockTransferDetailsJobWiseList
                             join hdr in StockTransferJobWiseList on dtl.StockTransferDetailsJobWiseSno equals hdr.StockTransferJobWiseStid
                             into leftDetail
                             from StockTrans in leftDetail.DefaultIfEmpty()
                             join itemMaster in ItemMasterList on (long)dtl.StockTransferDetailsJobWiseMaterialId equals itemMaster.ItemMasterItemId
                             into left
                             from item in left.DefaultIfEmpty()
                             select new DTOIssueReport()
                             {
                                 JobId = StockTrans != null ? StockTrans.StockTransferJobWiseJobIdFrom : null,
                                 DepartmentId = 0,
                                 ItemId = item != null ? (long?)item.ItemMasterItemId : null,
                                 IssueNo = Convert.ToString(StockTrans != null ? StockTrans.StockTransferJobWiseStid : 0),
                                 IssueDate = StockTrans != null ? StockTrans.StockTransferJobWiseDate : null,
                                 PartNo = StockTrans != null ? StockTrans.StockTransferJobWiseVno : null,
                                 ItemName = item != null ? item.ItemMasterItemName : null,
                                 Quantity = dtl.StockTransferDetailsJobWiseQty,
                                 Rate = dtl.StockTransferDetailsJobWiseRate,
                                 Amount = dtl.StockTransferDetailsJobWiseQty * dtl.StockTransferDetailsJobWiseRate
                             }).AsQueryable();

                var data3 = (from dtl in StockTransferDetailsJobWiseList
                             join hdr in StockTransferJobWiseList on dtl.StockTransferDetailsJobWiseSno equals hdr.StockTransferJobWiseStid
                             into leftDetail
                             from StockTrans in leftDetail.DefaultIfEmpty()
                             join itemMaster in ItemMasterList on (long)dtl.StockTransferDetailsJobWiseMaterialId equals itemMaster.ItemMasterItemId
                             into left
                             from item in left.DefaultIfEmpty()
                             select new DTOIssueReport()
                             {
                                 JobId = StockTrans != null ? StockTrans.StockTransferJobWiseJobIdFrom : null,
                                 DepartmentId = 0,
                                 ItemId = item != null ? (long?)item.ItemMasterItemId : null,
                                 IssueNo = Convert.ToString(StockTrans != null ? StockTrans.StockTransferJobWiseStid : 0),
                                 IssueDate = StockTrans != null ? StockTrans.StockTransferJobWiseDate : null,
                                 PartNo = StockTrans != null ? StockTrans.StockTransferJobWiseVno : null,
                                 ItemName = item != null ? item.ItemMasterItemName : null,
                                 Quantity = dtl.StockTransferDetailsJobWiseQty * -1,
                                 Rate = dtl.StockTransferDetailsJobWiseRate * -1,
                                 Amount = dtl.StockTransferDetailsJobWiseQty * dtl.StockTransferDetailsJobWiseRate
                             }).AsQueryable();

                var finalData = data.Union(data2).Union(data3).AsQueryable();

                if (finalData != null && finalData.Count() > 0)
                {
                    finalData = finalData.Where(o => (itemId == null ? true : o.ItemId == itemId) && (partNo == "" ? true : o.PartNo == partNo) && (departmentId == null ? true : o.DepartmentId == departmentId)
                    && (jobId == null ? true : o.JobId == jobId)).AsQueryable();
                }

                Console.WriteLine("**********************" + finalData.Count());
                return finalData;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable GetJobWiseReport(long? itemId, string partNo, long? departmentId, long? jobId, DateTime? dateFrom, DateTime? toDate)
        {
            try
            {
                var data = (from dtl in _context.IssueVoucherDetails
                            join hdr in _context.IssueVoucher on dtl.IssueVoucherDetailsNo equals hdr.IssueVoucherNo
                            join itemmaster in _ItemMaster.GetAll() on (long)dtl.IssueVoucherDetailsMatId equals itemmaster.ItemMasterItemId
                            into itm
                            from immaster in itm.DefaultIfEmpty()
                            join jobmaster in _JobMaster.GetAll() on hdr.IssueVoucherJobId equals jobmaster.JobMasterJobId
                            into jobm
                            from jobmas in jobm.DefaultIfEmpty()
                            select new
                            {
                                IssueNo = hdr.IssueVoucherNo,
                                IssueDate = hdr.IssueVoucherDate,
                                PartNo = immaster != null ? immaster.ItemMasterPartNo : "",
                                ItemName = immaster != null ? immaster.ItemMasterItemName : "",
                                Quantity = dtl.IssueVoucherDetailsQuantity,
                                Rate = dtl.IssueVoucherDetailsRate,
                                Amount = dtl.IssueVoucherDetailsRate * dtl.IssueVoucherDetailsQuantity,
                                JobName = jobmas != null ? jobmas.JobMasterJobName : "",
                                JobNumber = jobmas != null ? jobmas.JobMasterJobNo : "",
                                JobId = hdr.IssueVoucherJobId,
                                RelativeNo = immaster != null ? immaster.ItemMasterRef1 : "",
                                ItemId = immaster != null ? (long?)immaster.ItemMasterItemId : null,
                                DepId = hdr.IssueVoucherDepartmentId,
                                CostCenter = hdr.IssueVoucherCostCenterId,
                                Services = immaster != null ? immaster.ItemMasterServices : false,
                            }).AsQueryable();

                if (data != null && data.Count() > 0)
                {
                    data = data.Where(o => (itemId == null ? true : o.ItemId == itemId) && (partNo == "" ? true : o.PartNo == partNo) && (departmentId == null ? true : o.DepId == departmentId)
                    && (jobId == null ? true : o.JobId == jobId)).AsQueryable();
                }
                return data;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable GetSummaryWiseReport(long? itemId, string partNo, long? departmentId, long? jobId, DateTime? dateFrom, DateTime? toDate)
        {
            try
            {
                var data = _IssueWithStockTransfer.GetAll().Where(o => o.AV_Issue_Voucher_AVSV_NO == null && o.Item_Master_Item_ID == 714).Select(o => new
                {
                    SV_NO = o.AV_Issue_Voucher_AVSV_NO,
                    SV_Dt = o.AV_Issue_Voucher_AVSV_Date,
                    PartNo = o.PartNo,
                    ItemName = o.Item_Master_Item_Name,
                    Quantity = o.Quantity,
                    Rate = o.AV_Issue_Voucher_Details_Rate,
                    Amount = o.Amount,
                    JobNumber = o.Job_Master_Job_Number,
                    JobName = o.Job_Master_Job_Number,
                    DepId = o.DepId,
                    ItemId = o.Item_Master_Item_ID,
                    JobId = o.AV_Issue_Voucher_Job_ID
                }).OrderBy(o => o.SV_NO).ThenBy(o => o.SV_Dt).ThenBy(o => o.JobName).ThenBy(o => o.JobNumber).AsQueryable();
                if (data != null && data.Count() > 0)
                {
                    data = data.Where(o => (itemId == null ? true : o.ItemId == itemId) && (partNo == "" ? true : o.PartNo == partNo) && (departmentId == null ? true : o.DepId == departmentId)
                    && (jobId == null ? true : o.JobId == jobId)).AsQueryable();
                }
                return data;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}