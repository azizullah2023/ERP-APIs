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
using System.Threading.Tasks;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models.Sales.DeliveryNoteReport;
using System.Linq.Dynamic.Core;
using Inspire.Erp.Domain.DTO.Customer_Enquiry;

namespace Inspire.Erp.Application.Sales.Implementations
{
    public class CustomerEnquiryService : ICustomerEnquiryService
    {
        private IRepository<EnquiryMaster> _customerEnquiryRepository;
        private IRepository<EnquiryDetails> _customerEnquiryDetailsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<CustomerMaster> _customerMasterRepository;
        private IRepository<LocationMaster> _locationMasterRepository;
        private IRepository<EnquiryAbout> _enquiryAboutRepository;
        private IRepository<EnquiryStatus> _enquiryStatusRepository;
        private IRepository<BusinessTypeMaster> _businessTypeRepository;
        private IRepository<CurrencyMaster> _currenyMasterRepository;
        private readonly ILogger<CustomerEnquiryService> _logger;

        public CustomerEnquiryService(ILogger<CustomerEnquiryService> logger,
            IRepository<EnquiryMaster> customerEnquiryRepository, IRepository<VouchersNumbers> voucherNumbersRepository, IRepository<ProgramSettings> programsettingsRepository,
            IRepository<EnquiryDetails> customerEnquiryDetailsRepository,
            IRepository<CustomerMaster> customerMasterRepository,
            IRepository<LocationMaster> locationMasterRepository,
            IRepository<EnquiryAbout> enquiryAboutRepository,
            IRepository<EnquiryStatus> enquiryStatusRepository,
            IRepository<BusinessTypeMaster> businessTypeRepository,
            IRepository<CurrencyMaster> currenyMasterRepository
            )
        {

            this._customerEnquiryRepository = customerEnquiryRepository;
            this._customerEnquiryDetailsRepository = customerEnquiryDetailsRepository;
            this._programsettingsRepository = programsettingsRepository;
            this._voucherNumbersRepository = voucherNumbersRepository;
            _logger = logger;
            this._locationMasterRepository = locationMasterRepository;
            this._currenyMasterRepository = currenyMasterRepository;
            this._customerMasterRepository = customerMasterRepository;
            this._enquiryAboutRepository = enquiryAboutRepository;
            this._enquiryStatusRepository = enquiryStatusRepository;
            this._businessTypeRepository = businessTypeRepository;

        }

