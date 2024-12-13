using Inspire.Erp.Application.Procurement.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.Account.Implementations;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Extensions.Logging;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals;
using System.Threading.Tasks;
using Inspire.Erp.Infrastructure.Database;
using SendGrid.Helpers.Mail;
using System.Linq.Dynamic.Core;
using Inspire.Erp.Domain.Entities.POS;

namespace Inspire.Erp.Application.Procurement.Implementation
{
    public class StockRequisitionService : IStockRequisitionService
    {
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<StockRequisition> _StockRequisitionRepository;
        private IRepository<StockRequisitionDetails> _StockRequisitionDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;
        private readonly IRepository<PermissionApproval> _permissionApprovalRepo;
        private readonly IRepository<Approval> _approvalRepo;
        private readonly IRepository<ApprovalDetail> _approvalDetailRepo;
        public readonly InspireErpDBContext dbContext;
        private IRepository<AvIssueVoucherDetails> _AvIssueVoucherDetails;
        private IRepository<AvIssueVoucher> _AvIssueVoucher;
        private IRepository<ItemMaster> _itemMasterRepository;
        private IRepository<UnitMaster> _unitMasterRepository;
        private IRepository<SuppliersMaster> _suppliersMasterRepository;
        private IRepository<LocationMaster> _locationMasterRepository;

        public StockRequisitionService(IRepository<ItemMaster> itemMasterRepository, IRepository<UnitMaster> unitMasterRepository,
            IRepository<SuppliersMaster> suppliersMasterRepository, IRepository<LocationMaster> locationMasterRepository,
            IRepository<PermissionApproval> permissionApprovalRepo, IRepository<Approval> approvalRepo, IRepository<ApprovalDetail> approvalDetailRepo,
            IRepository<AccountsTransactions> accountTransactionRepository, IRepository<StockRegister> stockRegisterRepository, IRepository<ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,
        IRepository<StockRequisition> StockRequisitionRepository, IRepository<StockRequisitionDetails> StockRequisitionDetailsRepository)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._stockRegisterRepository = stockRegisterRepository;
            this._StockRequisitionRepository = StockRequisitionRepository;
            this._StockRequisitionDetailsRepository = StockRequisitionDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _approvalDetailRepo = approvalDetailRepo;
            _permissionApprovalRepo = permissionApprovalRepo;
            _approvalRepo = approvalRepo;

            _itemMasterRepository = itemMasterRepository;
            _unitMasterRepository = unitMasterRepository;
            _suppliersMasterRepository = suppliersMasterRepository;
            _locationMasterRepository = locationMasterRepository;


            ////_reportStockRequisitionRepository = reportStockRequisitionRepository;
        }

        public StockRequisitionModel UpdateStockRequisition(StockRequisition StockRequisition, List<AccountsTransactions> accountsTransactions,
            List<StockRequisitionDetails> StockRequisitionDetails
            )
        {

            try
            {
                _StockRequisitionRepository.BeginTransaction();

                //this.deleteAccountTractionByVoucherId(StockRequisition.StockRequisitionNo, VoucherType.StockRequisition_TYPE);
                StockRequisition.StockRequisitionDetails = StockRequisitionDetails.Select((k) =>
                {
                    k.StockRequisitionId = StockRequisition.StockRequisitionId;
                    k.StockRequisitionDetailsNo = StockRequisition.StockRequisitionNo;
                    return k;
                }).ToList();

                _StockRequisitionRepository.Update(StockRequisition);
                //_StockRequisitionRepository.Update(StockRequisition);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsTransDate = StockRequisition.StockRequisitionDate;
                        k.AccountsTransactionsVoucherNo = StockRequisition.StockRequisitionNo;
                        k.AccountsTransactionsVoucherType = VoucherType.StockRequisition_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }

                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                _StockRequisitionRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _StockRequisitionRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedStockRequisitionDetails(StockRequisition.StockRequisitionNo);
        }

