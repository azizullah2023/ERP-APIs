using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Inspire.Erp.Application.MODULE;
using Inspire.Erp.Domain.Modals.Sales;
using Inspire.Erp.Domain.Modals;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models.Sales.DeliveryNoteReport;
using System.Security.Cryptography;
using Inspire.Erp.Domain.Modals.Stock;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Models;
using Inspire.Erp.Domain.Entities.POS;

namespace Inspire.Erp.Application.Sales.Implementations
{
    public class CustomerQuotationService : ICustomerQuotationService
    {
        private IRepository<CustomerQuotation> _customerQuotationRepository;
        private IRepository<CustomerQuotationDetails> _customerQuotationDetailsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private IRepository<Domain.Entities.ProgramSettings> _programsettingsRepository;
        private IRepository<UserTracking> _userTrackingRepository;
        private readonly ILogger<CustomerQuotationService> _logger;
        public readonly InspireErpDBContext _Context;
        private readonly IRepository<PermissionApproval> _permissionApprovalRepo;
        private readonly IRepository<Approval> _approvalRepo;
        private readonly IRepository<ApprovalDetail> _approvalDetailRepo;
        private IRepository<ItemMaster> _itemMasterRepository;

        public CustomerQuotationService(ILogger<CustomerQuotationService> logger, IRepository<PermissionApproval> permissionApprovalRepo,
            IRepository<Approval> approvalRepo, IRepository<ApprovalDetail> approvalDetailRepo,
            IRepository<CustomerQuotation> customerQuotationRepository, IRepository<VouchersNumbers> voucherNumbersRepository, IRepository<Domain.Entities.ProgramSettings> programsettingsRepository,
            IRepository<CustomerQuotationDetails> customerQuotationDetailsRepository,
            IRepository<UserTracking> userTrackingRepository,
            InspireErpDBContext Context)
        {

            this._customerQuotationRepository = customerQuotationRepository;
            this._customerQuotationDetailsRepository = customerQuotationDetailsRepository;
            this._voucherNumbersRepository = voucherNumbersRepository;
            this._programsettingsRepository = programsettingsRepository;
            _logger = logger;
            this._userTrackingRepository = userTrackingRepository;
            _approvalDetailRepo = approvalDetailRepo;
            _approvalRepo = approvalRepo;
            _permissionApprovalRepo = permissionApprovalRepo;


            _Context = Context;
        }

