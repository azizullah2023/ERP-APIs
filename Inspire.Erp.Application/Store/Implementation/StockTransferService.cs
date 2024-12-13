using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.MODULE;
using Inspire.Erp.Application.Store.Interface;
using Inspire.Erp.Domain.DTO;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Store.Implementation
{
    public class StockTransferService : IStockTransferService
    {
        private readonly IRepository<StockTransfer> _StockTransferRepo;
        private readonly IRepository<VouchersNumbers> _VoucherNumberRepo;
        private readonly IRepository<StockItemResponse> _StockItemResponse;
        private readonly IRepository<StockTransferResponseModel> _StockTransferResponse;
        private readonly IRepository<StockTransferDetails> _StockTransferDetailRepo;
        private readonly IRepository<StockRegister> _StockRegisterRepo;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private static string prefix;
        private InspireErpDBContext _context;
        private IRepository<ItemMaster> _itemMasterRepository;
        private readonly IUtilityService _utilityService;
        public StockTransferService(IRepository<StockTransfer> StockTransferRepo,
            IRepository<StockTransferDetails> StockTransferDetailRepo,
            IRepository<StockItemResponse> stockItemResponse,
            IRepository<StockTransferResponseModel> StockTransferResponse,
            IRepository<VouchersNumbers> VoucherNumberRepo,
            IRepository<StockRegister> StockRegisterRepo,
            IRepository<ProgramSettings> programsettingsRepository,
            InspireErpDBContext context,
            IRepository<ItemMaster> itemMasterRepository,
            IUtilityService utilityService)
        {
            _StockRegisterRepo = StockRegisterRepo;
            _VoucherNumberRepo = VoucherNumberRepo;
            _StockItemResponse = stockItemResponse;
            _StockTransferResponse = StockTransferResponse;
            _StockTransferDetailRepo = StockTransferDetailRepo;
            _StockTransferRepo = StockTransferRepo;
            _programsettingsRepository = programsettingsRepository;
            _context = context;
            prefix = _programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == "HIDE_STOCKTRANSFER").FirstOrDefault().ProgramSettingsTextValue;
            _itemMasterRepository = itemMasterRepository;
            _utilityService = utilityService;
        }
        ////////   New Function   ///////////
        public async Task<List<StockItemResponse>> getStockItemResponse()
        {
            try
            {

                var results = await (from im in _context.ItemMaster.Where(a=> a.ItemMasterDelStatus!=true)
                                          join sr in _context.StockRegister on im.ItemMasterItemId equals sr.StockRegisterMaterialID.GetValueOrDefault() into gim
                                     from y in gim.DefaultIfEmpty()
                                     
                                     where im.ItemMasterItemType == "A"
                                     select new StockItemResponse
                                     {

                                         Item_Master_Item_Id = im.ItemMasterItemId,
                                         Item_Master_Item_Name = im.ItemMasterItemName,
                                         Item_Master_Batch_Code = im.ItemMasterBatchCode,
                                         Item_Master_Part_No = im.ItemMasterPartNo,
                                         Item_Master_Barcode = im.ItemMasterBarcode,
                                         //Stock=0
                                         //Stock= itemS.Any()?itemS.Sum(y => y.StockRegisterSIN??0 - y.StockRegisterSout??0):0
                                         Stock = (y.StockRegisterSIN ?? 0 - y.StockRegisterSout ?? 0)

                                     }).ToListAsync();


                //var items = await (from s in _context.ItemMaster
                //             join sr in _context.StockRegister on s.ItemMasterItemId equals sr.StockRegisterMaterialID.GetValueOrDefault()
                //             into leftDetail
                //             from ItemStock in leftDetail.DefaultIfEmpty()
                             
                //             select new StockItemResponse()
                //             {
                //                 Item_Master_Item_Id = s.ItemMasterItemId,
                //                 Item_Master_Item_Name = s.ItemMasterItemName,
                //                 Item_Master_Batch_Code = s.ItemMasterBatchCode,
                //                 Item_Master_Part_No = s.ItemMasterPartNo,
                //                 Item_Master_Barcode = s.ItemMasterBarcode,
                //                 //Stock=0
                //                 //Stock= itemS.Any()?itemS.Sum(y => y.StockRegisterSIN??0 - y.StockRegisterSout??0):0
                //                 Stock =  leftDetail.Count(t=>t.StockRegisterSIN !=null)
                //             }).ToListAsync();


                //var itemsWStock = await Task.WhenAll(items.Select(async (osvd) =>
                //{
                //    osvd.Stock = await _utilityService.GetStockQuantity(osvd.Item_Master_Item_Id, null);
                //    return osvd;
                //}));


                //return itemsWStock.ToList();
                return results;



            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IEnumerable<StockTransfer> GetStockTransfer()
        {
            IEnumerable<StockTransfer> stockTransfer_ALL = _StockTransferRepo.GetAll().Where(k => k.StockTransferDelStatus == false || k.StockTransferDelStatus == null);
            return stockTransfer_ALL;
        }
        public async Task<StockTransferModel> InsertStockTransfer(StockTransfer stockTransfer, List<StockTransferDetails> stockTransferDetails
             , List<StockRegister> stockRegister)
        {

            //await Task.Run(() =>
            //{
                try
                {

                    _StockTransferRepo.BeginTransaction();

                    string openingstockVoucherNumber = this.GenerateVoucherNo(stockTransfer.StockTransferStDate.Value).VouchersNumbersVNo;
                    stockTransfer.StockTransferVoucherNo = openingstockVoucherNumber;


                    int maxcount = 0;
                    maxcount = Convert.ToInt32(
                        await _StockTransferRepo.GetAsQueryable()
                        .DefaultIfEmpty().MaxAsync(o => o == null ? 0 : o.StockTransferId) + 1);

                    stockTransfer.StockTransferId = maxcount;

                    //x.OpeningStockVoucherDetailsConversionType = await _utilityService.GetBasicUnitConversion(x.OpeningStockVoucherDetailsMatId, x.OpeningStockVoucherDetailsUnitId);
                    stockTransfer.StockTransferDetails = stockTransfer.StockTransferDetails.Select((x) =>
                    {
                        x.StockTransferId = maxcount;
                        x.StockTransferDetailsSNo = openingstockVoucherNumber;

                        return x;
                    }).ToList();



                   // stockTransfer.StockTransferDetails = stockTransferDetails;

                    //accountsTransactions = accountsTransactions.Select((k) =>
                    //{
                    //    //k.AccountsTransactionsTransDate = issueVoucher.OpeningStockVoucherDate;
                    //    k.AccountsTransactionsVoucherNo = openingstockVoucherNumber;
                    //    k.AccountsTransactionsVoucherType = VoucherType.OpeningStockVoucher_TYPE;
                    //    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    //    return k;
                    //}).ToList();
                    //_accountTransactionRepository.InsertList(accountsTransactions);



                    stockRegister = stockRegister.Select((k) =>
                    {
                        k.StockRegisterVoucherDate = stockTransfer.StockTransferStDate;
                        k.StockRegisterRefVoucherNo = openingstockVoucherNumber;
                        k.StockRegisterTransType = VoucherType.StockTransferVoucher_TYPE;
                        k.StockRegisterStatus = AccountStatus.Approved;
                        return k;
                    }).ToList();

                    _StockRegisterRepo.InsertList(stockRegister);

                    _StockTransferRepo.Insert(stockTransfer);
                    _StockTransferRepo.TransactionCommit();
                    return this.GetSavedStockTransferDetails(stockTransfer.StockTransferVoucherNo);
                   

                }
                catch (Exception ex)
                {
                    _StockTransferRepo.TransactionRollback();
                    throw ex;


                }

                
           // });

          //  return null;
            //try
            //{

            //    List<StockTransferDetails> std = new List<StockTransferDetails>();
            //    List<StockTransfer> st = new List<StockTransfer>();
            //    List<VouchersNumbers> vn = new List<VouchersNumbers>();
            //    List<StockRegister> sr = new List<StockRegister>();

            //    await Task.Run(() =>

            //    {
            //        for (int i = 0; i < obj.Count; i++)
            //        {
            //            st[i].StockTransferStvno = prefix + obj[i].voucherNo;
            //            st[i].StockTransferStDate = DateTime.Parse(obj[i].voucherDate);
            //            st[i].StockTransferLocationIdFrom = int.Parse(obj[i].locationFrom);
            //            st[i].StockTransferLocationIdTo = int.Parse(obj[i].locationTo);
            //            st[i].StockTransferNarration = obj[i].narration;
            //            st[i].StockTransferJobId = int.Parse(obj[i].selectedJob);
            //            st[i].StockTransferDelStatus = false;
            //            st[i].StockTransferApproved = false;

            //            vn[i].VouchersNumbersVNo = prefix + obj[i].voucherNo;
            //            vn[i].VouchersNumbersVNoNu = decimal.Parse(obj[i].voucherNo);
            //            vn[i].VouchersNumbersVoucherDate = DateTime.Parse(obj[i].voucherDate);
            //            vn[i].VouhersNumbersDelStatus = false;
            //            vn[i].VouchersNumbersStatus = "P";
            //            vn[i].VouchersNumbersLocationId = decimal.Parse(obj[i].locationTo);
            //            _VoucherNumberRepo.InsertList(vn);

            //            sr[i].StockRegisterRefVoucherNo = prefix + obj[i].voucherNo;
            //            sr[i].StockRegisterAssignedDate = DateTime.Parse(obj[i].voucherDate);
            //            sr[i].StockRegisterMaterialID = int.Parse(obj[i].itemId);
            //            sr[i].StockRegisterQuantity = int.Parse(obj[i].quantity);
            //            sr[i].StockRegisterAmount = int.Parse(obj[i].amount);
            //            sr[i].StockRegisterFCAmount = int.Parse(obj[i].amount);
            //            sr[i].StockRegisterStatus = "S";
            //            sr[i].StockRegisterTransType = "Stock Transfer";
            //            sr[i].StockRegisterRemarks = obj[i].narration;
            //            sr[i].StockRegisterLocationID = int.Parse(obj[i].locationTo);
            //            sr[i].StockRegisterUnitID = int.Parse(obj[i].unit);
            //            sr[i].StockRegisterJobID = int.Parse(obj[i].selectedJob);
            //            sr[i].StockRegisterDepID = int.Parse(obj[i].department);
            //            sr[i].StockRegisterDelStatus = false;


            //            //std[i].VoucherNumber = int.Parse(obj[i].voucherNo);
            //            std[i].StockTransferDetailsUnitId = int.Parse(obj[i].unit);
            //            std[i].StockTransferDetailsMaterialId = int.Parse(obj[i].itemId);
            //            std[i].StockTransferDetailsQty = double.Parse(obj[i].quantity);
            //            std[i].StockTransferDetailsRate = double.Parse(obj[i].amount);
            //            std[i].StockTransferDetailsRemarks = obj[i].narration;
            //            std[i].StockTransferDetailsDelStatus = false;

            //        }
            //    });


            //    _StockRegisterRepo.InsertList(sr);
            //    _StockTransferDetailRepo.InsertList(std);
            //    _StockTransferRepo.InsertList(st);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;

            //}
        }
        public IEnumerable<StockTransfer> DeleteStockTransfers(string Id)
        {
            try
            {
                var dataObj = _StockTransferRepo.GetAsQueryable().Where(o => o.StockTransferVoucherNo == Id).FirstOrDefault();
                dataObj.StockTransferDelStatus = true;
                var detailData = _StockTransferDetailRepo.GetAsQueryable().Where(o => o.StockTransferDetailsSNo == Id).ToList();
                detailData.ForEach(elemt =>
                {
                    elemt.StockTransferDetailsDelStatus = true;
                });
                _StockTransferRepo.Update(dataObj);
                _StockTransferDetailRepo.UpdateList(detailData);
                return _StockTransferRepo.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public StockTransferModel GetSavedStockTransferDetails(string pvno)
        {
            StockTransferModel issueVoucherModel = new StockTransferModel();
            //issueVoucherModel.issueVoucher = _issueVoucherRepository.GetAsQueryable().Where(k => k.IssueVoucherNo == pvno && k.IssueVoucherDelStatus == false).SingleOrDefault();

            issueVoucherModel.stockTransfer = _StockTransferRepo.GetAsQueryable().Where(k => k.StockTransferVoucherNo == pvno).SingleOrDefault();



           // issueVoucherModel.accountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.IssueVoucher_TYPE && (c.AccountstransactionsDelStatus == false || c.AccountstransactionsDelStatus == null)).ToList();
            issueVoucherModel.stockTransferDetails = _StockTransferDetailRepo.GetAsQueryable().Where(x => x.StockTransferDetailsSNo == pvno && (x.StockTransferDetailsDelStatus == false || x.StockTransferDetailsDelStatus == null)).ToList();
            return issueVoucherModel;


        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {


                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.StockTransfer_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_VoucherNumberRepo.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.StockTransferVoucher_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;



                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.StockTransferVoucher_TYPE,
                    VouchersNumbersFsno = 1,
                    VouchersNumbersStatus = AccountStatus.Pending,
                    VouchersNumbersVoucherDate = VoucherGenDate

                };
                _VoucherNumberRepo.Insert(vouchersNumbers);
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
                VouchersNumbers vouchersNumbers = _VoucherNumberRepo.GetAsQueryable().Where(k => k.VouchersNumbersVNo == pvno).SingleOrDefault();
                return vouchersNumbers;
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex.Message);
                throw ex;
            }

        }
        public IEnumerable<StockTransferResponseModel> getStockTransferReport()
        {
            try
            {
                return _StockTransferResponse.GetBySP($"EXEC getStockTransferReport");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<bool> deleteStockTransfer(StockTransferRequestModel obj)
        //{
        //    try
        //    {
        //        StockTransfer st = _StockTransferRepo.GetAsQueryable().Where(k => k.StockTransferVoucherNo == obj.voucherNo).SingleOrDefault();
        //        if (st != null)
        //        {
        //            st.StockTransferDelStatus = true;
        //            _StockTransferRepo.Update(st);
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //        VouchersNumbers vn = await _VoucherNumberRepo.GetAsQueryable().Where(k => k.VouchersNumbersVNoNu == int.Parse(obj.voucherNo)).SingleOrDefaultAsync();
        //        if (vn != null)
        //        {
        //            vn.VouhersNumbersDelStatus = true;
        //            _VoucherNumberRepo.Update(vn);
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //        StockTransferDetails std = _StockTransferDetailRepo.GetAsQueryable().Where(k => k.StockTransferDetailsVoucherNo == obj.voucherNo).SingleOrDefault();
        //        if (std != null)
        //        {
        //            std.StockTransferDetailsDelStatus = true;
        //            _StockTransferDetailRepo.Update(std);
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //        StockRegister sr = _StockRegisterRepo.GetAsQueryable().Where(k => k.StockRegisterRefVoucherNo == obj.voucherNo).SingleOrDefault();
        //        if (sr != null)
        //        {
        //            sr.StockRegisterDelStatus = true;
        //            _StockRegisterRepo.Update(sr);
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //        throw ex;
        //    }
        //}
        //public async Task<bool> UpdateStockTransfer(StockTransferRequestModel obj)
        //{
        //    StockTransfer st = await _StockTransferRepo.GetAsQueryable().Where(k => k.StockTransferVoucherNo == obj.voucherNo).SingleOrDefaultAsync();
        //    if (st != null)
        //    {
        //        st.StockTransferLocationIdFrom = int.Parse(obj.locationFrom);
        //        st.StockTransferLocationIdTo = int.Parse(obj.locationTo);
        //        st.StockTransferNarration = obj.narration;
        //        st.StockTransferJobId = int.Parse(obj.selectedJob);
        //        st.StockTransferDelStatus = false;
        //        st.StockTransferApproved = false;
        //        _StockTransferRepo.Update(st);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    VouchersNumbers vn = _VoucherNumberRepo.GetAsQueryable().Where(k => k.VouchersNumbersVNoNu == int.Parse(obj.voucherNo)).SingleOrDefault();
        //    if (vn != null)
        //    {
        //        vn.VouchersNumbersVNoNu = decimal.Parse(obj.voucherNo);
        //        vn.VouhersNumbersDelStatus = false;
        //        vn.VouchersNumbersStatus = "P";
        //        vn.VouchersNumbersLocationId = decimal.Parse(obj.locationTo);
        //        _VoucherNumberRepo.Update(vn);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    StockTransferDetails std = _StockTransferDetailRepo.GetAsQueryable().Where(k => k.StockTransferDetailsVoucherNo == obj.voucherNo).SingleOrDefault();
        //    if (std != null)
        //    {
        //        std.StockTransferDetailsUnitId = int.Parse(obj.unit);
        //        std.StockTransferDetailsMaterialId = int.Parse(obj.itemId);
        //        std.StockTransferDetailsQty = double.Parse(obj.quantity);
        //        std.StockTransferDetailsRate = double.Parse(obj.amount);
        //        std.StockTransferDetailsRemarks = obj.narration;
        //        _StockTransferDetailRepo.Update(std);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    StockRegister sr = _StockRegisterRepo.GetAsQueryable().Where(k => k.StockRegisterRefVoucherNo == obj.voucherNo).SingleOrDefault();
        //    if (sr != null)
        //    {
        //        sr.StockRegisterMaterialID = int.Parse(obj.itemId);
        //        sr.StockRegisterQuantity = int.Parse(obj.quantity);
        //        sr.StockRegisterAmount = int.Parse(obj.amount);
        //        sr.StockRegisterFCAmount = int.Parse(obj.amount);
        //        sr.StockRegisterStatus = "S";
        //        sr.StockRegisterTransType = "Stock Transfer";
        //        sr.StockRegisterRemarks = obj.narration;
        //        sr.StockRegisterLocationID = int.Parse(obj.locationTo);
        //        sr.StockRegisterUnitID = int.Parse(obj.unit);
        //        sr.StockRegisterJobID = int.Parse(obj.selectedJob);
        //        sr.StockRegisterDepID = int.Parse(obj.department);
        //        _StockRegisterRepo.Update(sr);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    return true;
        //}



        public async Task<bool> DeleteStockTransfer(StockTransfer stockTransfer, List<StockTransferDetails> stockTransferDetails
             , List<StockRegister> stockRegister
          )
        {

            //await Task.Run(() =>
            //{
            //    int i = 0;
            try
            {
                _StockTransferRepo.BeginTransaction();

                //=================================
                clsCommonFunctions.Delete_OldEntry_StockRegister(stockTransfer.StockTransferVoucherNo, VoucherType.StockTransferVoucher_TYPE, _StockRegisterRepo);
                //clsCommonFunctions.Delete_OldEntry_AccountsTransactions(stockTransfer.StockTransferVoucherNo, VoucherType.StockTransferVoucher_TYPE, _accountTransactionRepository);
                //=================================

                stockTransfer.StockTransferDelStatus = true;

                    stockTransfer.StockTransferDetails = stockTransfer.StockTransferDetails.Select((k) =>
                {
                    k.StockTransferDetailsDelStatus = true;
                    return k;
                }).ToList();

                //_issueVoucherDetailsRepository.UpdateList(issueVoucherDetails);

                //accountsTransactions = accountsTransactions.Select((k) =>
                //{
                //    k.AccountstransactionsDelStatus = true;
                //    return k;
                //}).ToList();
                //_accountTransactionRepository.UpdateList(accountsTransactions);




                stockRegister = stockRegister.Select((k) =>
                {
                    k.StockRegisterDelStatus = true;
                    return k;
                }).ToList();
                _StockRegisterRepo.UpdateList(stockRegister);




                //stockTransfer.StockTransferDetails = stockTransferDetails;

                _StockTransferRepo.UpdateAsync(stockTransfer);

                var vchrnumer =await  _VoucherNumberRepo.GetAsQueryable().Where(k => k.VouchersNumbersVNo == stockTransfer.StockTransferVoucherNo).FirstOrDefaultAsync();

                 _VoucherNumberRepo.UpdateAsync(vchrnumer);

                _StockTransferRepo.TransactionCommit();
                return true;
            }
            catch (Exception ex)
            {
                _StockTransferRepo.TransactionRollback();
               
                    throw ex;
                }

          
           
        }

        public async Task<StockTransferModel> UpdateStockTransfer(StockTransfer stockTransfer, List<StockTransferDetails> stockTransferDetails
             , List<StockRegister> stockRegister
    )
        {

            //await Task.Run(() =>
            //{
                try
                {

                    _StockTransferRepo.BeginTransaction();

                    //=================================
                    clsCommonFunctions.Delete_OldEntry_StockRegister(stockTransfer.StockTransferVoucherNo, VoucherType.IssueVoucher_TYPE, _StockRegisterRepo);
                // clsCommonFunctions.Delete_OldEntry_AccountsTransactions(stockTransfer.StockTransferVoucherNo, VoucherType.IssueVoucher_TYPE, _accountTransactionRepository);
                //=================================

                //string openingstockVoucherNumber = this.GenerateVoucherNo(stockTransfer.StockTransferStDate.Value).VouchersNumbersVNo;
                //stockTransfer.StockTransferVoucherNo = openingstockVoucherNumber;
              
                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    await _StockTransferRepo.GetAsQueryable()
                    .DefaultIfEmpty().MaxAsync(o => o == null ? 0 : o.StockTransferId) + 1);

                //stockTransfer.StockTransferId = maxcount;

                //x.OpeningStockVoucherDetailsConversionType = await _utilityService.GetBasicUnitConversion(x.OpeningStockVoucherDetailsMatId, x.OpeningStockVoucherDetailsUnitId);
                stockTransfer.StockTransferDetails = stockTransfer.StockTransferDetails.Select((x) =>
                    {
                        x.StockTransferId = stockTransfer.StockTransferId;
                        x.StockTransferDetailsSNo = stockTransfer.StockTransferVoucherNo;

                        return x;
                    }).ToList();



                    //stockTransfer.StockTransferDetails = stockTransferDetails;




                    //accountsTransactions = accountsTransactions.Select((k) =>
                    //{
                    //    //k.AccountsTransactionsTransDate = issueVoucher.OpeningStockVoucherDate;
                    //    k.AccountsTransactionsVoucherNo = openingstockVoucherNumber;
                    //    k.AccountsTransactionsVoucherType = VoucherType.OpeningStockVoucher_TYPE;
                    //    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    //    return k;
                    //}).ToList();
                    //_accountTransactionRepository.InsertList(accountsTransactions);



                    stockRegister = stockRegister.Select((k) =>
                    {
                        k.StockRegisterVoucherDate = stockTransfer.StockTransferStDate;
                        k.StockRegisterRefVoucherNo = stockTransfer.StockTransferVoucherNo;
                        k.StockRegisterTransType = VoucherType.StockTransferVoucher_TYPE;
                        k.StockRegisterStatus = AccountStatus.Approved;
                        return k;
                    }).ToList();

                _StockRegisterRepo.InsertList(stockRegister);

                _StockTransferRepo.Update(stockTransfer);
                _StockTransferRepo.TransactionCommit();
                    return this.GetSavedStockTransferDetails(stockTransfer.StockTransferVoucherNo);

                }
                catch (Exception ex)
                {
                    _StockTransferRepo.TransactionRollback();
                    throw ex;


                }


            //});
            //return null;


      
        }
    }
}