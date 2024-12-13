using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Erp.Application.MODULE;
using Microsoft.VisualBasic;

namespace Inspire.Erp.Application.Account.Implementations
{
   public class AssetsOpeningService: IAssetsOpeningService
    {
        private readonly IRepository<VouchersNumbers> _voucherNumber;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private readonly IRepository<CurrencyMaster> _currencyMaster;
        private readonly IRepository<AssetMaster> _assetMaster;
        private readonly IRepository<FinancialPeriods> _financialPeriods;
        private readonly IRepository<AssetPurchaseVoucher> _assetopening;
        private readonly IRepository<AssetPurchaseVoucherDetails> _assetopeningDetail;
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private readonly IUtilityService _utilityService;
        private readonly string PurchaseAccNo = "EX6601";
        public AssetsOpeningService(IRepository<AssetPurchaseVoucher> assetopening, IRepository<VouchersNumbers> voucherNumber,
             IRepository<ProgramSettings> programsettingsRepository,
            IRepository<AssetPurchaseVoucherDetails> assetopeningDetail, IRepository<AssetMaster> assetMaster,
            IRepository<FinancialPeriods> financialPeriods, IRepository<CurrencyMaster> currencyMaster,
            IRepository<AccountsTransactions> accountTransactionRepository,
            IUtilityService utilityService)
        {
            _voucherNumber = voucherNumber;
            _assetopening = assetopening;
            _financialPeriods = financialPeriods;
            _assetopeningDetail = assetopeningDetail;
            _programsettingsRepository = programsettingsRepository;
            _currencyMaster = currencyMaster;
            _assetMaster = assetMaster;
            _accountTransactionRepository = accountTransactionRepository;
        }
        public async Task<Response<List<DropdownResponse>>> GetCurrencyMaster()
        {
            try
            {
                var response = new List<DropdownResponse>();
                //response.Add(new DropdownResponse()
                //{
                //    Value = "",
                //    Name = " All "
                //});
                response.AddRange(await _currencyMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Id = x.CurrencyMasterCurrencyId,
                    Name = x.CurrencyMasterCurrencyName
                }));
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<GetAssetsMasterList>>> GetAssetMaster()
        {
            try
            {
                var response = new List<GetAssetsMasterList>();
                //response.Add(new DropdownResponse()
                //{
                //    Value = "",
                //    Name = " All "
                //});
                response.AddRange(await _assetMaster.ListSelectAsync(x => 1 == 1, x => new GetAssetsMasterList
                {
                    AssetMasterAssetAccountNo = x.AssetMasterAssetAccountNo,
                    AssetMasterAssetId = x.AssetMasterAssetId,
                    AssetMasterAssetCode=x.AssetMasterAssetCode,
                    AssetMasterAssetName=x.AssetMasterAssetName,
                    AssetMasterAssetType=x.AssetMasterAssetType,
                    AssetMasterAssetDepLibAccount=x.AssetMasterAssetDepLibAccount,
                    AssetMasterAssetDepExpAccount=x.AssetMasterAssetDepExpAccount,
                     AssetMasterAssetCreatedDate=x.AssetMasterAssetCreatedDate
                }));
                return Response<List<GetAssetsMasterList>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<GetAssetsMasterList>>.Fail(new List<GetAssetsMasterList>(), ex.Message);
            }
        }
        public async Task<AssetPurchaseVoucherModel> InsertAssetPurchaseVoucher(AssetPurchaseVoucher assetPurchaseVoucher
            //, List<AccountsTransactions> accountsTransactions
            )
        {
            try
            {
                _assetopening.BeginTransaction();
                string assetPurchaseVoucherNumber = clsCommonFunctions.GenerateVoucherNo(assetPurchaseVoucher.AstPurDt
                    , VoucherType.AssetPurchaseVoucher_TYPE, Prefix.AssetPurchaseVoucher_Prefix
                    , _programsettingsRepository, _voucherNumber).VouchersNumbersVNo;

                assetPurchaseVoucher.AssetPurchaseVoucherNo = assetPurchaseVoucherNumber;
                assetPurchaseVoucher.AstPurDt = DateTime.Now;

                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _assetopening.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.AstPurID) + 1);