        public CustomerQuotation UpdateCustomerQuotation(CustomerQuotation customerQuotation, List<CustomerQuotationDetails> customerQuotationDetails)
        {

            try
            {
                _customerQuotationRepository.BeginTransaction();
                customerQuotation.CustomerQuotationDetails.Clear();

                _customerQuotationRepository.Update(customerQuotation);

                long maxcount1 = 0;
                maxcount1 = _customerQuotationDetailsRepository.GetAsQueryable()
                    .DefaultIfEmpty()
                    .Max(o => o == null ? 0 : o.CustomerQuotationDetailsId) + 1;


                foreach (var x in customerQuotationDetails)
                {
                    x.CustomerQuotationDetailsQuotationId = customerQuotation.CustomerQuotationQid;
                    x.CustomerQuotationDetailsVoucherNo = customerQuotation.CustomerQuotationVoucherNo;

                    if (x.CustomerQuotationDetailsId == 0)
                    {
                        x.CustomerQuotationDetailsId = maxcount1;
                        _customerQuotationDetailsRepository.Insert(x);
                        maxcount1++;
                    }
                    else
                    {
                        _customerQuotationDetailsRepository.Update(x);
                    }
                }


                //_customerQuotationDetailsRepository.UpdateList(customerQuotationDetails);
                //customerQuotation.CustomerQuotationDetails = customerQuotationDetails;                

                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Update";
                trackingData.UserTrackingUserVpNo = customerQuotation.CustomerQuotationVoucherNo;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "CustomerQuotation";
                _userTrackingRepository.Insert(trackingData);
                _customerQuotationRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _customerQuotationRepository.TransactionRollback();
                throw ex;
            }

            return customerQuotation;
        }
        public int DeleteCustomerQuotation(CustomerQuotation customerQuotation)
        {
            int i = 0;
            try
            {
                _customerQuotationRepository.BeginTransaction();

                customerQuotation.CustomerQuotationDelStatus = true;

                customerQuotation.CustomerQuotationDetails.Select(x =>
              {
                  x.CustomerQuotationDetailsDelStatus = true;
                  return x;
              }).ToList();
                //_customerQuotationDetailsRepository.UpdateList();
                _customerQuotationRepository.Update(customerQuotation);

                _customerQuotationRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _customerQuotationRepository.TransactionRollback();
                i = 0;
                throw ex;
            }


            return i;

        }
        public CustomerQuotation InsertCustomerQuotation(CustomerQuotation customerQuotation, List<CustomerQuotationDetails> customerQuotationDetails)
        {
            try
            {
                _customerQuotationRepository.BeginTransaction();
                string customerQuotationNumber = clsCommonFunctions.GenerateVoucherNo(customerQuotation.CustomerQuotationQuotationDate
                   , VoucherType.Quotation, Prefix.CQ_Prefix
                   , _programsettingsRepository, _voucherNumbersRepository).VouchersNumbersVNo;

                customerQuotation.CustomerQuotationVoucherNo = customerQuotationNumber;

                customerQuotation.CustomerQuotationDetails.Clear();
                long maxcount = 0;
                maxcount = (
                    _customerQuotationRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.CustomerQuotationQid) + 1);

                customerQuotation.CustomerQuotationQid = maxcount;

                long maxCount1 = 0;
                maxCount1 = _customerQuotationDetailsRepository.GetAsQueryable()
                     .DefaultIfEmpty()
                     .Max(o => o == null ? 0 : o.CustomerQuotationDetailsId) + 1;
                foreach (var x in customerQuotationDetails)
                {

                    x.CustomerQuotationDetailsId = maxCount1;
                    x.CustomerQuotationDetailsQuotationId = maxcount;
                    x.CustomerQuotationDetailsVoucherNo = customerQuotationNumber;
                    customerQuotation.CustomerQuotationDetails.Add(x);
                    maxCount1++;
                }

                _customerQuotationRepository.Insert(customerQuotation);
                //_customerQuotationDetailsRepository.InsertList(customerQuotationDetails);


                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Insert";
                trackingData.UserTrackingUserVpNo = customerQuotation.CustomerQuotationVoucherNo;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "CustomerQuotation";
                _userTrackingRepository.Insert(trackingData);
                var permsionApproval = _permissionApprovalRepo.GetAsQueryable().Where(x => x.VoucherType == "CustomerQuotation").OrderBy(x => x.LevelOrder).ToList();
                var approvals = new Approval()
                {
                    CreatedBy = 0,
                    LocationId = null,
                    Status = false,
                    VoucherType = "CustomerQuotation"
                };
                _approvalRepo.Insert(approvals);
                _approvalDetailRepo.InsertList(permsionApproval.Select(x => new ApprovalDetail()
                {
                    ApprovalId = approvals.Id,
                    Status = false,
                    UserId = x.UserId,
                    LevelOrder = x.LevelOrder,
                    ApprovedAt = DateTime.Now,
                }).ToList());

                _customerQuotationRepository.TransactionCommit();
                return customerQuotation;

            }
            catch (Exception ex)
            {
                _customerQuotationRepository.TransactionRollback();
                throw ex;
            }
        }
        public IEnumerable<CustomerQuotation> GetCustomerQuotation()
        {
            IEnumerable<CustomerQuotation> customerQuotation = _customerQuotationRepository.GetAll().Where(k => k.CustomerQuotationDelStatus == false || k.CustomerQuotationDelStatus == null);
            return customerQuotation;
        }
        public CustomerQuotation GetSavedCustQuotationDetails(int id)
        {

            CustomerQuotation voucherdata = new CustomerQuotation();
            voucherdata = _customerQuotationRepository.GetAsQueryable().Where(k => k.CustomerQuotationQid == id && k.CustomerQuotationDelStatus != true).FirstOrDefault();
            if (voucherdata != null)
            {
                voucherdata.CustomerQuotationDetails = _customerQuotationDetailsRepository.GetAsQueryable().Where(qd => qd.CustomerQuotationDetailsQuotationId == id).ToList();
                return voucherdata;

            }
            else
            {
                return null;
            }

        }
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.CQ_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.Quotation)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                VouchersNumbers vouchersNumbers = new VouchersNumbers

                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.Quotation,
                    VouchersNumbersFsno = 1,
                    VouchersNumbersStatus = AccountStatus.Pending,
                    VouchersNumbersVoucherDate = VoucherGenDate

                };
                _voucherNumbersRepository.Insert(vouchersNumbers);
                return vouchersNumbers;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public VouchersNumbers GetVouchersNumbers(string Qtno)
        {
            try
            {
                VouchersNumbers vouchersNumbers = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == Qtno && k.VouchersNumbersVType == VoucherType.Quotation).SingleOrDefault();
                return vouchersNumbers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }

        }
        public async Task<Response<List<GetCustQuotationForSaleOrderResponse>>> GetCustomerQuotationForSaleOrder()
        {
            try
            {
                List<GetCustQuotationForSaleOrderResponse> response = new List<GetCustQuotationForSaleOrderResponse>();
                var subqueryDRQD = (from s in _Context.CustomerPurchaseOrderDetails
                                    group s by new { s.CustomerPurchaseOrderDetailsQuotationDetailsId } into g
                                    select new
                                    {
                                        CPOrderDetailsQDetailsId = g.Key.CustomerPurchaseOrderDetailsQuotationDetailsId,
                                        CustomerPurchaseOrderDetailsQty = g.Sum(s => s.CustomerPurchaseOrderDetailsQty)
                                    });

                var results = await (from cpo in _Context.CustomerQuotation
                                     join cpod in _Context.CustomerQuotationDetails on cpo.CustomerQuotationQid equals cpod.CustomerQuotationDetailsQuotationId
                                     join im in _Context.ItemMaster on cpod.CustomerQuotationDetailsItemId equals im.ItemMasterItemId
                                     join um in _Context.UnitMaster on cpod.CustomerQuotationDetailsUnitId equals um.UnitMasterUnitId
                                     join drq in subqueryDRQD on cpod.CustomerQuotationDetailsQuotationId equals drq.CPOrderDetailsQDetailsId into gdrq
                                     from q in gdrq.DefaultIfEmpty()
                                     where (cpo.CustomerQuotationLocationId == 1)
                                     && ((cpod.CustomerQuotationDetailsQty - q.CustomerPurchaseOrderDetailsQty) > 0)
                                     && !((cpo.CustomerQuotationIsClose == null ? false : (bool)cpo.CustomerQuotationIsClose))
                                     select new GetCustQuotationForSaleOrderResponse
                                     {
                                         ItemName = im.ItemMasterItemName,
                                         QuotationId = cpo.CustomerQuotationQid,
                                         QuotationDate = cpo.CustomerQuotationQuotationDate,
                                         QuotationSerial = cpod.CustomerQuotationDetailsSlno,
                                         QuotationDetailsId = cpod.CustomerQuotationDetailsId,
                                         Unit = um.UnitMasterUnitShortName,
                                         UnitPrice = cpod.CustomerQuotationDetailsUnitPrice,
                                         GrossAmount = cpod.CustomerQuotationDetailsGrossAmount,
                                         Discount = cpod.CustomerQuotationDetailsDiscount,
                                         NetAmount = cpod.CustomerQuotationDetailsNetAmount,
                                         //LPONo = cpo.CustomerPurchaseOrderLpoNo == null ? "" : cpo.CustomerPurchaseOrderLpoNo,
                                         //CustomerName = cpo.CustomerPurchaseOrderCustomerName,
                                         //PartNo = im.ItemMasterPartNo == null ? "" : im.ItemMasterPartNo,
                                         //MatDes = cpod.CustomerPurchaseOrderDetailsDescription == null ? im.ItemMasterItemName : cpod.CustomerPurchaseOrderDetailsDescription,
                                         //UnitDes = um.UnitMasterUnitDescription,
                                         //CPODId = cpod.CustomerPurchaseOrderDetailsId,
                                         //Amount = cpod.CustomerPurchaseOrderDetailsAmount,
                                         //Quantity = cpod.CustomerPurchaseOrderDetailsQty,
                                         //DeliveredQuantity = q.CustomerDeliveryNoteDetailsQty ?? 0,
                                         //BalancedQuantity = cpod.CustomerPurchaseOrderDetailsQty - q.CustomerDeliveryNoteDetailsQty ?? 0,
                                         //UnitPrice = cpod.CustomerPurchaseOrderDetailsUnitPrice,
                                         //StockQuantity = _Context.StockRegister.Sum(y => y.StockRegisterSIN ?? 0 - y.StockRegisterSout ?? 0) / (decimal)umd.UnitDetailsConversionType
                                     }).OrderBy(x => x.QuotationId).ToListAsync();

                return Response<List<GetCustQuotationForSaleOrderResponse>>.Success(results, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<GetCustQuotationForSaleOrderResponse>>.Fail(new List<GetCustQuotationForSaleOrderResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<GetCustomerQuotationDetail>>> GetCustomerQuotationDetail(QuotationFilterModel FilterModel)
        {

            if (FilterModel.IsDateChecked)
            {
                var result = await (from s in _Context.CustomerQuotation.AsQueryable()
                                    join c in _Context.CustomerMaster.AsQueryable() on s.CustomerQuotationCustomerId equals c.CustomerMasterCustomerNo
                                    where s.CustomerQuotationDelStatus != true && (FilterModel.CustomerId == null || s.CustomerQuotationCustomerId == FilterModel.CustomerId)
                                    && (FilterModel.Status == null || s.CustomerQuotationIsClose == FilterModel.Status)
                                    && (s.CustomerQuotationQuotationDate >= FilterModel.fromDate && s.CustomerQuotationQuotationDate <= FilterModel.toDate)
                                    select new GetCustomerQuotationDetail
                                    {
                                        QuotationId = s.CustomerQuotationQid,
                                        VoucherNo = s.CustomerQuotationVoucherNo,
                                        VoucherType = s.CustomerQuotationVoucherType,
                                        QuotationDate = s.CustomerQuotationQuotationDate,
                                        CustomerName = c.CustomerMasterCustomerName,
                                        CustomerId = s.CustomerQuotationCustomerId,
                                        NetAmount = s.CustomerQuotationNetTotal,
                                        Status = s.CustomerQuotationIsClose
                                    }
                                    ).ToListAsync();
                return Response<List<GetCustomerQuotationDetail>>.Success(result, "Data Found");
            }
            else
            {
                var result = await (from s in _Context.CustomerQuotation.AsQueryable()
                                    join c in _Context.CustomerMaster.AsQueryable() on s.CustomerQuotationCustomerId equals c.CustomerMasterCustomerNo
                                    where (FilterModel.CustomerId == null || s.CustomerQuotationCustomerId == FilterModel.CustomerId)
                                    && (FilterModel.Status == null || s.CustomerQuotationIsClose == FilterModel.Status)
                                    select new GetCustomerQuotationDetail
                                    {
                                        QuotationId = s.CustomerQuotationQid,
                                        VoucherNo = s.CustomerQuotationVoucherNo,
                                        VoucherType = s.CustomerQuotationVoucherType,
                                        QuotationDate = s.CustomerQuotationQuotationDate,
                                        CustomerName = c.CustomerMasterCustomerName,
                                        CustomerId = s.CustomerQuotationCustomerId,
                                        NetAmount = s.CustomerQuotationNetTotal,
                                        Status = s.CustomerQuotationIsClose
                                    }
                                       ).ToListAsync();
                return Response<List<GetCustomerQuotationDetail>>.Success(result, "Data Found");
            }


        }
    }
}