        public CustomerEnquiryModel UpdateCustomerEnquiry(EnquiryMaster customerEnquiry, List<EnquiryDetails> customerEnquiryDetails)
        {

            try
            {
                _customerEnquiryRepository.BeginTransaction();
                _customerEnquiryRepository.Update(customerEnquiry);

                foreach (var item in customerEnquiryDetails)
                {
                    item.EnquiryDetailsEnquiryDetailsEquiryId = customerEnquiry.EnquiryMasterEnquiryId;

                    if (item.EnquiryDetailsEnquiryDetailsId != 0)
                    {
                        _customerEnquiryDetailsRepository.Update(item);
                    }
                    else
                    {
                        _customerEnquiryDetailsRepository.Insert(item);
                    }

                }
                //_customerEnquiryDetailsRepository.UpdateList(customerEnquiryDetails);
                _customerEnquiryRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _customerEnquiryRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedCustEnquiryDetails(customerEnquiry.EnquiryMasterEnquiryId);
        }

        public int DeleteCustomerEnquiry(EnquiryMaster customerEnquiry, List<EnquiryDetails> customerEnquiryDetails)
        {
            int i = 0;
            try
            {
                _customerEnquiryRepository.BeginTransaction();

                customerEnquiry.EnquiryMasterDelStatus = true;

                customerEnquiryDetails = customerEnquiryDetails.Select((k) =>
                {
                    k.EnquiryDetailsEnquiryDetailsDelStatus = true;
                    return k;
                }).ToList();

                _customerEnquiryDetailsRepository.UpdateList(customerEnquiryDetails);

                _customerEnquiryRepository.Update(customerEnquiry);



                _customerEnquiryRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _customerEnquiryRepository.TransactionRollback();
                i = 0;
                throw ex;
            }


            return i;

        }
        public CustomerEnquiryModel InsertCustomerEnquiry(EnquiryMaster customerEnquiry, List<EnquiryDetails> customerEnquiryDetails)
        {
            try
            {
                _customerEnquiryRepository.BeginTransaction();
                string customerEnquiryNumber = (customerEnquiry.EnquiryMasterEnquiryId == null || customerEnquiry.EnquiryMasterEnquiryId.Trim() == "") ?
                                              this.GenerateVoucherNo(customerEnquiry.EnquiryMasterEnquiryDate.Value).VouchersNumbersVNo : customerEnquiry.EnquiryMasterEnquiryId;
                customerEnquiry.EnquiryMasterEnquiryId = customerEnquiryNumber;

                customerEnquiryDetails = customerEnquiryDetails.Select((x) =>
                {
                    x.EnquiryDetailsEnquiryDetailsId = 0;
                    x.EnquiryDetailsEnquiryDetailsEquiryId = customerEnquiryNumber;
                    return x;
                }).ToList();
                _customerEnquiryDetailsRepository.InsertList(customerEnquiryDetails);

                _customerEnquiryRepository.Insert(customerEnquiry);
                _customerEnquiryRepository.TransactionCommit();
                return this.GetSavedCustEnquiryDetails(customerEnquiry.EnquiryMasterEnquiryId);

            }
            catch (Exception ex)
            {
                _customerEnquiryRepository.TransactionRollback();
                throw ex;
            }
        }

        public IEnumerable<EnquiryMaster> GetCustomerEnquiry()
        {
            IEnumerable<EnquiryMaster> customerEnquiry = _customerEnquiryRepository.GetAll().Where(k => k.EnquiryMasterDelStatus == false || k.EnquiryMasterDelStatus == null);
            return customerEnquiry;
        }
        public CustomerEnquiryModel GetSavedCustEnquiryDetails(string Qtno)
        {
            if (_voucherNumbersRepository.GetAsQueryable().Any(x => x.VouchersNumbersVNo == Qtno))
            {
                CustomerEnquiryModel voucherdata = _voucherNumbersRepository.GetAsQueryable().Where(x => x.VouchersNumbersVNo == Qtno)
                                                .Include(k => k.EnquiryDetails).AsEnumerable()
                                                .Select((k) => new CustomerEnquiryModel
                                                {
                                                    customerEnquiryDetails = k.EnquiryDetails.Where(x => x.EnquiryDetailsEnquiryDetailsDelStatus == false).ToList()
                                                })
                                                .SingleOrDefault();
                voucherdata.custEnquiry = _customerEnquiryRepository.GetAsQueryable().Where(k => k.EnquiryMasterEnquiryId == Qtno).FirstOrDefault();
                return voucherdata;

            }
            return null;

        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                ////var vno=  this._paymentVoucherRepository.GetAsQueryable().Where(k => k.PaymentVoucherVoucherNo == null).FirstOrDefault();
                ///
                ////int mxc = 0;
                ////mxc = Convert.ToInt32(this._customerEnquiryRepository.GetAsQueryable().Where(k => k.EnquiryMasterEnquiryId != null).Select(x => x.EnquiryMasterEnquiryId).Max());
                ////if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }

                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.CE_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.Enquiry)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                VouchersNumbers vouchersNumbers = new VouchersNumbers


                ////VouchersNumbers vouchersNumbers = new VouchersNumbers
                {

                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    ////VouchersNumbersVNo = mxc.ToString(),
                    ////VouchersNumbersVNoNu = mxc,
                    VouchersNumbersVType = VoucherType.Enquiry,
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
                VouchersNumbers vouchersNumbers = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == Qtno && k.VouchersNumbersVType == VoucherType.Enquiry).SingleOrDefault();
                return vouchersNumbers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }

        }

