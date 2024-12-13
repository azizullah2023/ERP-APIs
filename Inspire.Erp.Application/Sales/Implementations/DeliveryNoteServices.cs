using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Application.Account.Implementations;
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
using Inspire.Erp.Application.MODULE;
using System.Threading.Tasks;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Models.Sales.DeliveryNoteReport;
using Inspire.Erp.Domain.Modals.Common;
using SendGrid.Helpers.Mail;
using Inspire.Erp.Domain.Models.Procurement.PurchaseOrderTracking;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Inspire.Erp.Application.Sales.Implementations
{
    public class DeliveryNoteServices : IDeliveryNoteServices
    {
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<CustomerDeliveryNote> _deliveryNoteRepository;
        private IRepository<CustomerDeliveryNoteDetails> _deliveryNoteDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;

        private IRepository<ItemMaster> _itemMasterRepository;
        private IRepository<UnitMaster> _unitMasterRepository;
        private IRepository<SuppliersMaster> _suppliersMasterRepository;
        private IRepository<LocationMaster> _locationMasterRepository;

        private IRepository<CustomerMaster> _customerMasterRepository;
        private IRepository<CustomerMaster> _salesManMasterRepository;
        private IRepository<DepartmentMaster> _departmentMasterRepository;
        private IRepository<LocationMaster> _locationMasterRepo;



        //private IRepository<ReportSalesVoucher> _reportSalesVoucherRepository;
        public DeliveryNoteServices(
            //IRepository<ReportSalesVoucher> reportSalesVoucherRepository,
            IRepository<CustomerMaster> customerMasterRepository,
            IRepository<CustomerMaster> salesManMasterRepository,
            IRepository<DepartmentMaster> departmentMasterRepository,
            IRepository<ItemMaster> itemMasterRepository, IRepository<UnitMaster> unitMasterRepository,
            IRepository<SuppliersMaster> suppliersMasterRepository, IRepository<LocationMaster> locationMasterRepository,

            IRepository<AccountsTransactions> accountTransactionRepository, IRepository<StockRegister> stockRegisterRepository, IRepository<ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,

            IRepository<CustomerDeliveryNote> deliveryNoteRepository, IRepository<CustomerDeliveryNoteDetails> deliveryNoteDetailsRepository
            , IRepository<LocationMaster> locationMasterRepo)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._stockRegisterRepository = stockRegisterRepository;
            this._deliveryNoteRepository = deliveryNoteRepository;
            this._deliveryNoteDetailsRepository = deliveryNoteDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _itemMasterRepository = itemMasterRepository;
            _unitMasterRepository = unitMasterRepository;
            _suppliersMasterRepository = suppliersMasterRepository;
            _locationMasterRepository = locationMasterRepository;
            _customerMasterRepository = customerMasterRepository;
            _salesManMasterRepository = salesManMasterRepository;
            _departmentMasterRepository = departmentMasterRepository;
            _locationMasterRepo = locationMasterRepo;

        }

        public DeliveryNoteModel UpdateDeliveryNote(CustomerDeliveryNote deliveryNote, List<CustomerDeliveryNoteDetails> deliveryNoteDetails
            )
        {
            try
            {
                _deliveryNoteRepository.BeginTransaction();

                int max1count = 0;
                max1count =
                    _deliveryNoteDetailsRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : (int)o.CustomerDeliveryNoteDetailsDetId) + 1;

                deliveryNote.CustomerDeliveryNoteDetails.Clear();
                _deliveryNoteRepository.Update(deliveryNote);

                foreach (var item in deliveryNoteDetails)
                {
                    item.CustomerDeliveryNoteDetailsDeliveryNo = deliveryNote.CustomerDeliveryNoteDeliveryID;

                    if (item.CustomerDeliveryNoteDetailsDetId != 0)
                    {
                        _deliveryNoteDetailsRepository.Update(item);
                    }
                    else
                    {
                        item.CustomerDeliveryNoteDetailsDetId = max1count;
                        _deliveryNoteDetailsRepository.Insert(item);
                        max1count++;
                    }
                }
                _deliveryNoteRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _deliveryNoteRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedDeliveryNoteDetails(deliveryNote.CustomerDeliveryNoteDeliveryID);
        }

        public int DeleteDeliveryNote(CustomerDeliveryNote deliveryNote, List<CustomerDeliveryNoteDetails> deliveryNoteDetails
            )
        {
            int i = 0;
            try
            {
                _deliveryNoteRepository.BeginTransaction();

                deliveryNote.CustomerDeliveryNoteDelStatus = true;

                deliveryNoteDetails = deliveryNoteDetails.Select((k) =>
                {
                    k.CustomerDeliveryNoteDetailsDelStatus = true;
                    return k;
                }).ToList();

                deliveryNote.CustomerDeliveryNoteDetails = deliveryNoteDetails;

                _deliveryNoteRepository.Update(deliveryNote);

                _deliveryNoteRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _deliveryNoteRepository.TransactionRollback();
                i = 0;
                throw ex;
            }

            return i;
        }
        public DeliveryNoteModel InsertDeliveryNote(CustomerDeliveryNote deliveryNote, List<CustomerDeliveryNoteDetails> deliveryNoteDetails)
        {
            try
            {
                _deliveryNoteRepository.BeginTransaction();

                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _deliveryNoteRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.CustomerDeliveryNoteDeliveryID) + 1);

                deliveryNote.CustomerDeliveryNoteDeliveryID = maxcount;

                deliveryNote.CustomerDeliveryNoteDetails.Clear();

                int max1count = 0;
                max1count =
                    _deliveryNoteDetailsRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : (int)o.CustomerDeliveryNoteDetailsDetId) + 1;
                foreach (var item in deliveryNoteDetails)
                {
                    item.CustomerDeliveryNoteDetailsDetId = max1count;
                    item.CustomerDeliveryNoteDetailsDeliveryNo = maxcount;
                    deliveryNote.CustomerDeliveryNoteDetails.Add(item);
                    max1count++;
                }

                _deliveryNoteRepository.Insert(deliveryNote);

                List<StockRegister> stockRegister = new List<StockRegister>();
                foreach (var item in deliveryNoteDetails)
                {
                    stockRegister.Add(new StockRegister
                    {
                        StockRegisterMaterialID = (int)item.CustomerDeliveryNoteDetailsItemId,
                        StockRegisterQuantity = item.CustomerDeliveryNoteDetailsQty,
                        StockRegisterSIN = item.CustomerDeliveryNoteDetailsQty,
                        StockRegisterVoucherDate = deliveryNote.CustomerDeliveryNoteDeliveryDate,
                        StockRegisterTransType = VoucherType.DebitNote_TYPE,
                        StockRegisterStatus = AccountStatus.Approved,
                    });
                }
                _stockRegisterRepository.InsertList(stockRegister);

                _deliveryNoteRepository.TransactionCommit();

                return this.GetSavedDeliveryNoteDetails(deliveryNote.CustomerDeliveryNoteDeliveryID);

            }
            catch (Exception ex)
            {
                _deliveryNoteRepository.TransactionRollback();
                throw ex;
            }

        }
        public DeliveryNoteModel GetSavedDeliveryNoteDetails(long pvno)
        {
            DeliveryNoteModel deliveryNoteModel = new DeliveryNoteModel();

            deliveryNoteModel.deliveryNote = _deliveryNoteRepository.GetAsQueryable().Where(k => k.CustomerDeliveryNoteDeliveryID == pvno).SingleOrDefault();
            deliveryNoteModel.deliveryNoteDetails = _deliveryNoteDetailsRepository.GetAsQueryable().Where(x => x.CustomerDeliveryNoteDetailsDeliveryNo == pvno && (x.CustomerDeliveryNoteDetailsDelStatus == false || x.CustomerDeliveryNoteDetailsDelStatus == null)).ToList();
            return deliveryNoteModel;
        }

        public CustomerDeliveryNote GetSavedDeliveryNoteDetailsV2(long pvno)
        {
            CustomerDeliveryNote deliveryNote = new CustomerDeliveryNote();
            deliveryNote = _deliveryNoteRepository.GetAsQueryable().Where(k => k.CustomerDeliveryNoteDeliveryID == pvno).SingleOrDefault();
            deliveryNote.CustomerDeliveryNoteDetails = _deliveryNoteDetailsRepository.GetAsQueryable().Where(x => x.CustomerDeliveryNoteDetailsDeliveryNo == pvno && (x.CustomerDeliveryNoteDetailsDelStatus == false || x.CustomerDeliveryNoteDetailsDelStatus == null)).ToList();
            return deliveryNote;
        }
        public IEnumerable<CustomerDeliveryNote> GetDeliveryNote()
        {

            var customerDeliveryNotes = _deliveryNoteRepository.GetAll().Where(a => a.CustomerDeliveryNoteDelStatus == false || a.CustomerDeliveryNoteDelStatus == null).ToList();
            return customerDeliveryNotes;
        }

        public async Task<Response<List<DNReportDetails>>> DeliveryNoteReportDetails(DeliveryNoteReportFilter filter)
        {
            List<DNReportDetails> reportDetails = new List<DNReportDetails>();
            string filteredValue = " && 1 == 1";

            try
            {

                if (filter.LocationId > 0)
                {
                    filteredValue += $" && CustomerDeliveryNoteLocationID == {filter.LocationId} ";
                }
                if (filter.CustomerId > 0)
                {
                    filteredValue += $" && CustomerDeliveryNoteCustomerCode == {filter.CustomerId} ";
                }
                if (!string.IsNullOrEmpty(filter.fromDate) && !string.IsNullOrEmpty(filter.toDate))
                {
                    filteredValue += $" && CustomerDeliveryNoteDeliveryDate >= {Convert.ToDateTime(filter.fromDate)} && CustomerDeliveryNoteDeliveryDate <= {Convert.ToDateTime(filter.toDate)} ";
                }
                reportDetails = await (from dn in _deliveryNoteRepository.GetAsQueryable().Where($"1 == 1  {filteredValue}")
                                       join lm in _locationMasterRepo.GetAsQueryable() on dn.CustomerDeliveryNoteLocationID equals (long)lm.LocationMasterLocationId into lmGroup
                                       from lm in lmGroup.DefaultIfEmpty()
                                       join dnd in _deliveryNoteDetailsRepository.GetAsQueryable() on dn.CustomerDeliveryNoteDeliveryID equals (long)dnd.CustomerDeliveryNoteDetailsDeliveryDetailId into dndGroup
                                       from dnd in dndGroup.DefaultIfEmpty()
                                       select new DNReportDetails()
                                       {
                                           DaliveryDate = dn.CustomerDeliveryNoteDeliveryDate,
                                           CustomerName = dn.CustomerDeliveryNoteCustomerName,
                                           DeliverySatus = dn.CustomerDeliveryNoteDeliveryStatus,
                                           DelId = dn.CustomerDeliveryNoteDeliveryID,
                                           Location = lm.LocationMasterLocationName,
                                           ItemList = new List<DNItemList>
                                        {
                                            new DNItemList
                                            {
                                              Description   = dnd.CustomerDeliveryNoteDetailsDescription,
                                              Qty  = dnd.CustomerDeliveryNoteDetailsQty,
                                              MaterialId = dnd.CustomerDeliveryNoteDetailsItemId

                                            }
                                        },


                                       }).OrderBy(x => x.DaliveryDate).ToListAsync();


                return new Response<List<DNReportDetails>>
                {
                    Valid = true,
                    Result = reportDetails,
                    Message = "Delivery Note Data Fonud"
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public CustomerDeliveryNote InsertCustomerDeliveryNote(CustomerDeliveryNote deliveryNote, List<CustomerDeliveryNoteDetails> deliveryNoteDetails)
        {
            try
            {
                _deliveryNoteRepository.BeginTransaction();

                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _deliveryNoteRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.CustomerDeliveryNoteDeliveryID) + 1);

                deliveryNote.CustomerDeliveryNoteDeliveryID = maxcount;

                deliveryNoteDetails = deliveryNoteDetails.Select((x) =>
                {
                    x.CustomerDeliveryNoteDetailsDeliveryDetailId = maxcount;
                    return x;
                }).ToList();
                _deliveryNoteDetailsRepository.InsertList(deliveryNoteDetails);

                _deliveryNoteRepository.Insert(deliveryNote);

                List<StockRegister> stockRegister = new List<StockRegister>();
                foreach (var item in deliveryNoteDetails)
                {
                    stockRegister.Add(new StockRegister
                    {
                        StockRegisterMaterialID = (int)item.CustomerDeliveryNoteDetailsItemId,
                        StockRegisterQuantity = item.CustomerDeliveryNoteDetailsQty,
                        StockRegisterSIN = item.CustomerDeliveryNoteDetailsQty,
                        StockRegisterVoucherDate = deliveryNote.CustomerDeliveryNoteDeliveryDate,
                        StockRegisterTransType = VoucherType.DebitNote_TYPE,
                        StockRegisterStatus = AccountStatus.Approved,
                    });
                }
                _stockRegisterRepository.InsertList(stockRegister);

                _deliveryNoteRepository.TransactionCommit();

                return this.GetSavedDeliveryNoteDetailsV2(deliveryNote.CustomerDeliveryNoteDeliveryID);

            }
            catch (Exception ex)
            {
                _deliveryNoteRepository.TransactionRollback();
                throw ex;
            }

        }

        public CustomerDeliveryNote UpdateCustomerDeliveryNote(CustomerDeliveryNote deliveryNote, List<CustomerDeliveryNoteDetails> deliveryNoteDetails)
        {
            throw new NotImplementedException();
        }
    }
}