        public int DeleteStockRequisition(StockRequisition StockRequisition, List<AccountsTransactions> accountsTransactions,
            List<StockRequisitionDetails> StockRequisitionDetails
            )
        {
            int i = 0;
            try
            {
                _StockRequisitionRepository.BeginTransaction();

                StockRequisition.StockRequisitionDelStatus = true;

                StockRequisitionDetails = StockRequisitionDetails.Select((k) =>
                {
                    k.StockRequisitionDetailsDelStatus = true;
                    return k;
                }).ToList();

                _StockRequisitionDetailsRepository.UpdateList(StockRequisitionDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);

                StockRequisition.StockRequisitionDetails = StockRequisitionDetails;

                _StockRequisitionRepository.Update(StockRequisition);

                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == StockRequisition.StockRequisitionNo).FirstOrDefault();

                _voucherNumbersRepository.Update(vchrnumer);

                _StockRequisitionRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _StockRequisitionRepository.TransactionRollback();
                i = 0;
                throw ex;
            }

            return i;

        }

        public StockRequisitionModel InsertStockRequisition(StockRequisition StockRequisition, List<AccountsTransactions> accountsTransactions,
         List<StockRequisitionDetails> StockRequisitionDetails)
        {
            try
            {
                _StockRequisitionRepository.BeginTransaction();
                string StockRequisitionNumber = this.GenerateVoucherNo(StockRequisition.StockRequisitionDate.Date).VouchersNumbersVNo;
                StockRequisition.StockRequisitionNo = StockRequisitionNumber;

                decimal maxcount = 0;
                maxcount = (
                    _StockRequisitionRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : (decimal)o.StockRequisitionId) + 1);

                StockRequisition.StockRequisitionId = maxcount;

                StockRequisition.StockRequisitionDetails = StockRequisition.StockRequisitionDetails.Select((x) =>
                {
                    x.StockRequisitionId = maxcount;
                    x.StockRequisitionDetailsNo = StockRequisitionNumber;
                    return x;
                }).ToList();

                StockRequisition.StockRequisitionDetails.Clear();

                decimal max1count = 0;
                max1count =
                    _StockRequisitionDetailsRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : (decimal)o.StockRequisitionDetailsId) + 1;
                foreach (var item in StockRequisitionDetails)
                {

                    item.StockRequisitionDetailsId = max1count;
                    item.StockRequisitionId = maxcount;
                    item.StockRequisitionDetailsNo = StockRequisitionNumber ?? "";
                    StockRequisition.StockRequisitionDetails.Add(item);
                    max1count++;
                }


                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = StockRequisitionNumber;
                    k.AccountsTransactionsVoucherType = VoucherType.StockRequisition_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);

                _StockRequisitionRepository.Insert(StockRequisition);
                var permsionApproval = _permissionApprovalRepo.GetAsQueryable().Where(x => x.VoucherType == VoucherType.StockRequisition_TYPE).OrderBy(x => x.LevelOrder).ToList();
                var approvals = new Approval()
                {
                    CreatedBy = 0,
                    LocationId = null,
                    Status = false,
                    VoucherType = "StockRequisition"
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

                _StockRequisitionRepository.TransactionCommit();
                return this.GetSavedStockRequisitionDetails(StockRequisition.StockRequisitionNo);

            }
            catch (Exception ex)
            {
                _StockRequisitionRepository.TransactionRollback();
                throw ex;
            }

        }
        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<StockRequisition> GetStockRequisition()
        {
            IEnumerable<StockRequisition> StockRequisition_ALL = _StockRequisitionRepository.GetAll().Where(k => k.StockRequisitionDelStatus == false || k.StockRequisitionDelStatus == null);
            return StockRequisition_ALL;
        }
        public StockRequisitionModel GetSavedStockRequisitionDetails(string pvno)
        {
            StockRequisitionModel StockRequisitionModel = new StockRequisitionModel();
            StockRequisitionModel.StockRequisition = _StockRequisitionRepository.GetAsQueryable().Where(k => k.StockRequisitionNo == pvno).SingleOrDefault();
            StockRequisitionModel.StockRequisitionDetails = _StockRequisitionDetailsRepository.GetAsQueryable().Where(x => x.StockRequisitionDetailsNo == pvno && (x.StockRequisitionDetailsDelStatus == false || x.StockRequisitionDetailsDelStatus == null)).ToList();
            return StockRequisitionModel;

        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.StockRequisition_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.StockRequisition_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;


                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.StockRequisition_TYPE,
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
        public VouchersNumbers GetVouchersNumbers(string pvno)
        {
            try
            {
                VouchersNumbers vouchersNumbers = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == pvno).SingleOrDefault();
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
