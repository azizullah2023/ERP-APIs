using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.MODULE;
using Inspire.Erp.Application.StoreWareHouse.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.StoreWareHouse.Implementation
{
    public class DamageEntry : IDamageEntry
    {
        //private readonly IRepository<DamageEntryResponse> _DamageEntryResponse;
        private readonly IRepository<DamageMaster> _DamageMasterRepo;
        private readonly IRepository<DamageDetails> _DamageDetailsRepo;
        private readonly IRepository<VouchersNumbers> _VouchersNumbersRepo;
        private readonly IRepository<StockRegister> _StockRegisterRepo;
        private readonly IRepository<ProgramSettings> _programsettingsRepository;
        private readonly IRepository<ItemMaster> _itemMasterRepository;
        private readonly IRepository<LocationMaster> _locationMasterRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<UnitDetails> _UnitDetailsRepository;
        private static string prefix;
        public DamageEntry(IRepository<DamageDetails> _DetailsRepo,
            IRepository<VouchersNumbers> _VNrepo,
            IRepository<StockRegister> _SRrepo,
            IRepository<DamageMaster> _rep, IRepository<UnitDetails> unitDetailsRepository,
            IRepository<ProgramSettings> programsettingsRepository,
            IRepository<ItemMaster> itemMasterRepository,
            IRepository<LocationMaster> locationMasterRepository,
            IRepository<VouchersNumbers> voucherNumbersRepository,
            IRepository<StockRegister> stockRegisterRepository)
        {
            _programsettingsRepository = programsettingsRepository;
            _VouchersNumbersRepo = _VNrepo;
            _StockRegisterRepo = _SRrepo;
            _DamageMasterRepo = _rep;
            _DamageDetailsRepo = _DetailsRepo; _UnitDetailsRepository = unitDetailsRepository;
            _itemMasterRepository = itemMasterRepository;
            _locationMasterRepository = locationMasterRepository;
            _voucherNumbersRepository = voucherNumbersRepository;
            _stockRegisterRepository = stockRegisterRepository;
        }
        //public VouchersNumbers DamageEntryPrefix()
        //{
        //    try
        //    {
        //        prefix = _programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.DE_Prefix).FirstOrDefault().ProgramSettingsTextValue;
        //        int vnoMaxVal = Convert.ToInt32(_VouchersNumbersRepo.GetAsQueryable()
        //                                                .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.DamageEntry_TYPE)
        //                                                .DefaultIfEmpty()
        //                                                .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;


        //        //var prefix = "DE";
        //        //int vnoMaxVal = 1;


        //        VouchersNumbers vouchersNumbers = new VouchersNumbers
        //        {
        //            VouchersNumbersVNo = prefix + vnoMaxVal,
        //            VouchersNumbersVNoNu = vnoMaxVal,
        //            VouchersNumbersVType = VoucherType.DamageEntry_TYPE,
        //            VouchersNumbersFsno = 1,
        //            VouchersNumbersStatus = AccountStatus.Pending,
        //            VouchersNumbersVoucherDate = DateTime.Now,

        //        };
        //        _VouchersNumbersRepo.Insert(vouchersNumbers);
        //        return vouchersNumbers;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        //public IEnumerable<DamageEntryModel> DamageEntryResponse()
        //{
        //    try
        //    {
        //        var data = (from dm in _DamageMasterRepo.GetAsQueryable()
        //                    join dm_dt in _DamageDetailsRepo.GetAsQueryable()
        //                    on dm.DamageMasterVoucherNumber equals dm_dt.DamageDetailsVoucherNumber
        //                    join stk in _StockRegisterRepo.GetAsQueryable()
        //                    on dm.DamageMasterVoucherNumber equals stk.StockRegisterRefVoucherNo
        //                    join itmMaster in _itemMasterRepository.GetAsQueryable()
        //                    on stk.StockRegisterMaterialID equals Convert.ToInt32(itmMaster.ItemMasterItemId)
        //                    join loc in _locationMasterRepository.GetAsQueryable()
        //                    on dm.DamageMasterLocationId equals loc.LocationMasterLocationId
        //                    where (dm.DamageMasterDelStatus == false)
        //                    select new DamageEntryModel
        //                    {
        //                        LocationMasterLocationName = loc.LocationMasterLocationName,
        //                        ItemMasterItemId = itmMaster.ItemMasterItemId,
        //                        ItemMasterItemName = itmMaster.ItemMasterItemName,
        //                        DamageDetailsVoucherNumber = dm_dt.DamageDetailsVoucherNumber,
        //                        StockRegisterMaterialId = stk.StockRegisterMaterialID,
        //                        DamageMasterID = dm.DamageMasterId,
        //                        DamageMasterVdat = dm.DamageMasterVdate,
        //                        DamageMasterLocationID = dm.DamageMasterLocationId,
        //                        DamageMasterNarration = dm.DamageMasterNarration,
        //                        DamageDetailsMaterialID = dm_dt.DamageDetailsMaterialId,
        //                        DamageDetailsQTY = dm_dt.DamageDetailsQty,
        //                        DamageDetailsPrice = dm_dt.DamageDetailsPrice,
        //                        DamageDetailsAmount = dm_dt.DamageDetailsAmount,
        //                        DamageDetailsRemarks = dm_dt.DamageDetailsRemarks,
        //                        DamageDetailsUnitID = dm_dt.DamageDetailsUnitId
        //                    }).Distinct().ToList();
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public async Task<bool> submitDamageEntry(DemageRequest obj)
        //{
        //    try
        //    {
        //        _VouchersNumbersRepo.Insert(obj.voucherNumberList[0]);
        //        _DamageMasterRepo.InsertList(obj.damageMasterList);
        //        _DamageDetailsRepo.InsertList(obj.damageDetailsList);
        //        _StockRegisterRepo.InsertList(obj.stockRegisterList);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //        return false;
        //    }
        //}

        public DamageMaster InsertDamageEntry(DamageMaster DamageMaster)
        {
            try
            {


                string openingstockVoucherNumber = this.GenerateVoucherNo(DamageMaster.DamageMasterVdate.Value).VouchersNumbersVNo;
                DamageMaster.DamageMasterVoucherNumber = openingstockVoucherNumber;

                _DamageMasterRepo.BeginTransaction();
                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _DamageMasterRepo.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.DamageMasterId) + 1);
                DamageMaster.DamageMasterId = maxcount;

                _DamageMasterRepo.Insert(DamageMaster);


                int maxcount1 = 0;
                maxcount1 = Convert.ToInt32(
                    _DamageDetailsRepo.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.DamageDetailsId) + 1);
                DamageMaster.DamageDetails.ForEach(elemt =>
                {
                    elemt.DamageDetailsId = maxcount1;
                    elemt.DamageDetailsVoucherNumber = DamageMaster.DamageMasterVoucherNumber;
                    elemt.DamageMasterId = maxcount;
                    maxcount1++;
                });
                _DamageDetailsRepo.InsertList(DamageMaster.DamageDetails);

                var stockReg = new List<StockRegister>();
                foreach (var item in DamageMaster.DamageDetails)
                {
                    // Retrieve conversion type based on MaterialId and UnitId
                    var conversionType = this.getConverionTypebyUnitId(item.DamageDetailsMaterialId, item.DamageDetailsUnitId);

                    // Calculate the converted quantity
                    var convertedQuantity = (decimal)(item.DamageDetailsQty ?? 0) * conversionType;

                    // Create a new StockRegister object and add it to the stockReg list
                    stockReg.Add(new StockRegister()
                    {
                        StockRegisterPurchaseID = DamageMaster.DamageMasterVoucherNumber,
                        StockRegisterRefVoucherNo = DamageMaster.DamageMasterVoucherNumber,
                        StockRegisterVoucherDate = DamageMaster.DamageMasterVdate,
                        StockRegisterMaterialID = item.DamageDetailsMaterialId,
                        StockRegisterQuantity = convertedQuantity,
                        StockRegisterSIN = 0,
                        StockRegisterSout = convertedQuantity,
                        StockRegisterRate = (decimal)(item.DamageDetailsPrice ?? 0),
                        StockRegisterAmount = (decimal)(item.DamageDetailsPrice ?? 0) * convertedQuantity,
                        StockRegisterFCAmount = (decimal)(item.DamageDetailsPrice ?? 0) * convertedQuantity,
                        StockRegisterAssignedDate = DamageMaster.DamageMasterVdate,
                        StockRegisterStatus = AccountStatus.Approved,
                        StockRegisterTransType = VoucherType.DamageEntry_TYPE,
                        StockRegisterUnitID = item.DamageDetailsUnitId,
                        StockRegisterLocationID = DamageMaster.DamageMasterLocationId,
                        StockRegisterExpDate = null,  // Assuming no expiration date is set
                        StockRegisterDepID = 1,       // Department ID, ensure this is correct for your case
                        StockRegisterDelStatus = false
                    });
                }

                _stockRegisterRepository.InsertList(stockReg);


                _DamageMasterRepo.TransactionCommit();
                return this.GetDamageById(DamageMaster.DamageMasterId);
            }
            catch (Exception ex)
            {
                _DamageMasterRepo.TransactionRollback();
                throw ex;
            }
        }
        public DamageMaster UpdateDamageEntry(DamageMaster DamageMaster)
        {
            try
            {
                _DamageMasterRepo.BeginTransaction();

                clsCommonFunctions.Delete_OldEntryOf_StockRegister(DamageMaster.DamageMasterVoucherNumber, VoucherType.DamageEntry_TYPE, _stockRegisterRepository);

                _DamageMasterRepo.Update(DamageMaster);

                int maxcount1 = 0;
                maxcount1 = Convert.ToInt32(
                    _DamageDetailsRepo.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.DamageDetailsId) + 1);

                foreach (var item in DamageMaster.DamageDetails)
                {
                    if (item.DamageDetailsId == 0)
                    {
                        item.DamageDetailsId = maxcount1;
                        item.DamageDetailsVoucherNumber = DamageMaster.DamageMasterVoucherNumber;
                        item.DamageMasterId = DamageMaster.DamageMasterId;
                        _DamageDetailsRepo.Insert(item);
                        maxcount1++;
                    }
                    else
                    {
                        _DamageDetailsRepo.Update(item);
                    }
                }



                var stockReg = new List<StockRegister>();
                foreach (var item in DamageMaster.DamageDetails)
                {
                    // Retrieve conversion type based on MaterialId and UnitId
                    var conversionType = this.getConverionTypebyUnitId(item.DamageDetailsMaterialId, item.DamageDetailsUnitId);

                    // Calculate the converted quantity
                    var convertedQuantity = (decimal)(item.DamageDetailsQty ?? 0) * conversionType;

                    // Create a new StockRegister object and add it to the stockReg list
                    stockReg.Add(new StockRegister()
                    {
                        StockRegisterPurchaseID = DamageMaster.DamageMasterVoucherNumber,
                        StockRegisterRefVoucherNo = DamageMaster.DamageMasterVoucherNumber,
                        StockRegisterVoucherDate = DamageMaster.DamageMasterVdate,
                        StockRegisterMaterialID = item.DamageDetailsMaterialId,
                        StockRegisterQuantity = convertedQuantity,
                        StockRegisterSIN = 0,
                        StockRegisterSout = convertedQuantity,
                        StockRegisterRate = (decimal)(item.DamageDetailsPrice ?? 0),
                        StockRegisterAmount = (decimal)(item.DamageDetailsPrice ?? 0) * convertedQuantity,
                        StockRegisterFCAmount = (decimal)(item.DamageDetailsPrice ?? 0) * convertedQuantity,
                        StockRegisterAssignedDate = DamageMaster.DamageMasterVdate,
                        StockRegisterStatus = AccountStatus.Approved,
                        StockRegisterTransType = VoucherType.DamageEntry_TYPE,
                        StockRegisterUnitID = item.DamageDetailsUnitId,
                        StockRegisterLocationID = DamageMaster.DamageMasterLocationId,
                        StockRegisterExpDate = null,  // Assuming no expiration date is set
                        StockRegisterDepID = 1,       // Department ID, ensure this is correct for your case
                        StockRegisterDelStatus = false
                    });
                }
                _stockRegisterRepository.InsertList(stockReg);
                //_DamageDetailsRepo.UpdateList(DamageMaster.DamageDetails);
                _DamageMasterRepo.TransactionCommit();
                return this.GetDamageById(DamageMaster.DamageMasterId);
            }
            catch (Exception ex)
            {
                _DamageMasterRepo.TransactionRollback();
                throw ex;
            }
        }
        public DamageMaster GetDamageById(int? id)
        {
            try
            {
                var DamageMaster = new DamageMaster();
                DamageMaster = _DamageMasterRepo.GetAsQueryable().Where(x => x.DamageMasterId == id && x.DamageMasterDelStatus != true).FirstOrDefault();
                if (DamageMaster != null)
                {
                    DamageMaster.DamageDetails = _DamageDetailsRepo.GetAsQueryable().Where(x => x.DamageMasterId == id && x.DamageDetailsDelStatus != true).ToList();
                }
                return DamageMaster;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<DamageMaster> GetDamageEntry()
        {
            try
            {
                var detailList = _DamageDetailsRepo.GetAll().ToList();
                var data = _DamageMasterRepo.GetAll().Select(o => new
                {
                    DamageMaster = o,
                    _DamageDetailsRepo = detailList.Where(a => Convert.ToInt32(a.DamageDetailsId) == Convert.ToInt32(o.DamageMasterId)).ToList()
                }).ToList();
                return _DamageMasterRepo.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<DamageMaster> DeleteDamageEntry(string Id)
        {
            try
            {
                var dataObj = _DamageMasterRepo.GetAsQueryable().Where(o => o.DamageMasterVoucherNumber == Id).FirstOrDefault();
                dataObj.DamageMasterDelStatus = true;
                var detailData = _DamageDetailsRepo.GetAsQueryable().Where(o => o.DamageDetailsVoucherNumber == Id).ToList();
                detailData.ForEach(elemt =>
                {
                    elemt.DamageDetailsDelStatus = true;
                });
                _DamageMasterRepo.Update(dataObj);
                _DamageDetailsRepo.UpdateList(detailData);
                return _DamageMasterRepo.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private decimal getConverionTypebyUnitId(int? itemid, int? unitDetailsid)
        {
            try
            {
                return (decimal)_UnitDetailsRepository.GetAsQueryable().FirstOrDefault(x => x.UnitDetailsItemId == itemid && x.UnitDetailsUnitId == unitDetailsid).UnitDetailsConversionType;
            }
            catch
            {
                return 1;
            }
        }
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.DE_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.DamageEntry_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;


                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.DamageEntry_TYPE,
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
        public DamageMaster GetDamageEntryVoucherNo(string VNo)
        {
            try
            {

                DamageMaster Damageentry = new DamageMaster();
                Damageentry = _DamageMasterRepo.GetAsQueryable().Where(k => k.DamageMasterVoucherNumber == VNo).SingleOrDefault();
                Damageentry.DamageDetails = _DamageDetailsRepo.GetAsQueryable().Where(x => x.DamageDetailsVoucherNumber == VNo && (x.DamageDetailsDelStatus == false || x.DamageDetailsDelStatus == null)).ToList();
                return Damageentry;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