                assetPurchaseVoucher.AstPurID = maxcount;

                assetPurchaseVoucher.AssetPurchaseVoucherDetails = assetPurchaseVoucher.AssetPurchaseVoucherDetails.Select((x) =>
                {
                    x.AstPurDetID = 0;
                    x.AstPurchaseID = maxcount;
                    x.AssetPurchaseDetailsVoucherNo = assetPurchaseVoucherNumber;
                    return x;
                }).ToList();

                var currency = await _currencyMaster.GetAsQueryable().Where(x => x.CurrencyMasterCurrencyId == assetPurchaseVoucher.CurrencyId).FirstOrDefaultAsync();
                //accountsTransactions = accountsTransactions.Select((k) =>
                //{
                //    k.AccountsTransactionsTransDate = assetPurchaseVoucher.AstPurDt;
                //    k.AccountsTransactionsVoucherNo = assetPurchaseVoucherNumber;
                //    k.AccountsTransactionsVoucherType = VoucherType.AssetPurchaseVoucher_TYPE;
                //    k.AccountsTransactionsStatus = AccountStatus.Approved;
                //    return k;
                //}).ToList();
                //_accountTransactionRepository.InsertList(accountsTransactions);
                List<AccountsTransactions> accountsTransactions = new List<AccountsTransactions>();
                foreach (var item in assetPurchaseVoucher.AssetPurchaseVoucherDetails)
                {
                    var assetMas= await _assetMaster.GetAsQueryable().Where(k => k.AssetMasterAssetId == item.AstID).SingleOrDefaultAsync(); 
                    accountsTransactions.Add(new AccountsTransactions()
                    {


                        AccountsTransactionsAccNo = assetMas.AssetMasterAssetAccountNo,
                        AccountsTransactionsTransDate = DateTime.Now,
                        AccountsTransactionsParticulars = "ASSET PURCHASE",
                        AccountsTransactionsFcCredit = 0,
                        AccountsTransactionsCredit = 0,
                        AccountsTransactionsFcDebit = item.Amount,
                        AccountsTransactionsDebit = (decimal)(item.Amount * (decimal)currency.CurrencyMasterCurrencyRate),
                        AccountsTransactionsVoucherType = VoucherType.AssetPurchaseVoucher_TYPE,
                        AccountsTransactionsVoucherNo = assetPurchaseVoucherNumber,
                        AccountsTransactionsDescription = assetPurchaseVoucher.Description,
                        AccountsTransactionsUserId = 1,//will be modified later
                        AccountsTransactionsStatus = "A",
                        AccountsTransactionsAllocDebit = 0,
                        AccountsTransactionsAllocCredit = 0,
                        AccountsTransactionsAllocBalance = 0,
                        AccountsTransactionsFcAllocCredit = 0,
                        AccountsTransactionsFcAllocDebit = 0,
                        AccountsTransactionsFcAllocBalance = 0,
                        AccountsTransactionsTstamp = DateTime.Now,
                        AccountsTransactionsApprovalDt = DateTime.Now,
                        AccountsTransactionsFsno = (decimal)item.FSNO,
                        AccountsTransactionsLocation = item.LocationID,
                        AccountsTransactionsFcRate = (decimal)currency.CurrencyMasterCurrencyRate,
                        AccountsTransactionsCurrencyId = currency.CurrencyMasterCurrencyId,
                        AccountsTransactionsLpoNo = assetPurchaseVoucher.LPONo
                    });
                }

