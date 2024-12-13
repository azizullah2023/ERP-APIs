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
    public class BillofQtyService : IBillofQtyService
    {
        private IRepository<BillofQtyMaster> _BillofQtyMasterRepository;
        private IRepository<BillOfQTyDetails> _BillOfQTyDetailsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private IRepository<Domain.Entities.ProgramSettings> _programsettingsRepository;
        private IRepository<UserTracking> _userTrackingRepository;
        private readonly ILogger<CustomerQuotationService> _logger;
        public readonly InspireErpDBContext _Context;
        private readonly IRepository<PermissionApproval> _permissionApprovalRepo;
        private readonly IRepository<Approval> _approvalRepo;
        private readonly IRepository<ApprovalDetail> _approvalDetailRepo;
        private IRepository<ItemMaster> _itemMasterRepository;

        public BillofQtyService(ILogger<CustomerQuotationService> logger, IRepository<PermissionApproval> permissionApprovalRepo,
            IRepository<Approval> approvalRepo, IRepository<ApprovalDetail> approvalDetailRepo,
            IRepository<BillofQtyMaster> BillofQtyMasterRepository, IRepository<VouchersNumbers> voucherNumbersRepository, IRepository<Domain.Entities.ProgramSettings> programsettingsRepository,
            IRepository<BillOfQTyDetails> BillOfQTyDetailsRepository,
            IRepository<UserTracking> userTrackingRepository,
            InspireErpDBContext Context)
        {

            _BillofQtyMasterRepository = BillofQtyMasterRepository;
            _BillOfQTyDetailsRepository = BillOfQTyDetailsRepository;
            this._voucherNumbersRepository = voucherNumbersRepository;
            this._programsettingsRepository = programsettingsRepository;
            _logger = logger;
            this._userTrackingRepository = userTrackingRepository;
            _approvalDetailRepo = approvalDetailRepo;
            _approvalRepo = approvalRepo;
            _permissionApprovalRepo = permissionApprovalRepo;


            _Context = Context;
        }

        public BillofQtyMaster UpdateBillofQty(BillofQtyMaster BillofQty, List<BillOfQTyDetails> BillOfQTyDetails)
        {

            try
            {
                _BillofQtyMasterRepository.BeginTransaction();
                BillofQty.BillOfQTyDetails.Clear();

                _BillofQtyMasterRepository.Update(BillofQty);

                decimal maxcount1 = 0;
                maxcount1 = _BillOfQTyDetailsRepository.GetAsQueryable()
                    .DefaultIfEmpty()
                    .Max(o => o == null ? 0 : o.Id) + 1;


                foreach (var x in BillOfQTyDetails)
                {
                    x.BillQtyId = BillofQty.Id;

                    if (x.Id == 0)
                    {
                        x.Id = maxcount1;
                        x.BillQtyId = BillofQty.Id;
                        _BillOfQTyDetailsRepository.Insert(x);
                        maxcount1++;
                    }
                    else
                    {
                        _BillOfQTyDetailsRepository.Update(x);
                    }
                }
                _BillofQtyMasterRepository.TransactionCommit();
            }
            catch (Exception ex)
            {
                _BillofQtyMasterRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedBillofQty(BillofQty.Id);
        }
        public int DeleteBillofQty(decimal id)
        {
            int i = 0;
            try
            {
                _BillofQtyMasterRepository.BeginTransaction();

                var BillofQty = _BillofQtyMasterRepository.GetAsQueryable().Where(x => x.Id == id).FirstOrDefault();                

                var BillofQtydt = _BillOfQTyDetailsRepository.GetAsQueryable().Where(x => x.BillQtyId == id).ToList();
                _BillOfQTyDetailsRepository.DeleteList(BillofQtydt);

                _BillofQtyMasterRepository.Delete(BillofQty);
                _BillofQtyMasterRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _BillofQtyMasterRepository.TransactionRollback();
                i = 0;
                throw ex;
            }


            return i;

        }
        public BillofQtyMaster InsertBillofQty(BillofQtyMaster BillofQty, List<BillOfQTyDetails> BillOfQTyDetails)
        {
            try
            {
                _BillofQtyMasterRepository.BeginTransaction();
                string customerQuotationNumber = clsCommonFunctions.GenerateVoucherNo(BillofQty.Date
                   , VoucherType.BOQ, Prefix.BOQ_Prefix
                   , _programsettingsRepository, _voucherNumbersRepository).VouchersNumbersVNo;

                BillofQty.BillofqtyNo = customerQuotationNumber;

                BillofQty.BillOfQTyDetails.Clear();
                decimal maxcount = 0;
                maxcount = (
                    _BillofQtyMasterRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.Id) + 1);

                BillofQty.Id = maxcount;

                decimal maxCount1 = 0;
                maxCount1 = _BillOfQTyDetailsRepository.GetAsQueryable()
                     .DefaultIfEmpty()
                     .Max(o => o == null ? 0 : (decimal)o.Id) + 1;
                foreach (var x in BillOfQTyDetails)
                {

                    x.Id = maxCount1;
                    x.BillQtyId = maxcount;
                    BillofQty.BillOfQTyDetails.Add(x);
                    maxCount1++;
                }

                _BillofQtyMasterRepository.Insert(BillofQty);
                _BillofQtyMasterRepository.TransactionCommit();

                return this.GetSavedBillofQty(BillofQty.Id);

            }
            catch (Exception ex)
            {
                _BillofQtyMasterRepository.TransactionRollback();
                throw ex;
            }
        }
        public IEnumerable<BillofQtyMaster> GetBillofQty()
        {
            IEnumerable<BillofQtyMaster> customerQuotation = _BillofQtyMasterRepository.GetAll();
            return customerQuotation;
        }
        public BillofQtyMaster GetSavedBillofQty(decimal id)
        {

            BillofQtyMaster voucherdata = new BillofQtyMaster();
            voucherdata = _BillofQtyMasterRepository.GetAsQueryable().Where(k => k.Id == id).FirstOrDefault();
            if (voucherdata != null)
            {
                voucherdata.BillOfQTyDetails = _BillOfQTyDetailsRepository.GetAsQueryable().Where(qd => qd.BillQtyId == id).ToList();
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
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.BOQ_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.BOQ)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                VouchersNumbers vouchersNumbers = new VouchersNumbers

                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.BOQ,
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
                VouchersNumbers vouchersNumbers = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == Qtno && k.VouchersNumbersVType == VoucherType.BOQ).SingleOrDefault();
                return vouchersNumbers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }

        }
    }
}


