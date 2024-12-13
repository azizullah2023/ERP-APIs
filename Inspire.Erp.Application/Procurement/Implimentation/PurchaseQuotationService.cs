using Inspire.Erp.Application.Procurement.Interfaces;
using Inspire.Erp.Application.Account.Implementations;
using Inspire.Erp.Application.Procurement.Implementation;
using Inspire.Erp.Application.Common;
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
using Inspire.Erp.Domain.Entities.POS;

namespace Inspire.Erp.Application.Procurement.Implementation
{
    public class PurchaseQuotationService : IPurchaseQuotationService
    {
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<PurchaseQuotation> _purchaseQuotationRepository;
        private IRepository<PurchaseQuotationDetails> _purchaseQuotationDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;
        private readonly IRepository<PermissionApproval> _permissionApprovalRepo;
        private readonly IRepository<Approval> _approvalRepo;
        private readonly IRepository<ApprovalDetail> _approvalDetailRepo;

        private IRepository<ItemMaster> _itemMasterRepository;
        private IRepository<UnitMaster> _unitMasterRepository;
        private IRepository<SuppliersMaster> _suppliersMasterRepository;
        private IRepository<LocationMaster> _locationMasterRepository;

        ////      private IRepository<ReportPurchaseQuotation> _reportPurchaseQuotationRepository;
        ////public PurchaseQuotationService(IRepository<ReportPurchaseQuotation> reportPurchaseQuotationRepository,
        public PurchaseQuotationService(IRepository<ItemMaster> itemMasterRepository, IRepository<UnitMaster> unitMasterRepository,
            IRepository<SuppliersMaster> suppliersMasterRepository, IRepository<LocationMaster> locationMasterRepository,
             IRepository<PermissionApproval> permissionApprovalRepo,
            IRepository<Approval> approvalRepo, IRepository<ApprovalDetail> approvalDetailRepo,
            IRepository<AccountsTransactions> accountTransactionRepository, IRepository<StockRegister> stockRegisterRepository, IRepository<ProgramSettings> programsettingsRepository,
             IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,

            IRepository<PurchaseQuotation> purchaseQuotationRepository, IRepository<PurchaseQuotationDetails> purchaseQuotationDetailsRepository)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._stockRegisterRepository = stockRegisterRepository;
            this._purchaseQuotationRepository = purchaseQuotationRepository;
            this._purchaseQuotationDetailsRepository = purchaseQuotationDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;


            _itemMasterRepository = itemMasterRepository;
            _unitMasterRepository = unitMasterRepository;
            _suppliersMasterRepository = suppliersMasterRepository;
            _locationMasterRepository = locationMasterRepository;
            _permissionApprovalRepo = permissionApprovalRepo;
            _approvalRepo = approvalRepo;
            _approvalDetailRepo = approvalDetailRepo;

            ////_reportPurchaseQuotationRepository = reportPurchaseQuotationRepository;
        }

        ////public IEnumerable<ReportPurchaseQuotation> PurchaseQuotation_GetReportPurchaseQuotation()
        ////{
        ////    return _reportPurchaseQuotationRepository.GetAll();
        ////}



        public IEnumerable<LocationMaster> PurchaseQuotation_GetAllLocationMaster()
        {
            return _locationMasterRepository.GetAll();
        }


        public IEnumerable<SuppliersMaster> PurchaseQuotation_GetAllSuppliersMaster()
        {
            return _suppliersMasterRepository.GetAll();
        }


        public IEnumerable<UnitMaster> PurchaseQuotation_GetAllUnitMaster()
        {
            return _unitMasterRepository.GetAll();
        }
        public IEnumerable<ItemMaster> PurchaseQuotation_GetAllItemMaster()
        {
            return _itemMasterRepository.GetAll();
        }

        public PurchaseQuotationModel UpdatePurchaseQuotation(PurchaseQuotation purchaseQuotation, List<AccountsTransactions> accountsTransactions, 
            List<PurchaseQuotationDetails> purchaseQuotationDetails
            //, List<StockRegister> stockRegister
            )
        {

            try
            {
                _purchaseQuotationRepository.BeginTransaction();


                purchaseQuotation.PurchaseQuotationDetails = purchaseQuotationDetails.Select((k) =>
                {
                    //if (k.PurchaseQuotationDetailsId == 0)
                    //{
                    k.PurchaseQuotationId = purchaseQuotation.PurchaseQuotationId;
                    k.PurchaseQuotationDetailsNo = purchaseQuotation.PurchaseQuotationNo;
                    //k.PurchaseQuotationDetailsId = 0;
                    //}

                    return k;
                }).ToList();

                _purchaseQuotationRepository.Update(purchaseQuotation);





                //_purchaseQuotationRepository.Update(purchaseQuotation);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsTransDate = purchaseQuotation.PurchaseQuotationDate;
                        k.AccountsTransactionsVoucherNo = purchaseQuotation.PurchaseQuotationNo;
                        k.AccountsTransactionsVoucherType = VoucherType.PurchaseQuotation_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }

                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);


                //stockRegister = stockRegister.Select((k) =>
                //{
                //    if (k.StockRegisterId == 0)
                //    {
                //        k.StockRegisterVoucherDate = purchaseQuotation.PurchaseQuotationDate;
                //        k.StockRegisterVoucherNo = purchaseQuotation.PurchaseQuotationNo;
                //        k.StockRegisterTransType = VoucherType.PurchaseQuotation_TYPE;

                //        k.StockRegisterStatus = AccountStatus.Approved;
                //    }

                //    return k;
                //}).ToList();
                //_stockRegisterRepository.UpdateList(stockRegister);






