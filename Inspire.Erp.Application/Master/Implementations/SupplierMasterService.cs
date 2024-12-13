using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.DTO.Job_Master;
using Inspire.Erp.Domain.DTO.Supplier;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Master

{
    public class SupplierMasterService : ISupplierMasterService
    {
        private readonly IRepository<SuppliersMaster> supplierrepository;
        private readonly IRepository<PurchaseVoucherDetails> pVoucherDetailsrepo;
        private readonly IRepository<PurchaseVoucher> pVoucherrepo;
        private readonly IRepository<MasterAccountsTable> _masterAccountTable;
        private readonly IRepository<CityMaster> _cityMasterrepository;
        private readonly IRepository<CountryMaster> _counrtyMasterrepository;
        private readonly IUtilityService _utilityService;
        private string SupplierRelativeNo = "";
        public SupplierMasterService(IRepository<SuppliersMaster> _supplierrepository, IRepository<PurchaseVoucher> _pVoucherrepo,
            IUtilityService utilityService, IConfiguration config,
                                     IRepository<MasterAccountsTable> masterAccountTable, IRepository<PurchaseVoucherDetails> _pVoucherDetailsrepo,
                                     IRepository<CityMaster> cityMasterrepository,
                                     IRepository<CountryMaster> counrtyMasterrepository)
        {
            supplierrepository = _supplierrepository;
            pVoucherDetailsrepo = _pVoucherDetailsrepo;
            pVoucherrepo = _pVoucherrepo;
            _masterAccountTable = masterAccountTable;
            _utilityService = utilityService;
            SupplierRelativeNo = config["ApplicationSettings:SupplierRelativeNo"];
            _cityMasterrepository = cityMasterrepository;
            _counrtyMasterrepository = counrtyMasterrepository;
        }
        public async Task<SuppliersMaster> InsertSupplier(SuppliersMaster SuppliersMaster)
        {
            bool valid = false;
            try
            {
                var fs = await _utilityService.GetFinancialPeriods();
                valid = true;
                int mxc = 0;
                var maxId = _masterAccountTable.GetAsQueryable().Max(x => x.MaSno);

                var masterAccount = _masterAccountTable.GetAsQueryable().Where(x => x.MaAccNo == SupplierRelativeNo).FirstOrDefault();
                var suppAccNo = string.Empty;
                MasterAccountsTable masterAccountsTable = null;
                var count = _masterAccountTable.GetAsQueryable().AsNoTracking().Where(x => x.MaRelativeNo == SupplierRelativeNo).ToList().Count() + 1;
                // suppAccNo = count.ToString().Length == 1 ? "00" + count : count.ToString().Length == 2 ? "0" + count : count.ToString();
                suppAccNo = count.ToString("D3");
                suppAccNo = masterAccount.MaAccNo + suppAccNo;


                var isunique = _masterAccountTable.GetAsQueryable().AsNoTracking().Where(x => x.MaAccNo == suppAccNo).FirstOrDefault();
                while (isunique != null)
                {
                    // Increment the count and regenerate the cusAccNo
                    count++;
                    suppAccNo = count.ToString("D3");
                    suppAccNo = masterAccount.MaAccNo + suppAccNo;

                    // Check if the new cusAccNo is unique
                    isunique = _masterAccountTable.GetAsQueryable().AsNoTracking()
                                                  .Where(x => x.MaAccNo == suppAccNo)
                                                  .FirstOrDefault();
                }
                masterAccountsTable = new MasterAccountsTable()
                {
                    MaAccName = SuppliersMaster.SuppliersMasterSupplierName,
                    MaAccNo = suppAccNo,
                    MaAccType = "A",
                    MaImageKey = masterAccount.MaImageKey,
                    MaAssetDate = DateTime.Now,
                    MaFsno = fs.Result.FinancialPeriodsFsno,
                    MaMainHead = masterAccount.MaMainHead,
                    MaRelativeNo = masterAccount.MaAccNo,
                    MaSubHead = masterAccount.MaSubHead,
                    MaUserId = 0,
                    MaSno = maxId + 1,
                    MaDateCreated = DateTime.Now,
                    MaStatus = "R",
                };

                if (masterAccountsTable != null)
                {
                    await _utilityService.SaveMasterAccountTable(masterAccountsTable);
                }


                //mxc = (int)supplierrepository.GetAsQueryable().Where(k => k.SuppliersMasterSupplierId != null).Select(x => x.SuppliersMasterSupplierId).Max();
                //if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }
                var query = supplierrepository.GetAsQueryable().AsNoTracking().Where(k => k.SuppliersMasterSupplierId != null)
                       .Select(x => x.SuppliersMasterSupplierId);
                if (query.Any())
                {
                    mxc = query.Max();
                }
                mxc = mxc == null ? 1 : mxc + 1;

                SuppliersMaster.SuppliersMasterSupplierId = mxc;
                SuppliersMaster.SuppliersMasterSupplierReffAcNo = suppAccNo;
                supplierrepository.Insert(SuppliersMaster);



                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel log = new AddActivityLogViewModel()
                {
                    Page = "Supplier Master",
                    Section = "Add Supplier Master",
                    Entity = "Supplier Master",

                };
                await _utilityService.AddUserTrackingLog(log);
                #endregion

                _masterAccountTable.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //currencyrepository.Dispose();
            }
            return this.GetAllSupplierById(SuppliersMaster.SuppliersMasterSupplierId);
        }
        public SuppliersMaster UpdateSupplier(SuppliersMaster SuppliersMaster)
        {
            bool valid = false;
            try
            {
                supplierrepository.Update(SuppliersMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //currencyrepository.Dispose();
            }
            return this.GetAllSupplierById(SuppliersMaster.SuppliersMasterSupplierId);
        }
        public SuppliersMaster DeleteSupplier(SuppliersMaster SuppliersMaster)
        {
            bool valid = false;
            try
            {
                supplierrepository.Delete(SuppliersMaster);
                var account = _masterAccountTable.GetAsQueryable().Where(a => a.MaAccNo == SuppliersMaster.SuppliersMasterSupplierReffAcNo).FirstOrDefault();
                if(account != null)
                {
                    _masterAccountTable.Delete(account);
                }
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //currencyrepository.Dispose();
            }
            return SuppliersMaster;
        }

        public IEnumerable<SuppliersMaster> GetAllSupplier()
        {
            IEnumerable<SuppliersMaster> SuppliersMaster;
            try
            {
                SuppliersMaster = supplierrepository.GetAll().Where(a => a.SuppliersMasterSupplierDelStatus != true).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //currencyrepository.Dispose();
            }
            return SuppliersMaster;
        }
        public SuppliersMaster GetAllSupplierById(int id)
        {
            SuppliersMaster SuppliersMaster;
            try
            {
                SuppliersMaster = supplierrepository.GetAsQueryable().Where(k => k.SuppliersMasterSupplierId == id).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return SuppliersMaster;

        }

        public IEnumerable<ItemMasterSupplierDetais> GetUpdatedSupplierDetailsByItem(int itemId)
        {
            //Select max(Pur_dt),MAX(PD.rate) as Rate,(isnull(S.INS_Code, '') + ' :: ' + S.INS_Name) as INS_Name from Purchase_Voucher_Details PD INNER JOIN
            //Purchase_Voucher P ON P.purchaseid = PD.purchaseid INNER JOIN Suppliers S ON P.Sp_Id = S.INS_ID Where mat_id = (Selectiing Items ItemID)

            IEnumerable<ItemMasterSupplierDetais> supplierDetails = (from PD in pVoucherDetailsrepo.GetAsQueryable()
                                                                     join P in pVoucherrepo.GetAsQueryable()
                                                                     on PD.PurchaseVoucherDetailsGrnNo equals P.PurchaseVoucherDayBookno
                                                                     join S in supplierrepository.GetAsQueryable()
                                                                     //on P.PurchaseVoucherSPID equals S.SuppliersMasterSupplierId
                                                                     on P.PurchaseVoucherSPID equals S.SuppliersMasterSupplierId
                                                                     where PD.PurchaseVoucherDetailsMaterialId == itemId
                                                                     select new
                                                                     {
                                                                         Supplier = S.SuppliersMasterSupplierName,
                                                                         SupplierId = S.SuppliersMasterSupplierId,
                                                                         PurchaseDate = P.PurchaseVoucherPurchaseDate,
                                                                         Rate = PD.PurchaseVoucherDetailsRate
                                                                     }).GroupBy(arg => new
                                                                     {
                                                                         arg.Supplier,
                                                                         arg.SupplierId
                                                                     }).Select(grouping => new ItemMasterSupplierDetais
                                                                     {
                                                                         Supplier = grouping.Key.Supplier,
                                                                         SupplierId = (int)grouping.Key.SupplierId,
                                                                         PurchaseDate = Convert.ToDateTime(grouping.Max(arg => arg.PurchaseDate)),
                                                                         Rate = (float)grouping.Max(arg => arg.Rate)
                                                                     });


            return supplierDetails;
        }

        public async Task<Response<List<SupplierSearchListDto>>> GetSupplierFilteredList(string supplierName)
        {
            List<SupplierSearchListDto> reportDetails = new List<SupplierSearchListDto>();
            string filteredValue = " && 1 == 1";

            try
            {
                if (!string.IsNullOrEmpty(supplierName) && !string.IsNullOrEmpty(supplierName))
                {
                    filteredValue += $" && SuppliersMasterSupplierName == \"{supplierName}\" ";
                }

                reportDetails = await (from s in supplierrepository.GetAsQueryable().Where($"1 == 1  {filteredValue}")
                                       join c in _cityMasterrepository.GetAsQueryable() on s.SuppliersMasterSupplierCityId equals c.CityMasterCityId into scGroup
                                       from c in scGroup.DefaultIfEmpty()
                                       join cntry in _counrtyMasterrepository.GetAsQueryable() on s.SuppliersMasterSupplierCountryId equals cntry.CountryMasterCountryId into scntryGroup
                                       from cntry in scntryGroup.DefaultIfEmpty()
                                       select new SupplierSearchListDto()
                                       {
                                           ContactPerson = s.SuppliersMasterSupplierContactPerson,
                                           SupplierName = s.SuppliersMasterSupplierName,
                                           VatNo = s.SuppliersMasterSupplierVatNo,
                                           MobileNo = s.SuppliersMasterSupplierMobile,
                                           Telephone = s.SuppliersMasterSupplierTel1,
                                           CityName = c.CityMasterCityName,
                                           CountryName = cntry.CountryMasterCountryName
                                       }).ToListAsync();

                return new Response<List<SupplierSearchListDto>>
                {
                    Valid = true,
                    Result = reportDetails,
                    Message = "Job Cost Data Fonud"
                };
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