        public async Task<Response<List<CustomerEnquiryReportResponse>>> GetCustomerEnquirySearch(CustomerEnquirySearchFilter filter)
        {
            List<CustomerEnquiryReportResponse> reportDetails = new List<CustomerEnquiryReportResponse>();

            try
            {

                if (filter.IsDateChecked)
                {
                    reportDetails = await (from ce in _customerEnquiryRepository.GetAsQueryable()
                        .Where(a => (filter.CustomerId == null || a.EnquiryMasterSalesManId == filter.CustomerId)
                                   && (filter.StatusId == null || a.EnquiryMasterEnquiryStatusId == filter.StatusId)
                                   && (filter.Enquiry_No == null || a.EnquiryMasterEnquiryId == filter.Enquiry_No)
                                   && (filter.fromDate == null || a.EnquiryMasterEnquiryDate >= filter.fromDate)
                                   && (filter.toDate == null || a.EnquiryMasterEnquiryDate < filter.toDate))
                                           join lm in _locationMasterRepository.GetAsQueryable() on ce.EnquiryMasterLocationId equals (long)lm.LocationMasterLocationId into lmGroup
                                           from lm in lmGroup.DefaultIfEmpty()
                                           join cm in _customerMasterRepository.GetAsQueryable() on ce.EnquiryMasterSalesManId equals cm.CustomerMasterCustomerNo into cmGroup
                                           from cm in cmGroup.DefaultIfEmpty()
                                           join bt in _businessTypeRepository.GetAsQueryable() on ce.EnquiryMasterBusineesTypeId equals bt.BusinessTypeMasterBusinessTypeId into btGroup
                                           from bt in btGroup.DefaultIfEmpty()
                                           join curr in _currenyMasterRepository.GetAsQueryable() on ce.EnquiryMasterCurrencyId equals curr.CurrencyMasterCurrencyId into currGroup
                                           from curr in currGroup.DefaultIfEmpty()
                                           join abt in _enquiryAboutRepository.GetAsQueryable() on ce.EnquiryMasterEnquiryAboutId equals abt.EnquiryAboutEnquiryAboutId into abtGroup
                                           from abt in abtGroup.DefaultIfEmpty()
                                           join es in _enquiryStatusRepository.GetAsQueryable() on ce.EnquiryMasterEnquiryStatusId equals es.EnquiryStatusEnquiryStatusId into esGroup
                                           from es in esGroup.DefaultIfEmpty()

                                           select new CustomerEnquiryReportResponse()
                                           {
                                               Enquiry_Master_Enquiry_ID = ce.EnquiryMasterEnquiryId,
                                               Enquiry_Master_Enquiry_Date = ce.EnquiryMasterEnquiryDate,
                                               Enquiry_Master_Remarks = ce.EnquiryMasterRemarks,
                                               Customer_Master_Customer_Name = cm.CustomerMasterCustomerName,
                                               Location_Master_Location_Name = lm.LocationMasterLocationName,
                                               Currency_Master_Currency_Name = curr.CurrencyMasterCurrencyName,
                                               Enquiry_About_Enquiry_About = abt.EnquiryAboutEnquiryAbout,
                                               Business_Type_Master_Business_Type_Name = bt.BusinessTypeMasterBusinessTypeName,
                                               Enquiry_Master_Contact_Email = ce.EnquiryMasterContactEmail,
                                               Enquiry_Status_Enquiry_Status = es.EnquiryStatusEnquiryStatus,
                                               Enquiry_Master_Status_ID = es.EnquiryStatusEnquiryStatusId,
                                               Enquiry_Master_SaleMan_ID = ce.EnquiryMasterSalesManId

                                           }).ToListAsync();
                }
                else
                {
                    reportDetails = await (from ce in _customerEnquiryRepository.GetAsQueryable()
                        .Where(a => (filter.CustomerId == null || a.EnquiryMasterSalesManId == filter.CustomerId)
                                   && (filter.StatusId == null || a.EnquiryMasterEnquiryStatusId == filter.StatusId)
                                   && (filter.Enquiry_No == null || a.EnquiryMasterEnquiryId == filter.Enquiry_No))
                                           join lm in _locationMasterRepository.GetAsQueryable() on ce.EnquiryMasterLocationId equals (long)lm.LocationMasterLocationId into lmGroup
                                           from lm in lmGroup.DefaultIfEmpty()
                                           join cm in _customerMasterRepository.GetAsQueryable() on ce.EnquiryMasterSalesManId equals cm.CustomerMasterCustomerNo into cmGroup
                                           from cm in cmGroup.DefaultIfEmpty()
                                           join bt in _businessTypeRepository.GetAsQueryable() on ce.EnquiryMasterBusineesTypeId equals bt.BusinessTypeMasterBusinessTypeId into btGroup
                                           from bt in btGroup.DefaultIfEmpty()
                                           join curr in _currenyMasterRepository.GetAsQueryable() on ce.EnquiryMasterCurrencyId equals curr.CurrencyMasterCurrencyId into currGroup
                                           from curr in currGroup.DefaultIfEmpty()
                                           join abt in _enquiryAboutRepository.GetAsQueryable() on ce.EnquiryMasterEnquiryAboutId equals abt.EnquiryAboutEnquiryAboutId into abtGroup
                                           from abt in abtGroup.DefaultIfEmpty()
                                           join es in _enquiryStatusRepository.GetAsQueryable() on ce.EnquiryMasterEnquiryStatusId equals es.EnquiryStatusEnquiryStatusId into esGroup
                                           from es in esGroup.DefaultIfEmpty()
                                           select new CustomerEnquiryReportResponse()
                                           {
                                               Enquiry_Master_Enquiry_ID = ce.EnquiryMasterEnquiryId,
                                               Enquiry_Master_Enquiry_Date = ce.EnquiryMasterEnquiryDate,
                                               Enquiry_Master_Remarks = ce.EnquiryMasterRemarks,
                                               Customer_Master_Customer_Name = cm.CustomerMasterCustomerName,
                                               Location_Master_Location_Name = lm.LocationMasterLocationName,
                                               Currency_Master_Currency_Name = curr.CurrencyMasterCurrencyName,
                                               Enquiry_About_Enquiry_About = abt.EnquiryAboutEnquiryAbout,
                                               Business_Type_Master_Business_Type_Name = bt.BusinessTypeMasterBusinessTypeName,
                                               Enquiry_Master_Contact_Email = ce.EnquiryMasterContactEmail,
                                               Enquiry_Status_Enquiry_Status = es.EnquiryStatusEnquiryStatus,
                                               Enquiry_Master_Status_ID = es.EnquiryStatusEnquiryStatusId,
                                               Enquiry_Master_SaleMan_ID = ce.EnquiryMasterSalesManId
                                           }).ToListAsync();
                }

                return new Response<List<CustomerEnquiryReportResponse>>
                {
                    Valid = true,
                    Result = reportDetails,
                    Message = "Customer Enquiry Data Fonud"
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Response<List<CustomerEnquiryReportResponse>>> getCustomerEnquirybyStatuss(CustomerEnquirySearchFilterStatus filter)
        {
            List<CustomerEnquiryReportResponse> reportDetails = new List<CustomerEnquiryReportResponse>();

            try
            {

                if (filter.IsDateChecked)
                {
                    reportDetails = await (from ce in _customerEnquiryRepository.GetAsQueryable()
                        .Where(a => (filter.CustomerId == null || a.EnquiryMasterSalesManId == filter.CustomerId)
                                   && (filter.StatusId == null || a.EnquiryMasterEnquiryStatusId == filter.StatusId)
                                   && (filter.fromDate == null || a.EnquiryMasterEnquiryDate >= filter.fromDate)
                                   && (filter.toDate == null || a.EnquiryMasterEnquiryDate < filter.toDate))
                                           join lm in _locationMasterRepository.GetAsQueryable() on ce.EnquiryMasterLocationId equals (long)lm.LocationMasterLocationId into lmGroup
                                           from lm in lmGroup.DefaultIfEmpty()
                                           join cm in _customerMasterRepository.GetAsQueryable() on ce.EnquiryMasterSalesManId equals cm.CustomerMasterCustomerNo into cmGroup
                                           from cm in cmGroup.DefaultIfEmpty()
                                           join bt in _businessTypeRepository.GetAsQueryable() on ce.EnquiryMasterBusineesTypeId equals bt.BusinessTypeMasterBusinessTypeId into btGroup
                                           from bt in btGroup.DefaultIfEmpty()
                                           join curr in _currenyMasterRepository.GetAsQueryable() on ce.EnquiryMasterCurrencyId equals curr.CurrencyMasterCurrencyId into currGroup
                                           from curr in currGroup.DefaultIfEmpty()
                                           join abt in _enquiryAboutRepository.GetAsQueryable() on ce.EnquiryMasterEnquiryAboutId equals abt.EnquiryAboutEnquiryAboutId into abtGroup
                                           from abt in abtGroup.DefaultIfEmpty()
                                           join es in _enquiryStatusRepository.GetAsQueryable() on ce.EnquiryMasterEnquiryStatusId equals es.EnquiryStatusEnquiryStatusId into esGroup
                                           from es in esGroup.DefaultIfEmpty()

                                           select new CustomerEnquiryReportResponse()
                                           {
                                               Enquiry_Master_Enquiry_ID = ce.EnquiryMasterEnquiryId,
                                               Enquiry_Master_Enquiry_Date = ce.EnquiryMasterEnquiryDate,
                                               Enquiry_Master_Remarks = ce.EnquiryMasterRemarks,
                                               Customer_Master_Customer_Name = cm.CustomerMasterCustomerName,
                                               Location_Master_Location_Name = lm.LocationMasterLocationName,
                                               Currency_Master_Currency_Name = curr.CurrencyMasterCurrencyName,
                                               Enquiry_About_Enquiry_About = abt.EnquiryAboutEnquiryAbout,
                                               Business_Type_Master_Business_Type_Name = bt.BusinessTypeMasterBusinessTypeName,
                                               Enquiry_Master_Contact_Email = ce.EnquiryMasterContactEmail,
                                               Enquiry_Status_Enquiry_Status = es.EnquiryStatusEnquiryStatus,
                                               Enquiry_Master_Status_ID = es.EnquiryStatusEnquiryStatusId,
                                               Enquiry_Master_SaleMan_ID = ce.EnquiryMasterSalesManId

                                           }).ToListAsync();
                }
                else
                {
                    reportDetails = await (from ce in _customerEnquiryRepository.GetAsQueryable()
                        .Where(a => (filter.CustomerId == null || a.EnquiryMasterSalesManId == filter.CustomerId)
                                   && (filter.StatusId == null || a.EnquiryMasterEnquiryStatusId == filter.StatusId))
                                           join lm in _locationMasterRepository.GetAsQueryable() on ce.EnquiryMasterLocationId equals (long)lm.LocationMasterLocationId into lmGroup
                                           from lm in lmGroup.DefaultIfEmpty()
                                           join cm in _customerMasterRepository.GetAsQueryable() on ce.EnquiryMasterSalesManId equals cm.CustomerMasterCustomerNo into cmGroup
                                           from cm in cmGroup.DefaultIfEmpty()
                                           join bt in _businessTypeRepository.GetAsQueryable() on ce.EnquiryMasterBusineesTypeId equals bt.BusinessTypeMasterBusinessTypeId into btGroup
                                           from bt in btGroup.DefaultIfEmpty()
                                           join curr in _currenyMasterRepository.GetAsQueryable() on ce.EnquiryMasterCurrencyId equals curr.CurrencyMasterCurrencyId into currGroup
                                           from curr in currGroup.DefaultIfEmpty()
                                           join abt in _enquiryAboutRepository.GetAsQueryable() on ce.EnquiryMasterEnquiryAboutId equals abt.EnquiryAboutEnquiryAboutId into abtGroup
                                           from abt in abtGroup.DefaultIfEmpty()
                                           join es in _enquiryStatusRepository.GetAsQueryable() on ce.EnquiryMasterEnquiryStatusId equals es.EnquiryStatusEnquiryStatusId into esGroup
                                           from es in esGroup.DefaultIfEmpty()
                                           select new CustomerEnquiryReportResponse()
                                           {
                                               Enquiry_Master_Enquiry_ID = ce.EnquiryMasterEnquiryId,
                                               Enquiry_Master_Enquiry_Date = ce.EnquiryMasterEnquiryDate,
                                               Enquiry_Master_Remarks = ce.EnquiryMasterRemarks,
                                               Customer_Master_Customer_Name = cm.CustomerMasterCustomerName,
                                               Location_Master_Location_Name = lm.LocationMasterLocationName,
                                               Currency_Master_Currency_Name = curr.CurrencyMasterCurrencyName,
                                               Enquiry_About_Enquiry_About = abt.EnquiryAboutEnquiryAbout,
                                               Business_Type_Master_Business_Type_Name = bt.BusinessTypeMasterBusinessTypeName,
                                               Enquiry_Master_Contact_Email = ce.EnquiryMasterContactEmail,
                                               Enquiry_Status_Enquiry_Status = es.EnquiryStatusEnquiryStatus,
                                               Enquiry_Master_Status_ID = es.EnquiryStatusEnquiryStatusId,
                                               Enquiry_Master_SaleMan_ID = ce.EnquiryMasterSalesManId
                                           }).ToListAsync();
                }

                return new Response<List<CustomerEnquiryReportResponse>>
                {
                    Valid = true,
                    Result = reportDetails,
                    Message = "Customer Enquiry Data Fonud"
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<IEnumerable<EnquiryNoDto>> GetAllEnquiryNo()
        {
            try
            {
                var allEnquiryIdsStrings = await _customerEnquiryRepository.GetAsQueryable()
                    .Select(ce => new EnquiryNoDto
                    {
                        EnquiryNO = ce.EnquiryMasterEnquiryId,
                        EnquiryNOId = ce.EnquiryMasterEnquiryId
                    })
                    .ToListAsync();

                return allEnquiryIdsStrings;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching Enquiry IDs: {ex.Message}", ex);
            }
        }

    }
}