                _purchaseQuotationRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _purchaseQuotationRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedPurchaseQuotationDetails(purchaseQuotation.PurchaseQuotationNo);
        }

        public int DeletePurchaseQuotation(PurchaseQuotation purchaseQuotation, List<AccountsTransactions> accountsTransactions,
            List<PurchaseQuotationDetails> purchaseQuotationDetails
            //, List<StockRegister> stockRegister
            )
        {
            int i = 0;
            try
            {
                _purchaseQuotationRepository.BeginTransaction();

                purchaseQuotation.PurchaseQuotationDelStatus = true;

                purchaseQuotationDetails = purchaseQuotationDetails.Select((k) =>
                {
                    k.PurchaseQuotationDetailsDelStatus = true;
                    return k;
                }).ToList();

                //_purchaseQuotationDetailsRepository.UpdateList(purchaseQuotationDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);




                //stockRegister = stockRegister.Select((k) =>
                //{
                //    k.StockRegisterDelStatus = true;
                //    return k;
                //}).ToList();
                //_stockRegisterRepository.UpdateList(stockRegister);




                purchaseQuotation.PurchaseQuotationDetails = purchaseQuotationDetails;

                _purchaseQuotationRepository.Update(purchaseQuotation);

                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == purchaseQuotation.PurchaseQuotationNo).FirstOrDefault();

                _voucherNumbersRepository.Update(vchrnumer);

                _purchaseQuotationRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _purchaseQuotationRepository.TransactionRollback();
                i = 0;
                throw ex;
            }

            return i;

        }
        public PurchaseQuotationModel InsertPurchaseQuotation(PurchaseQuotation purchaseQuotation, List<AccountsTransactions> accountsTransactions,
            List<PurchaseQuotationDetails> purchaseQuotationDetails
            //, List<StockRegister> stockRegister
            )
        {
            try
            {
                _purchaseQuotationRepository.BeginTransaction();
                string purchaseQuotationNumber = this.GenerateVoucherNo(purchaseQuotation.PurchaseQuotationDate.Date).VouchersNumbersVNo;
                purchaseQuotation.PurchaseQuotationNo = purchaseQuotationNumber;


                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _purchaseQuotationRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.PurchaseQuotationId) + 1);

                purchaseQuotation.PurchaseQuotationId = maxcount;






                purchaseQuotationDetails = purchaseQuotationDetails.Select((x) =>
                {
                    x.PurchaseQuotationId = maxcount;
                    x.PurchaseQuotationDetailsNo = purchaseQuotationNumber;
                    return x;
                }).ToList();
                //_purchaseQuotationDetailsRepository.InsertList(purchaseQuotationDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    //k.AccountsTransactionsTransDate = purchaseQuotation.PurchaseQuotationDate;
                    k.AccountsTransactionsVoucherNo = purchaseQuotationNumber;
                    k.AccountsTransactionsVoucherType = VoucherType.PurchaseQuotation_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);



                //stockRegister = stockRegister.Select((k) =>
                //{
                //    k.StockRegisterVoucherDate = purchaseQuotation.PurchaseQuotationDate;
                //    k.StockRegisterVoucherNo = purchaseQuotationNumber;
                //    k.StockRegisterTransType = VoucherType.PurchaseQuotation_TYPE;
                //    k.StockRegisterStatus = AccountStatus.Approved;
                //    return k;
                //}).ToList();
                //_stockRegisterRepository.InsertList(stockRegister);





                _purchaseQuotationRepository.Insert(purchaseQuotation);

                _purchaseQuotationRepository.TransactionCommit();
                return this.GetSavedPurchaseQuotationDetails(purchaseQuotation.PurchaseQuotationNo);

            }
            catch (Exception ex)
            {
                _purchaseQuotationRepository.TransactionRollback();
                throw ex;
            }



        }
        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<PurchaseQuotation> GetPurchaseQuotation()
        {
            IEnumerable<PurchaseQuotation> purchaseQuotation_ALL = _purchaseQuotationRepository.GetAll().Where(k => k.PurchaseQuotationDelStatus == false || k.PurchaseQuotationDelStatus == null);
            return purchaseQuotation_ALL;
        }
        public PurchaseQuotationModel GetSavedPurchaseQuotationDetails(string pvno)
        {
            PurchaseQuotationModel purchaseQuotationModel = new PurchaseQuotationModel();
            //purchaseQuotationModel.purchaseQuotation = _purchaseQuotationRepository.GetAsQueryable().Where(k => k.PurchaseQuotationNo == pvno && k.PurchaseQuotationDelStatus == false).SingleOrDefault();

            purchaseQuotationModel.purchaseQuotation = _purchaseQuotationRepository.GetAsQueryable().Where(k => k.PurchaseQuotationNo == pvno).SingleOrDefault();



            purchaseQuotationModel.accountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.PurchaseQuotation_TYPE && (c.AccountstransactionsDelStatus == false || c.AccountstransactionsDelStatus == null)).ToList();
            purchaseQuotationModel.purchaseQuotationDetails = _purchaseQuotationDetailsRepository.GetAsQueryable().Where(x => x.PurchaseQuotationDetailsNo == pvno && (x.PurchaseQuotationDetailsDelStatus == false || x.PurchaseQuotationDetailsDelStatus == null)).ToList();
            return purchaseQuotationModel;





        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {


                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.PurchaseQuotation_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.PurchaseQuotation_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;


                //var prefix = "CN";
                //int vnoMaxVal = 1;


                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.PurchaseQuotation_TYPE,
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



        //using Microsoft.Extensions.Logging;
        //private readonly ILogger<PaymentVoucherService> _logger;
        //IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,
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