                ////Credit AccountsTransactions
                accountsTransactions.Add(new AccountsTransactions()
                {
                    AccountsTransactionsAccNo = "03001001001001",//temporary,
                    AccountsTransactionsTransDate = DateTime.Now,
                    AccountsTransactionsParticulars = "ASSET PURCHASE" + " ",
                    AccountsTransactionsFcCredit = assetPurchaseVoucher.NetAmount,
                    AccountsTransactionsCredit = (decimal)(assetPurchaseVoucher.NetAmount * (decimal)currency.CurrencyMasterCurrencyRate),
                    AccountsTransactionsFcDebit = 0,
                    AccountsTransactionsDebit = 0,
                    AccountsTransactionsVoucherType = VoucherType.AssetPurchaseVoucher_TYPE,
                    AccountsTransactionsVoucherNo = assetPurchaseVoucherNumber,
                    AccountsTransactionsDescription = assetPurchaseVoucher.Description,
                    AccountsTransactionsUserId = 1,//will be modified later
                    AccountsTransactionsStatus = "A",
                    AccountsTransactionsAllocDebit = 0,
                    AccountsTransactionsAllocCredit = 0,
                    AccountsTransactionsAllocBalance = assetPurchaseVoucher.AstPurchaseType == "Cash" ? assetPurchaseVoucher.NetAmount : 0,
                    AccountsTransactionsFcAllocCredit = 0,
                    AccountsTransactionsFcAllocDebit = 0,
                    AccountsTransactionsFcAllocBalance = assetPurchaseVoucher.AstPurchaseType == "Cash" ? (decimal)(assetPurchaseVoucher.NetAmount * (decimal)currency.CurrencyMasterCurrencyRate) : 0,
                    AccountsTransactionsTstamp = DateTime.Now,
                    AccountsTransactionsApprovalDt = DateTime.Now,
                    AccountsTransactionsFsno =(decimal) assetPurchaseVoucher.FSNO,
                    AccountsTransactionsLocation = assetPurchaseVoucher.LocationID,
                    AccountsTransactionsFcRate = (decimal)currency.CurrencyMasterCurrencyRate,
                    AccountsTransactionsCurrencyId = currency.CurrencyMasterCurrencyId,
                    AccountsTransactionsLpoNo = assetPurchaseVoucher.LPONo
                });

                accountsTransactions.Add(new AccountsTransactions()
                {
                    AccountsTransactionsAccNo = "AS2544",//to be changed,
                    AccountsTransactionsTransDate = DateTime.Now,
                    AccountsTransactionsParticulars = "ASSET PURCHASE" + " ",
                    AccountsTransactionsFcCredit = assetPurchaseVoucher.DisAmount,
                    AccountsTransactionsCredit = (decimal)(assetPurchaseVoucher.DisAmount * (decimal)currency.CurrencyMasterCurrencyRate),
                    AccountsTransactionsFcDebit = 0,
                    AccountsTransactionsDebit = 0,
                    AccountsTransactionsVoucherType = VoucherType.AssetPurchaseVoucher_TYPE,
                    AccountsTransactionsVoucherNo = assetPurchaseVoucherNumber,
                    AccountsTransactionsDescription = assetPurchaseVoucher.Description,
                    AccountsTransactionsUserId = 1,//will be modified later
                    AccountsTransactionsStatus = "A",
                    AccountsTransactionsAllocDebit = 0,
                    AccountsTransactionsAllocCredit = 0,
                    AccountsTransactionsAllocBalance = 0,
                    AccountsTransactionsFcAllocCredit = 0,
                    AccountsTransactionsFcAllocDebit = 0,
                    AccountsTransactionsFcAllocBalance = 0,
                    AccountsTransactionsTstamp = DateTime.Now,
                    AccountsTransactionsApprovalDt = DateTime.Now,
                    AccountsTransactionsFsno = (decimal)assetPurchaseVoucher.FSNO,
                    AccountsTransactionsLocation = assetPurchaseVoucher.LocationID,
                    AccountsTransactionsFcRate = (decimal)currency.CurrencyMasterCurrencyRate,
                    AccountsTransactionsCurrencyId = currency.CurrencyMasterCurrencyId,
                    AccountsTransactionsLpoNo = assetPurchaseVoucher.LPONo
                });
                ////End Credit AccountsTransactions
                _accountTransactionRepository.InsertList(accountsTransactions);

                _assetopening.Insert(assetPurchaseVoucher);

                _assetopening.TransactionCommit();
                return this.GetSavedAssetPurchaseVoucherDetails(assetPurchaseVoucher.AssetPurchaseVoucherNo);

            }
            catch (Exception ex)
            {
                _assetopening.TransactionRollback();
                throw ex;
            }

        }
        public AssetPurchaseVoucherModel UpdateAssetPurchaseVoucher(AssetPurchaseVoucher assetPurchaseVoucher, List<AccountsTransactions> accountsTransactions

    )
        {
            try
            {
                _assetopening.BeginTransaction();


                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(assetPurchaseVoucher.AssetPurchaseVoucherNo, VoucherType.AssetPurchaseVoucher_TYPE, _accountTransactionRepository);
             
                _assetopening.Update(assetPurchaseVoucher);
             
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsTransDate = assetPurchaseVoucher.AstPurDt;
                        k.AccountsTransactionsVoucherNo = assetPurchaseVoucher.AssetPurchaseVoucherNo;
                        k.AccountsTransactionsVoucherType = VoucherType.AssetPurchaseVoucher_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }

                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                _assetopening.Update(assetPurchaseVoucher);
                _assetopening.TransactionCommit();

            }
            catch (Exception ex)
            {
                _assetopening.TransactionRollback();
                throw ex;
            }

            return this.GetSavedAssetPurchaseVoucherDetails(assetPurchaseVoucher.AssetPurchaseVoucherNo);
        }
        public int DeleteAssetPurchaseVoucher(AssetPurchaseVoucher assetPurchaseVoucher, List<AccountsTransactions> accountsTransactions
        )
        {
            int i = 0;
            try
            {
                _assetopening.BeginTransaction();


                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(assetPurchaseVoucher.AssetPurchaseVoucherNo, VoucherType.AssetPurchaseVoucher_TYPE, _accountTransactionRepository);

                assetPurchaseVoucher.AssetPurchaseVoucherDelStatus = true;

                assetPurchaseVoucher.AssetPurchaseVoucherDetails = assetPurchaseVoucher.AssetPurchaseVoucherDetails.Select((k) =>
                {
                    k.AssetPurchaseVoucherDetailsDelStatus = true;
                    return k;
                }).ToList();

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);

                _assetopening.Update(assetPurchaseVoucher);

                var vchrnumer = _voucherNumber.GetAsQueryable().Where(k => k.VouchersNumbersVNo == assetPurchaseVoucher.AssetPurchaseVoucherNo).FirstOrDefault();

                _voucherNumber.Update(vchrnumer);

                _assetopening.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _assetopening.TransactionRollback();
                i = 0;
                throw ex;
            }

            return i;

        }
        public AssetPurchaseVoucherModel GetSavedAssetPurchaseVoucherDetails(string pvno)
        {
            AssetPurchaseVoucherModel customerPurchaseOrderModel = new AssetPurchaseVoucherModel();
            customerPurchaseOrderModel.assetPurchaseVoucher = _assetopening.GetAsQueryable().Include(x=>x.AssetPurchaseVoucherDetails).Where(k => k.AssetPurchaseVoucherNo == pvno).SingleOrDefault();
            customerPurchaseOrderModel.accountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.CustomerPurchaseOrder_TYPE && (c.AccountstransactionsDelStatus == false || c.AccountstransactionsDelStatus == null)).ToList();
            //customerPurchaseOrderModel.assetPurchaseVoucher.AssetPurchaseVoucherDetails = _assetopeningDetail.GetAsQueryable().Where(x => x.AssetPurchaseDetailsVoucherNo == pvno && (x.AssetPurchaseVoucherDetailsDelStatus == false || x.AssetPurchaseVoucherDetailsDelStatus == null)).ToList();
            return customerPurchaseOrderModel;
        }
        public IEnumerable<AssetPurchaseVoucher> GetAssetPurchaseVouchers()
        {
            IEnumerable<AssetPurchaseVoucher> customerPurchaseOrder_ALL = _assetopening.GetAll();//.Where(k => k.CustomerPurchaseOrderDelStatus == false || k.CustomerPurchaseOrderDelStatus == null);
            return customerPurchaseOrder_ALL;
        }
        public async Task<Response<GridWrapperResponse<List<GetAssetPurchaseVoucher>>>> GetAssetOpeningList(GenericGridViewModel model)
        {
            try
            {
                List<GetAssetPurchaseVoucher> response = new List<GetAssetPurchaseVoucher>();
                var orders = await _assetopening.GetAsQueryable().Where(x => 1 == 1).Skip(model.Skip).Take(model.Take).ToListAsync();
                response.AddRange(orders.Select(x => new GetAssetPurchaseVoucher
                {
                    AstPurID = x.AstPurID,
                    // AstPurchaseIDNum = x.AstPurchaseIDNum,
                  
                    ActualAmount = x.ActualAmount,
                    AstPurchaseType = x.AstPurchaseType,
                    AstPurDt = x.AstPurDt,
                    NetAmount = x.NetAmount,
                    DrAccNo = x.DrAccNo,
                    DrAmount = x.DrAmount,
                    DisAmount = x.DisAmount,
                    Description = x.Description,
                    DisPer = x.DisPer,
                }).ToList());
                var gridResponse = new GridWrapperResponse<List<GetAssetPurchaseVoucher>>();
                gridResponse.Data = response;
                gridResponse.Total = 0;
                return Response<GridWrapperResponse<List<GetAssetPurchaseVoucher>>>.Success(gridResponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<GetAssetPurchaseVoucher>>>.Fail(new GridWrapperResponse<List<GetAssetPurchaseVoucher>>(), ex.Message);
            }
        }
        public async Task<Response<bool>> AddEditAssetOpening(AddEditAssetPurchaseVoucher model)
        {
            try
            {
                string message = null;
                if (model.AstPurID == 0)
                {

                    message = await AddAssetOpening(model);

                }
                else
                {

                    message = await EditAssetOpening(model);

                }
                if (message == null)
                {
                    return Response<bool>.Fail(false, "Something went wrong. Please try again later.");
                }
                return Response<bool>.Success(true, "Changes Saved Successfully.");
            }
            catch (Exception ex)
            {

                return Response<bool>.Fail(false, ex.Message);
            }
        }
        private async Task<string> AddAssetOpening(AddEditAssetPurchaseVoucher model)
        {
            try
            {
                var maxid = _voucherNumber.GetAsQueryable();
                var financial = _financialPeriods.GetAsQueryable().FirstOrDefault();
                Random random = new Random();
                int vouhcerDetailid = random.Next();
                var Id = Convert.ToInt32(maxid.Max(x => Math.Round(Convert.ToDecimal(x.VouchersNumbersVsno))) + 1);
                AssetPurchaseVoucher order = new AssetPurchaseVoucher()
                {
                    AstPurID = Id,
                    // AstPurchaseIDNum = Id,
                    AssetPurchaseVoucherNo = "AO" + Id,
                    ActualAmount = model.ActualAmount,
                    AstPurchaseType = "Cash",
                    AstPurDt = Convert.ToDateTime(model.AstPurDt),
                    SPID = 0,
                    GRNo = "",
                    GRDate = DateTime.Now,
                    LPODate = DateTime.Now,
                    LPONo = "",
                    NetAmount = model.NetAmount,
                    TransportCost = 0,
                    Handlingcharges = 0,
                    DrAccNo = PurchaseAccNo,
                    DrAmount = model.DrAmount,
                    DisAmount = model.DisAmount,
                    FSNO = financial != null ? Convert.ToInt32(financial.FinancialPeriodsFsno) : 0,
                    Status = "P",
                    UserID = 0,
                    FcRate = 0,
                    LocationID = 0,
                    Description = model.Description,
                    DisPer = model.DisPer,
                    SupInvNo = "",
                    CurrencyId = model.CurrencyId,
                    PONO = "",

                };
                _assetopening.Insert(order);

                _assetopening.SaveChangesAsync();
                List<AssetPurchaseVoucherDetails> orderDetails = new List<AssetPurchaseVoucherDetails>();
                orderDetails.AddRange(model.AssetPurchaseVoucherDetail.Select(x => new AssetPurchaseVoucherDetails
                {
                    AstPurDetID = vouhcerDetailid,
                    AstPurchaseID = Id,
                    AssetSize = x.AssetSize,
                    AstPurDate = DateTime.Now,
                    Amount = x.Amount,
                    Quantity = x.Quantity,
                    BookValue = x.BookValue,
                    AccDepAmt = x.AccDepAmt,
                    Rate = x.Rate,
                    LifrInYrs = x.LifrInYrs,
                    DepMode = x.DepMode,
                    Colour = x.Colour,
                    AstID = x.AstID,
                    AstPurRefNo = x.AstPurRefNo,
                    BatchCode = x.BatchCode,
                }));
                _assetopeningDetail.InsertList(orderDetails);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel log = new AddActivityLogViewModel()
                {
                    Page = "Asset Opening",
                    Section = "Add Asset Opening ",
                    Entity = " Asset Opening ",

                };
                await _utilityService.AddUserTrackingLog(log);
                #endregion
                _assetopeningDetail.SaveChangesAsync();
                return "Record Added successfully.";
            }
            catch (Exception)
            {
                return null;
            }
        }
        private async Task<string> EditAssetOpening(AddEditAssetPurchaseVoucher model)
        {
            try
            {
                Random random = new Random();
                int vouhcerDetailid = random.Next();
                var orders = _assetopening.GetAsQueryable().FirstOrDefault(x => x.AstPurID == model.AstPurID);
                orders.ActualAmount = model.ActualAmount;
                // orders.AstPurchaseType = "Cash";
                //orders.AstPurDt = DateTime.Now;
                //   orders.GRDate = DateTime.Now;
                //  orders.LPODate = DateTime.Now;
                // orders.LPONo = "",
                orders.AstPurDt = Convert.ToDateTime(model.AstPurDt);
                orders.NetAmount = model.NetAmount;
                //orders.TransportCost = 0,
                // orders.Handlingcharges = 0,
                orders.DrAccNo = PurchaseAccNo;
                orders.DrAmount = model.DrAmount;
                orders.DisAmount = model.DisAmount;
                //   orders.FSNO = financial != null ? Convert.ToInt32(financial.FinancialPeriodsFsno) : 0,
                // orders.Status = "P",
                orders.UserID = 0;
                //orders.FcRate = 0,
                //orders.LocationID = 0,
                orders.Description = model.Description;
                orders.DisPer = model.DisPer;
                // orders.SupInvNo = "",
                orders.CurrencyId = model.CurrencyId;
                // orders.PONO = "",
                _assetopening.Update(orders);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel log = new AddActivityLogViewModel()
                {
                    Page = "Asset Opening",
                    Section = "Update Asset Opening ",
                    Entity = " Asset Opening ",

                };
                await _utilityService.AddUserTrackingLog(log);
                #endregion
                _assetopening.SaveChangesAsync();

                var listOrdersDetails = _assetopeningDetail.GetAsQueryable().Where(x => x.AstPurchaseID == model.AstPurID).ToList();
                _assetopeningDetail.DeleteList(listOrdersDetails);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel Deletelog = new AddActivityLogViewModel()
                {
                    Page = "Asset Opening",
                    Section = "Delete Asset Opening ",
                    Entity = " Asset Opening ",

                };
                await _utilityService.AddUserTrackingLog(Deletelog);
                #endregion
                _assetopeningDetail.SaveChangesAsync();
                List<AssetPurchaseVoucherDetails> orderDetails = new List<AssetPurchaseVoucherDetails>();
                orderDetails.AddRange(model.AssetPurchaseVoucherDetail.Select(x => new AssetPurchaseVoucherDetails
                {
                    AstPurDetID = vouhcerDetailid,
                    AstPurchaseID = model.AstPurID,
                    AssetSize = x.AssetSize,
                    AstPurDate = DateTime.Now,
                    Amount = x.Amount,
                    Quantity = x.Quantity,
                    BookValue = x.BookValue,
                    AccDepAmt = x.AccDepAmt,
                    Rate = x.Rate,
                    LifrInYrs = x.LifrInYrs,
                    DepMode = x.DepMode,
                    Colour = x.Colour,
                    AstID = x.AstID,
                    AstPurRefNo = x.AstPurRefNo,
                    BatchCode = x.BatchCode,
                }));
                _assetopeningDetail.InsertList(orderDetails);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel Detaillog = new AddActivityLogViewModel()
                {
                    Page = "Asset Opening ",
                    Section = "Update Asset Opening Detail",
                    Entity = " Asset Opening Detail",

                };
                await _utilityService.AddUserTrackingLog(Detaillog);
                #endregion
                _assetopeningDetail.SaveChangesAsync();

                return "Record Updated successfully.";
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Response<AddEditAssetPurchaseVoucher>> GetAssetOpening(int id = 0)
        {
            try
            {
                AddEditAssetPurchaseVoucher model = new AddEditAssetPurchaseVoucher();
                await Task.Run(() =>
                {
                    var orders = _assetopening.GetAsQueryable().FirstOrDefault(x => x.AstPurID == id);
                  
                    model.AstPurID = orders.AstPurID;
                    model.AstPurDt = orders.AstPurDt;
                    model.ActualAmount = orders.ActualAmount;
                    model.NetAmount = orders.NetAmount;
                    model.DrAccNo = orders.DrAccNo;
                    model.DrAmount = orders.DrAmount;
                    model.DisAmount = orders.DisAmount;
                    model.Description = orders.Description;
                    model.DisPer = orders.DisPer;
                    model.CurrencyId = orders.CurrencyId;
                    model.AssetPurchaseVoucherDetail = _assetopeningDetail.GetAsQueryable()
                        .Where(x => x.AstPurchaseID == id).Select(x => new AddEditAssetPurchaseVoucherDetail
                        {
                            AstPurDetID = x.AstPurDetID,
                            AstPurchaseID = x.AstPurchaseID,
                            AssetSize = x.AssetSize,
                            AstPurDate = x.AstPurDate,
                            Amount = x.Amount,
                            Quantity = (int)x.Quantity,
                            BookValue = x.BookValue,
                            AccDepAmt = x.AccDepAmt,
                            Rate = x.Rate,
                            LifrInYrs = x.LifrInYrs,
                            DepMode = x.DepMode,
                            Colour = x.Colour,
                            AstID =(int) x.AstID,
                            AstPurRefNo = x.AstPurRefNo,
                            BatchCode = x.BatchCode,
                        }).ToList();

                });
                return Response<AddEditAssetPurchaseVoucher>.Success(model, "Records FOund.");
            }
            catch (Exception ex)
            {
                return Response<AddEditAssetPurchaseVoucher>.Fail(new AddEditAssetPurchaseVoucher(), ex.Message);
            }
        }


    }
}
