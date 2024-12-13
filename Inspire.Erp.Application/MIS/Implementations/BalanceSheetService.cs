using Inspire.Erp.Application.MIS.Interfaces;
using Inspire.Erp.Application.NewFolder.Interfaces;
using Inspire.Erp.Application.Store.Implementation;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.File;
using Inspire.Erp.Domain.Modals.MIS;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.MIS.Implementations
{
    public class BalanceSheetService : IBalanceSheetService
    {
        private readonly IRepository<FinancialPeriods> _financialPeriods;
        private readonly IRepository<AccountsTransactions> _accountTrans;
        private readonly IRepository<MasterAccountsTable> _masterAccount;
        private readonly IRepository<GetAccountTransactionBalanceSheetResponse> _balanceSheet;
        private readonly IProfitLossService _profitLoss;
        private readonly IFileService _fileService;
        public BalanceSheetService(IRepository<FinancialPeriods> financialPeriods, IRepository<GetAccountTransactionBalanceSheetResponse> balanceSheet,
            IRepository<AccountsTransactions> accountTrans, IRepository<MasterAccountsTable> masterAccount, IProfitLossService profitLoss,
            IFileService fileService)
        {
            _financialPeriods = financialPeriods;
            _masterAccount = masterAccount;
            _accountTrans = accountTrans;
            _balanceSheet = balanceSheet;
            _profitLoss = profitLoss;
            _fileService = fileService;
        }


        private  void GetBalanceSheetTotals(GenericGridViewModel model,string relativeNo, ref double totalAsset)
        {
            try
            {
                var assets = (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaRelativeNo.ToUpper() == relativeNo.ToUpper())
                                   join at in _accountTrans.GetAsQueryable()
                                                 .Where(x => x.AccountsTransactionsTransDate >= model.FromDate
                                   && x.AccountsTransactionsTransDate <= model.ToDate
                                   )
                                   on ma.MaAccNo equals at.AccountsTransactionsAccNo into g
                                   from grd in g.DefaultIfEmpty()
                                   group grd by new
                                   {
                                       grd.AccountsTransactionsAccNo,
                                       ma.MaAccName,
                                       ma.MaMainHead,
                                       ma.MaRelativeNo,
                                       ma.MaSubHead
                                   } into gp

                                   where gp.Sum(x => x.AccountsTransactionsCredit) > 0 || gp.Sum(x => x.AccountsTransactionsDebit) > 0
                                   select new BalanceSheetResponse
                                   {
                                       MA_MainHead = gp.Key.MaMainHead,
                                       MA_SubHead = gp.Key.MaSubHead,
                                       MA_AccNo = gp.Key.AccountsTransactionsAccNo,
                                       MA_AccName = gp.Key.MaAccName,
                                       MA_RelativeNo = gp.Key.MaRelativeNo,
                                       Debit = gp.Sum(pc => pc.AccountsTransactionsDebit),
                                       Credit = gp.Sum(pc => pc.AccountsTransactionsCredit)
                                   })

                      .OrderBy(t => t.MA_AccName).ToList();

                var AccountTransIds = _accountTrans.GetAsQueryable()
                                                               .Where(x => x.AccountsTransactionsTransDate >= model.FromDate
                                                 && x.AccountsTransactionsTransDate <= model.ToDate).Select(o => o.AccountsTransactionsAccNo).Distinct().ToList();

                var assetsNotIn =  (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaRelativeNo.ToUpper() == relativeNo.ToUpper()
                                                                      && y.MaAccType == "A"
                                                                      && y.MaStatus == "R"
                                                                      && !AccountTransIds.Contains(y.MaAcAcc)
                                     )

                                         select new BalanceSheetResponse
                                         {
                                             MA_MainHead = ma.MaMainHead,
                                             MA_SubHead = ma.MaSubHead,
                                             MA_AccNo = ma.MaAccNo,
                                             MA_AccName = ma.MaAccName,
                                             MA_RelativeNo = ma.MaRelativeNo,
                                             MA_RelativeName = _masterAccount.GetAsQueryable().Where(y => y.MaAccNo.ToUpper() == ma.MaRelativeNo.ToLower()).Select(y => y.MaAccName).FirstOrDefault(),
                                             Debit = 0,
                                             Credit = 0
                                         })
                    .OrderBy(t => t.MA_AccName).ToList();

                var assetsFinal = assets.Union(assetsNotIn);
                double Total_Dr = assetsFinal.Sum(x => Convert.ToDouble(x.Debit)); 
                double Total_Cr = assetsFinal.Sum(x => Convert.ToDouble(x.Credit));
                totalAsset = Total_Cr - Total_Dr;
            }
            catch (Exception)
            {
                return ;
            }
        }
        private void GetBalanceSheetTotalsMainHead(GenericGridViewModel model, string relativeNo, ref double totalAsset)
        {
            try
            {
                var assets = (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaMainHead.ToUpper() == relativeNo.ToUpper())
                              join at in _accountTrans.GetAsQueryable()
                                            .Where(x => x.AccountsTransactionsTransDate >= model.FromDate
                              && x.AccountsTransactionsTransDate <= model.ToDate
                              )
                              on ma.MaAccNo equals at.AccountsTransactionsAccNo into g
                              from grd in g.DefaultIfEmpty()
                              group grd by new
                              {
                                  grd.AccountsTransactionsAccNo,
                                  ma.MaAccName,
                                  ma.MaMainHead,
                                  ma.MaRelativeNo,
                                  ma.MaSubHead
                              } into gp

                              where gp.Sum(x => x.AccountsTransactionsCredit) > 0 || gp.Sum(x => x.AccountsTransactionsDebit) > 0
                              select new BalanceSheetResponse
                              {
                                  MA_MainHead = gp.Key.MaMainHead,
                                  MA_SubHead = gp.Key.MaSubHead,
                                  MA_AccNo = gp.Key.AccountsTransactionsAccNo,
                                  MA_AccName = gp.Key.MaAccName,
                                  MA_RelativeNo = gp.Key.MaRelativeNo,
                                  Debit = gp.Sum(pc => pc.AccountsTransactionsDebit),
                                  Credit = gp.Sum(pc => pc.AccountsTransactionsCredit)
                              })

                      .OrderBy(t => t.MA_AccName).ToList();

                var AccountTransIds = _accountTrans.GetAsQueryable()
                                                               .Where(x => x.AccountsTransactionsTransDate >= model.FromDate
                                                 && x.AccountsTransactionsTransDate <= model.ToDate).Select(o => o.AccountsTransactionsAccNo).Distinct().ToList();

                var assetsNotIn = (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaMainHead.ToUpper() == relativeNo.ToUpper()
                                                                      && y.MaAccType == "A"
                                                                      && y.MaStatus == "R"
                                                                      && !AccountTransIds.Contains(y.MaAcAcc)
                                     )

                                   select new BalanceSheetResponse
                                   {
                                       MA_MainHead = ma.MaMainHead,
                                       MA_SubHead = ma.MaSubHead,
                                       MA_AccNo = ma.MaAccNo,
                                       MA_AccName = ma.MaAccName,
                                       MA_RelativeNo = ma.MaRelativeNo,
                                       MA_RelativeName = _masterAccount.GetAsQueryable().Where(y => y.MaAccNo.ToUpper() == ma.MaRelativeNo.ToLower()).Select(y => y.MaAccName).FirstOrDefault(),
                                       Debit = 0,
                                       Credit = 0
                                   })
                    .OrderBy(t => t.MA_AccName).ToList();

                var assetsFinal = assets.Union(assetsNotIn);
                double Total_Dr = assetsFinal.Sum(x => Convert.ToDouble(x.Debit));
                double Total_Cr = assetsFinal.Sum(x => Convert.ToDouble(x.Credit));
                totalAsset = Total_Cr - Total_Dr;

            }
            catch (Exception)
            {
                return;
            }
        }
        public async Task<Response<List<BalanceSheetResponse>>> GetAccountTransactionsBalanceSheetsSummary(GenericGridViewModel model)
        {
            try
            {
                var gridReponse = new List<BalanceSheetResponse>();
                double Net_Capital = 0, Net_Retained = 0, Net_Owners = 0, Total_INCOME = 0, Total_Expenses = 0, Net_Profit_Loss = 0;
                GetBalanceSheetTotals(model, "EQ5", ref Net_Capital);
                GetBalanceSheetTotals(model, "EQ38", ref Net_Retained);
                GetBalanceSheetTotals(model, "EQ6", ref Net_Owners);
                GetBalanceSheetTotalsMainHead(model, "INCOME", ref Total_INCOME);
                GetBalanceSheetTotalsMainHead(model, "EXPENSES", ref Total_Expenses);
                Net_Profit_Loss = (Total_INCOME - Total_Expenses); //+ ClostingStock - Opening_Stock_Value;

                var assets = await (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaMainHead.ToUpper() == "ASSETS")
                                    join at in _accountTrans.GetAsQueryable()
                                                  .Where(x => x.AccountsTransactionsTransDate >= model.FromDate
                                    && x.AccountsTransactionsTransDate <= model.ToDate
                                    )
                                    on ma.MaAccNo equals at.AccountsTransactionsAccNo into g
                                    from grd in g.DefaultIfEmpty()
                                    group grd by new
                                    {
                                        //grd.AccountsTransactionsAccNo,
                                        //ma.MaAccName,
                                        ma.MaMainHead,
                                        ma.MaRelativeNo,
                                        ma.MaSubHead
                                    } into gp
                                    where gp.Sum(x => x.AccountsTransactionsCredit) > 0 || gp.Sum(x => x.AccountsTransactionsDebit) > 0
                                    select new BalanceSheetResponse
                                    {
                                        MA_MainHead = gp.Key.MaMainHead,
                                        MA_SubHead = gp.Key.MaSubHead,
                                        //MA_AccNo = gp.Key.AccountsTransactionsAccNo,
                                        //MA_AccName = gp.Key.MaAccName,
                                        MA_RelativeNo = gp.Key.MaRelativeNo,
                                        MA_RelativeName = _masterAccount.GetAsQueryable().Where(y => y.MaAccNo.ToUpper() == gp.Key.MaRelativeNo.ToUpper()).Select(y => y.MaAccName).FirstOrDefault(),
                                        Debit = gp.Sum(pc => pc.AccountsTransactionsDebit),
                                        Credit = gp.Sum(pc => pc.AccountsTransactionsCredit)
                                    })

                         .OrderBy(t => t.MA_RelativeName).ToListAsync();

                var AccountTransIds = _accountTrans.GetAsQueryable()
                                                               .Where(x => x.AccountsTransactionsTransDate >= model.FromDate
                                                 && x.AccountsTransactionsTransDate <= model.ToDate).Select(o => o.AccountsTransactionsAccNo).Distinct().ToList();

                var assetsNotIn = await (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaMainHead.ToUpper() == "ASSETS"
                                                                          && y.MaAccType == "A"
                                                                          && y.MaStatus == "R"
                                                                          && !AccountTransIds.Contains(y.MaAcAcc)
                                         )

                                         select new BalanceSheetResponse
                                         {
                                             MA_MainHead = ma.MaMainHead,
                                             MA_SubHead = ma.MaSubHead,
                                             //MA_AccNo = ma.MaAccNo,
                                             //MA_AccName = ma.MaAccName,
                                             MA_RelativeNo = ma.MaRelativeNo,
                                             MA_RelativeName = _masterAccount.GetAsQueryable().Where(y => y.MaAccNo.ToUpper() == ma.MaRelativeNo.ToLower()).Select(y => y.MaAccName).FirstOrDefault(),
                                             Debit = 0,
                                             Credit = 0
                                         })
                        .OrderBy(t => t.MA_RelativeName).ToListAsync();

                var assetsFinal = assets.Union(assetsNotIn);

                var liabilites = await (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaMainHead.ToUpper() == "LIABILITIES")
                                        join at in _accountTrans.GetAsQueryable()
                                                      .Where(x => x.AccountsTransactionsTransDate >= model.FromDate
                                        && x.AccountsTransactionsTransDate <= model.ToDate
                                        )
                                        on ma.MaAccNo equals at.AccountsTransactionsAccNo into g
                                        from grd in g.DefaultIfEmpty()
                                        group grd by new
                                        {
                                            //grd.AccountsTransactionsAccNo,
                                            //ma.MaAccName,
                                            ma.MaMainHead,
                                            ma.MaRelativeNo,
                                            ma.MaSubHead
                                        } into gp

                                        where gp.Sum(x => x.AccountsTransactionsCredit) > 0 || gp.Sum(x => x.AccountsTransactionsDebit) > 0
                                        select new BalanceSheetResponse
                                        {
                                            MA_MainHead = gp.Key.MaMainHead,
                                            MA_SubHead = gp.Key.MaSubHead,
                                            //MA_AccNo = gp.Key.AccountsTransactionsAccNo,
                                            //MA_AccName = gp.Key.MaAccName,
                                            MA_RelativeNo = gp.Key.MaRelativeNo,
                                            MA_RelativeName = _masterAccount.GetAsQueryable().Where(y => y.MaAccNo.ToUpper() == gp.Key.MaRelativeNo.ToUpper()).Select(y => y.MaAccName).FirstOrDefault(),
                                            Debit = gp.Sum(pc => pc.AccountsTransactionsDebit),
                                            Credit = gp.Sum(pc => pc.AccountsTransactionsCredit)
                                        })

                       .OrderBy(t => t.MA_RelativeName).ToListAsync();

                var liabilitesNotIn = await (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaMainHead.ToUpper() == "LIABILITIES"
                                                                          && y.MaAccType == "A"
                                                                          && y.MaStatus == "R"
                                                                          && !AccountTransIds.Contains(y.MaAcAcc)
                                         )

                                             select new BalanceSheetResponse
                                             {
                                                 MA_MainHead = ma.MaMainHead,
                                                 MA_SubHead = ma.MaSubHead,
                                                 //MA_AccNo = ma.MaAccNo,
                                                 //MA_AccName = ma.MaAccName,
                                                 MA_RelativeNo = ma.MaRelativeNo,
                                                 MA_RelativeName = _masterAccount.GetAsQueryable().Where(y => y.MaAccNo.ToUpper() == ma.MaRelativeNo.ToLower()).Select(y => y.MaAccName).FirstOrDefault(),
                                                 Debit = 0,
                                                 Credit = 0
                                             })
                        .OrderBy(t => t.MA_RelativeName).ToListAsync();

                var liabilitesFinal = liabilites.Union(liabilitesNotIn);

                var assetsLiablitiesEquties = assetsFinal.Union(liabilitesFinal).ToList();
                assetsLiablitiesEquties.Add(new BalanceSheetResponse
                {
                    MA_MainHead = "Equity",
                    MA_SubHead = "Profit & Loss",
                    MA_AccNo = "",
                    MA_AccName = "Profit & Loss",
                    MA_RelativeNo = "",
                    MA_RelativeName = "Profit & Loss",
                    Debit = Net_Profit_Loss > 0 ? 0 : Convert.ToDecimal(System.Math.Abs(Net_Profit_Loss)),
                    Credit = Net_Profit_Loss > 0 ? Convert.ToDecimal(System.Math.Abs(Net_Profit_Loss)) : 0,
                });

                return Response<List<BalanceSheetResponse>>.Success(assetsLiablitiesEquties.ToList(), "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<BalanceSheetResponse>>.Fail(new List<BalanceSheetResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<BalanceSheetResponse>>> GetAccountTransactionsBalanceSheetsDetails(GenericGridViewModel model)
        {
            try
            {
                var gridReponse = new List<BalanceSheetResponse>();
                double Net_Capital=0, Net_Retained=0, Net_Owners=0, Total_INCOME=0, Total_Expenses=0, Net_Profit_Loss=0;
                GetBalanceSheetTotals(model, "EQ5",ref Net_Capital);
                GetBalanceSheetTotals(model, "EQ38", ref Net_Retained);
                GetBalanceSheetTotals(model, "EQ6", ref Net_Owners);
                GetBalanceSheetTotalsMainHead(model, "INCOME", ref Total_INCOME);
                GetBalanceSheetTotalsMainHead(model, "EXPENSES", ref Total_Expenses);
                Net_Profit_Loss = (Total_INCOME - Total_Expenses); //+ ClostingStock - Opening_Stock_Value;

                var assets = await (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaMainHead.ToUpper() == "ASSETS")
                                                 join at in _accountTrans.GetAsQueryable()
                                                               .Where(x => x.AccountsTransactionsTransDate >= model.FromDate
                                                 && x.AccountsTransactionsTransDate <= model.ToDate
                                                 )
                                                 on ma.MaAccNo equals at.AccountsTransactionsAccNo into g
                                                 from grd in g.DefaultIfEmpty()
                                                 group grd by new
                                                 {
                                                     ma.MaMainHead,
                                                     ma.MaSubHead,
                                                     ma.MaRelativeNo,
                                                     grd.AccountsTransactionsAccNo,
                                                     ma.MaAccName
                                                     
                                                   
                                                 } into gp
                                                 where gp.Sum(x => x.AccountsTransactionsCredit) > 0 || gp.Sum(x => x.AccountsTransactionsDebit) > 0
                                                 select new BalanceSheetResponse
                                                 {
                                                     MA_MainHead = gp.Key.MaMainHead,
                                                     MA_SubHead = gp.Key.MaSubHead,
                                                     MA_AccNo = gp.Key.AccountsTransactionsAccNo,
                                                     MA_AccName = gp.Key.MaAccName,
                                                     MA_RelativeNo = gp.Key.MaRelativeNo,
                                                     MA_RelativeName = _masterAccount.GetAsQueryable().Where(y => y.MaAccNo.ToUpper() == gp.Key.MaRelativeNo.ToUpper()).Select(y=>y.MaAccName).FirstOrDefault(),
                                                     Debit = gp.Sum(pc => pc.AccountsTransactionsDebit),
                                                     Credit = gp.Sum(pc => pc.AccountsTransactionsCredit)
                                                 })

                         .OrderBy(t => t.MA_AccName).ToListAsync();
              
                var AccountTransIds = _accountTrans.GetAsQueryable()
                                                               .Where(x => x.AccountsTransactionsTransDate >= model.FromDate
                                                 && x.AccountsTransactionsTransDate <= model.ToDate).Select(o => o.AccountsTransactionsAccNo).Distinct().ToList();

                var assetsNotIn = await (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaMainHead.ToUpper() == "ASSETS"
                                                                          && y.MaAccType=="A"
                                                                          && y.MaStatus == "R"
                                                                          && !AccountTransIds.Contains(y.MaAcAcc)
                                         )

                                    select new BalanceSheetResponse
                                    {
                                        MA_MainHead = ma.MaMainHead,
                                        MA_SubHead = ma.MaSubHead,
                                        MA_AccNo = ma.MaAccNo,
                                        MA_AccName = ma.MaAccName,
                                        MA_RelativeNo = ma.MaRelativeNo,
                                        MA_RelativeName = _masterAccount.GetAsQueryable().Where(y => y.MaAccNo.ToUpper() == ma.MaRelativeNo.ToLower()).Select(y => y.MaAccName).FirstOrDefault(),
                                        Debit =0,
                                        Credit = 0
                                    })
                        .OrderBy(t => t.MA_AccName).ToListAsync();

                var assetsFinal = assets.Union(assetsNotIn);

                var liabilites = await (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaMainHead.ToUpper() == "LIABILITIES")
                                    join at in _accountTrans.GetAsQueryable()
                                                  .Where(x => x.AccountsTransactionsTransDate >= model.FromDate
                                    && x.AccountsTransactionsTransDate <= model.ToDate
                                    )
                                    on ma.MaAccNo equals at.AccountsTransactionsAccNo into g
                                    from grd in g.DefaultIfEmpty()
                                    group grd by new
                                    {
                                        grd.AccountsTransactionsAccNo,
                                        ma.MaAccName,
                                        ma.MaMainHead,
                                        ma.MaRelativeNo,
                                        ma.MaSubHead
                                    } into gp

                                    where gp.Sum(x => x.AccountsTransactionsCredit) > 0 || gp.Sum(x => x.AccountsTransactionsDebit) > 0
                                    select new BalanceSheetResponse
                                    {
                                        MA_MainHead = gp.Key.MaMainHead,
                                        MA_SubHead = gp.Key.MaSubHead,
                                        MA_AccNo = gp.Key.AccountsTransactionsAccNo,
                                        MA_AccName = gp.Key.MaAccName,
                                        MA_RelativeNo = gp.Key.MaRelativeNo,
                                        MA_RelativeName = _masterAccount.GetAsQueryable().Where(y => y.MaAccNo.ToUpper() == gp.Key.MaRelativeNo.ToUpper()).Select(y => y.MaAccName).FirstOrDefault(),
                                        Debit = gp.Sum(pc => pc.AccountsTransactionsDebit),
                                        Credit = gp.Sum(pc => pc.AccountsTransactionsCredit)
                                    })

                       .OrderBy(t => t.MA_AccName).ToListAsync();

                var liabilitesNotIn = await (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaMainHead.ToUpper() == "LIABILITIES"
                                                                          && y.MaAccType == "A"
                                                                          && y.MaStatus == "R"
                                                                          && !AccountTransIds.Contains(y.MaAcAcc)
                                         )

                                         select new BalanceSheetResponse
                                         {
                                             MA_MainHead = ma.MaMainHead,
                                             MA_SubHead = ma.MaSubHead,
                                             MA_AccNo = ma.MaAccNo,
                                             MA_AccName = ma.MaAccName,
                                             MA_RelativeNo = ma.MaRelativeNo,
                                             MA_RelativeName = _masterAccount.GetAsQueryable().Where(y => y.MaAccNo.ToUpper() == ma.MaRelativeNo.ToLower()).Select(y => y.MaAccName).FirstOrDefault(),
                                             Debit = 0,
                                             Credit = 0
                                         })
                        .OrderBy(t => t.MA_AccName).ToListAsync();

                var liabilitesFinal = liabilites.Union(liabilitesNotIn);

                var assetsLiablitiesEquties = assetsFinal.Union(liabilitesFinal).ToList();
                assetsLiablitiesEquties.Add(new BalanceSheetResponse
                {
                    MA_MainHead = "Equity",
                    MA_SubHead = "Profit & Loss",
                    MA_AccNo = "",
                    MA_AccName = "Profit & Loss",
                    MA_RelativeNo = "",
                    MA_RelativeName = "Profit & Loss",
                    Debit = Net_Profit_Loss > 0 ? 0 : Convert.ToDecimal(System.Math.Abs(Net_Profit_Loss)),
                    Credit = Net_Profit_Loss > 0 ? Convert.ToDecimal(System.Math.Abs(Net_Profit_Loss)) : 0,
                });

                return Response<List<BalanceSheetResponse>>.Success(assetsLiablitiesEquties.ToList(), "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<BalanceSheetResponse>>.Fail(new List<BalanceSheetResponse>(), ex.Message);
            }
        }
        public async Task<Response<GridWrapperResponse<List<GetBalanceSheetSummaryResponse>>>> GetAccountTransactionsBalanceSheetSummary(GenericGridViewModel model)
        {
            try
            {
                string assetsQuery = @$"  {model.Filter} and ( mat.MA_MainHead= 'Assets') ";
                string equityQuery = @$"  {model.Filter}  and (mat.MA_MainHead= 'Equity' ) ";
                string liabilitiesQuery = @$"  {model.Filter}  and (mat.MA_MainHead= 'Liabilities' ) ";
                decimal totalasset = 0;
                decimal totalequity = 0;
                decimal totalLiabilities = 0;
                var BalanceSheetSummaryList = new List<GetBalanceSheetSummaryResponse>();
                var assets = await GetBalanceSheetByFilter(new GenericGridViewModel() { Filter = assetsQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                var equitys = await GetBalanceSheetByFilter(new GenericGridViewModel() { Filter = equityQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                var liabilities = await GetBalanceSheetByFilter(new GenericGridViewModel() { Filter = liabilitiesQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                BalanceSheetSummaryList.Add(GetBalanceSheetSummaryAsset(assets.Result, ref totalasset));
                BalanceSheetSummaryList.Add(GetBalanceSheetSummaryEquity(equitys.Result, ref totalequity));
                BalanceSheetSummaryList.Add(GetBalanceSheetSummaryLiability(liabilities.Result, ref totalLiabilities));
                var gridReponse = new GridWrapperResponse<List<GetBalanceSheetSummaryResponse>>();
                gridReponse.Data = BalanceSheetSummaryList;
                var total = 0;
                gridReponse.Total = Convert.ToInt32(total);
                return Response<GridWrapperResponse<List<GetBalanceSheetSummaryResponse>>>.Success(gridReponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<GetBalanceSheetSummaryResponse>>>.Fail(new GridWrapperResponse<List<GetBalanceSheetSummaryResponse>>(), ex.Message);
            }
        }
        public async Task<Response<GridWrapperResponse<List<GetBalanceSheetDetailResponse>>>> GetAccountTransactionsBalanceSheetDetail(GenericGridViewModel model)
        {
            try
            {
                string assetsQuery = @$"  {model.Filter} and ( mat.MA_MainHead= 'Assets') ";
                string equityQuery = @$"  {model.Filter}  and (mat.MA_MainHead= 'Equity' ) ";
                string liabilitiesQuery = @$"  {model.Filter}  and (mat.MA_MainHead= 'Liabilities' ) ";
                decimal totalasset = 0;
                decimal totalequity = 0;
                decimal totalLiabilities = 0;
                var BalanceSheetSummaryList = new List<GetBalanceSheetDetailResponse>();
                var assets = await GetBalanceSheetByFilter(new GenericGridViewModel() { Filter = assetsQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                var equitys = await GetBalanceSheetByFilter(new GenericGridViewModel() { Filter = equityQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                var liabilities = await GetBalanceSheetByFilter(new GenericGridViewModel() { Filter = liabilitiesQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                var profitloss = await _profitLoss.GetAccountTransactionsProfitLoss(new GenericGridViewModel() { Filter = model.Filter, Search = model.Search, Take = model.Take, Skip = model.Skip });
                var listequity = new List<GetAccountTransactionBalanceSheetResponse>();
                listequity.AddRange(equitys.Result);
                listequity.Add(new GetAccountTransactionBalanceSheetResponse()
                {
                    MasterAccountsTableMainHead = "Equity",
                    AccountsTransactionsAccName = profitloss.Result.Details[2].accName,
                    MasterAccountsTableHead = "Profit Loss",
                    MasterAccountsTableSubHead = "Profit Loss",
                    MasterAccountsTableRelativeNo = "Profit Loss",
                    AccountsTransactionsCredit = Convert.ToDecimal(Convert.ToDecimal(profitloss.Result.Details[2].amount) + Convert.ToDecimal(profitloss.Result.Details[2].amount) / 2),
                    AccountsTransactionsDebit = Convert.ToDecimal(Convert.ToDecimal(profitloss.Result.Details[2].amount) - Convert.ToDecimal(profitloss.Result.Details[2].amount) / 2)
                });

                BalanceSheetSummaryList.Add(GetBalanceSheetDetailAsset(assets.Result, ref totalasset));
                BalanceSheetSummaryList.Add(GetBalanceSheetDetailEquity(listequity, ref totalequity));
                BalanceSheetSummaryList.Add(GetBalanceSheetDetailLiability(liabilities.Result, ref totalLiabilities));
                var gridReponse = new GridWrapperResponse<List<GetBalanceSheetDetailResponse>>();
                gridReponse.Data = BalanceSheetSummaryList;
                var total = 0;
                gridReponse.Total = Convert.ToInt32(total);
                return Response<GridWrapperResponse<List<GetBalanceSheetDetailResponse>>>.Success(gridReponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<GetBalanceSheetDetailResponse>>>.Fail(new GridWrapperResponse<List<GetBalanceSheetDetailResponse>>(), ex.Message);
            }
        }
        private GetBalanceSheetDetailResponse GetBalanceSheetDetailAsset(List<GetAccountTransactionBalanceSheetResponse> data, ref decimal totalAsset)
        {
            try
            {
                var heads = data.Select(x => x.MasterAccountsTableHead).Distinct().ToList();
                var subheads = data.Select(x => x.MasterAccountsTableSubHead).Distinct().ToList();

                var mainheadBalanceSheetSummary = new GetBalanceSheetDetailResponse();
                mainheadBalanceSheetSummary.mainHead = "Assets";
                mainheadBalanceSheetSummary.head = null;
                mainheadBalanceSheetSummary.subHead = null;
                mainheadBalanceSheetSummary.amount = null;
                mainheadBalanceSheetSummary.__children = new List<HeadBalanceSheetDetail>();
                foreach (var head in heads)
                {
                    var headBalanceSheetSummary = new HeadBalanceSheetDetail();
                    headBalanceSheetSummary.head = head;
                    headBalanceSheetSummary.subHead = null;
                    headBalanceSheetSummary.amount = null;
                    headBalanceSheetSummary.__children = new List<SubHeadBalanceSheetDetail>();
                    decimal totalSubhead = 0;
                    foreach (var subhead in data.Where(x => x.MasterAccountsTableHead == head).Select(x => x.MasterAccountsTableSubHead).Distinct().ToList())
                    {
                        var subheadBalanceSheetSummary = new SubHeadBalanceSheetDetail();
                        subheadBalanceSheetSummary.head = subhead;

                        subheadBalanceSheetSummary.amount = null;
                        subheadBalanceSheetSummary.__children = new List<AccNameBalanceSheetDetail>();
                        decimal totalAcc = 0;
                        foreach (var asset in data.Where(x => x.MasterAccountsTableSubHead == subhead).ToList())
                        {
                            subheadBalanceSheetSummary.__children.Add(new AccNameBalanceSheetDetail()
                            {
                                accName = asset.AccountsTransactionsAccName,
                                amount = (asset.AccountsTransactionsCredit - asset.AccountsTransactionsDebit).ToString(),
                            });
                            totalAcc += asset.AccountsTransactionsCredit - asset.AccountsTransactionsDebit;
                        }
                        totalSubhead += totalAcc;
                        subheadBalanceSheetSummary.__children.Add(new AccNameBalanceSheetDetail()
                        {
                            accName = @$"Total {subhead}",
                            amount = totalAcc.ToString(),
                        });
                        headBalanceSheetSummary.__children.Add(subheadBalanceSheetSummary);
                    }
                    totalAsset += totalSubhead;
                    headBalanceSheetSummary.__children.Add(new SubHeadBalanceSheetDetail()
                    {
                        head = @$"Total {head}",
                        subHead = null,
                        accName = null,
                        amount = totalSubhead.ToString(),
                    });
                    mainheadBalanceSheetSummary.__children.Add(headBalanceSheetSummary);
                }
                mainheadBalanceSheetSummary.__children.Add(new HeadBalanceSheetDetail()
                {
                    mainHead = null,
                    head = "Total Assets",
                    subHead = null,
                    amount = totalAsset.ToString(),
                });
                return mainheadBalanceSheetSummary;
            }
            catch (Exception)
            {
                return new GetBalanceSheetDetailResponse();
            }
        }
        private GetBalanceSheetDetailResponse GetBalanceSheetDetailEquity(List<GetAccountTransactionBalanceSheetResponse> data, ref decimal totalEquity)
        {
            try
            {
                var heads = data.Select(x => x.MasterAccountsTableHead).Distinct().ToList();
                var subheads = data.Select(x => x.MasterAccountsTableSubHead).Distinct().ToList();
                var mainheadBalanceSheetSummary = new GetBalanceSheetDetailResponse();
                mainheadBalanceSheetSummary.mainHead = "Equity";
                mainheadBalanceSheetSummary.head = null;
                mainheadBalanceSheetSummary.subHead = null;
                mainheadBalanceSheetSummary.amount = null;
                mainheadBalanceSheetSummary.__children = new List<HeadBalanceSheetDetail>();
                foreach (var head in heads)
                {
                    var headBalanceSheetSummary = new HeadBalanceSheetDetail();
                    headBalanceSheetSummary.head = head;
                    headBalanceSheetSummary.subHead = null;
                    headBalanceSheetSummary.amount = null;
                    headBalanceSheetSummary.__children = new List<SubHeadBalanceSheetDetail>();
                    decimal totalSubhead = 0;
                    foreach (var subhead in data.Where(x => x.MasterAccountsTableHead == head).Select(x => x.MasterAccountsTableSubHead).Distinct().ToList())
                    {
                        var subheadBalanceSheetSummary = new SubHeadBalanceSheetDetail();
                        subheadBalanceSheetSummary.head = subhead;

                        subheadBalanceSheetSummary.amount = null;
                        subheadBalanceSheetSummary.__children = new List<AccNameBalanceSheetDetail>();
                        decimal totalAcc = 0;
                        foreach (var asset in data.Where(x => x.MasterAccountsTableSubHead == subhead).ToList())
                        {
                            subheadBalanceSheetSummary.__children.Add(new AccNameBalanceSheetDetail()
                            {
                                accName = asset.AccountsTransactionsAccName,
                                amount = (asset.AccountsTransactionsCredit - asset.AccountsTransactionsDebit).ToString(),
                            });
                            totalAcc += asset.AccountsTransactionsCredit - asset.AccountsTransactionsDebit;
                        }
                        totalSubhead += totalAcc;
                        subheadBalanceSheetSummary.__children.Add(new AccNameBalanceSheetDetail()
                        {
                            accName = @$"Total {subhead}",
                            amount = totalAcc.ToString(),
                        });
                        headBalanceSheetSummary.__children.Add(subheadBalanceSheetSummary);
                    }
                    totalEquity += totalSubhead;
                    headBalanceSheetSummary.__children.Add(new SubHeadBalanceSheetDetail()
                    {
                        head = @$"Total {head}",
                        subHead = null,
                        accName = null,
                        amount = totalSubhead.ToString(),
                    });
                    mainheadBalanceSheetSummary.__children.Add(headBalanceSheetSummary);
                }
                mainheadBalanceSheetSummary.__children.Add(new HeadBalanceSheetDetail()
                {
                    mainHead = null,
                    head = "Total Equity",
                    subHead = null,
                    amount = totalEquity.ToString(),
                });
                return mainheadBalanceSheetSummary;
            }
            catch (Exception)
            {
                return new GetBalanceSheetDetailResponse();
            }
        }
        private GetBalanceSheetDetailResponse GetBalanceSheetDetailLiability(List<GetAccountTransactionBalanceSheetResponse> data, ref decimal totalliabilities)
        {
            try
            {
                var heads = data.Select(x => x.MasterAccountsTableHead).Distinct().ToList();
                var subheads = data.Select(x => x.MasterAccountsTableSubHead).Distinct().ToList();

                var mainheadBalanceSheetSummary = new GetBalanceSheetDetailResponse();
                mainheadBalanceSheetSummary.mainHead = "Liablity";
                mainheadBalanceSheetSummary.head = null;
                mainheadBalanceSheetSummary.subHead = null;
                mainheadBalanceSheetSummary.amount = null;
                mainheadBalanceSheetSummary.__children = new List<HeadBalanceSheetDetail>();
                foreach (var head in heads)
                {
                    var headBalanceSheetSummary = new HeadBalanceSheetDetail();
                    headBalanceSheetSummary.head = head;
                    headBalanceSheetSummary.subHead = null;
                    headBalanceSheetSummary.amount = null;
                    headBalanceSheetSummary.__children = new List<SubHeadBalanceSheetDetail>();
                    decimal totalSubhead = 0;
                    foreach (var subhead in data.Where(x => x.MasterAccountsTableHead == head).Select(x => x.MasterAccountsTableSubHead).Distinct().ToList())
                    {
                        var subheadBalanceSheetSummary = new SubHeadBalanceSheetDetail();
                        subheadBalanceSheetSummary.head = subhead;

                        subheadBalanceSheetSummary.amount = null;
                        subheadBalanceSheetSummary.__children = new List<AccNameBalanceSheetDetail>();
                        decimal totalAcc = 0;
                        foreach (var asset in data.Where(x => x.MasterAccountsTableSubHead == subhead).ToList())
                        {
                            subheadBalanceSheetSummary.__children.Add(new AccNameBalanceSheetDetail()
                            {
                                accName = asset.AccountsTransactionsAccName,
                                amount = (asset.AccountsTransactionsCredit - asset.AccountsTransactionsDebit).ToString(),
                            });
                            totalAcc += asset.AccountsTransactionsCredit - asset.AccountsTransactionsDebit;
                        }
                        totalSubhead += totalAcc;
                        subheadBalanceSheetSummary.__children.Add(new AccNameBalanceSheetDetail()
                        {
                            accName = @$"Total {subhead}",
                            amount = totalAcc.ToString(),
                        });
                        headBalanceSheetSummary.__children.Add(subheadBalanceSheetSummary);
                    }
                    totalliabilities += totalSubhead;
                    headBalanceSheetSummary.__children.Add(new SubHeadBalanceSheetDetail()
                    {
                        head = @$"Total {head}",
                        subHead = null,
                        accName = null,
                        amount = totalSubhead.ToString(),
                    });

                    mainheadBalanceSheetSummary.__children.Add(headBalanceSheetSummary);
                }
                mainheadBalanceSheetSummary.__children.Add(new HeadBalanceSheetDetail()
                {
                    mainHead = null,
                    head = "Total Liabilities",
                    subHead = null,
                    amount = totalliabilities.ToString(),
                });
                return mainheadBalanceSheetSummary;
            }
            catch (Exception)
            {
                return new GetBalanceSheetDetailResponse();
            }
        }
        private GetBalanceSheetSummaryResponse GetBalanceSheetSummaryAsset(List<GetAccountTransactionBalanceSheetResponse> data, ref decimal totalAsset)
        {
            try
            {
                var incomesubhead = data.Select(x => x.MasterAccountsTableHead).Distinct().ToList();
                var headBalanceSheetSummary = new GetBalanceSheetSummaryResponse();
                headBalanceSheetSummary.head = "Assets";
                headBalanceSheetSummary.accName = null;
                headBalanceSheetSummary.debit = null;
                headBalanceSheetSummary.credit = null;
                headBalanceSheetSummary.amount = null;
                headBalanceSheetSummary.__children = new List<SubHeadBalanceSheetSummary>();
                foreach (var subhead in incomesubhead)
                {
                    var subheadBalanceSheetSummary = new SubHeadBalanceSheetSummary();
                    subheadBalanceSheetSummary.debit = null;
                    subheadBalanceSheetSummary.accName = subhead;
                    subheadBalanceSheetSummary.credit = null;
                    subheadBalanceSheetSummary.amount = null;
                    subheadBalanceSheetSummary.__children = new List<AccNameBalanceSheetSummary>();
                    decimal totalAcc = 0;
                    foreach (var income in data.Where(x => x.MasterAccountsTableHead == subhead))
                    {
                        subheadBalanceSheetSummary.__children.Add(new AccNameBalanceSheetSummary()
                        {
                            accName = income.AccountsTransactionsAccName,
                            credit = income.AccountsTransactionsCredit.ToString(),
                            debit = income.AccountsTransactionsDebit.ToString(),
                            amount = (income.AccountsTransactionsCredit - income.AccountsTransactionsDebit).ToString()
                        });
                        totalAcc += income.AccountsTransactionsCredit - income.AccountsTransactionsDebit;
                    }
                    totalAsset += totalAcc;
                    subheadBalanceSheetSummary.__children.Add(new AccNameBalanceSheetSummary()
                    {
                        accName = @$"Total {subhead}",
                        amount = totalAcc.ToString(),
                        credit = null,
                        debit = null
                    });

                    headBalanceSheetSummary.__children.Add(subheadBalanceSheetSummary);
                }
                headBalanceSheetSummary.__children.Add(new SubHeadBalanceSheetSummary()
                {
                    accName = "Total Assets",
                    debit = null,
                    credit = null,
                    amount = totalAsset.ToString(),
                });
                return headBalanceSheetSummary;
            }
            catch (Exception)
            {
                return new GetBalanceSheetSummaryResponse();
            }
        }
        private GetBalanceSheetSummaryResponse GetBalanceSheetSummaryEquity(List<GetAccountTransactionBalanceSheetResponse> data, ref decimal totalEquity)
        {
            try
            {
                var subheads = data.Select(x => x.MasterAccountsTableHead).Distinct().ToList();
                var headBalanceSheetSummary = new GetBalanceSheetSummaryResponse();
                headBalanceSheetSummary.head = "Equity";
                headBalanceSheetSummary.debit = null;
                headBalanceSheetSummary.credit = null;
                headBalanceSheetSummary.amount = null;
                headBalanceSheetSummary.accName = null;
                headBalanceSheetSummary.__children = new List<SubHeadBalanceSheetSummary>();
                foreach (var subhead in subheads)
                {
                    var subheadBalanceSheetSummary = new SubHeadBalanceSheetSummary();
                    subheadBalanceSheetSummary.debit = null;
                    subheadBalanceSheetSummary.credit = null;
                    subheadBalanceSheetSummary.amount = null;
                    subheadBalanceSheetSummary.accName = subhead;
                    subheadBalanceSheetSummary.__children = new List<AccNameBalanceSheetSummary>();
                    decimal totalAcc = 0;
                    foreach (var income in data.Where(x => x.MasterAccountsTableHead == subhead))
                    {

                        subheadBalanceSheetSummary.__children.Add(new AccNameBalanceSheetSummary()
                        {
                            accName = income.AccountsTransactionsAccName,
                            credit = income.AccountsTransactionsCredit.ToString(),
                            debit = income.AccountsTransactionsDebit.ToString(),
                            amount = (income.AccountsTransactionsCredit - income.AccountsTransactionsDebit).ToString()
                        });
                        totalAcc += income.AccountsTransactionsCredit - income.AccountsTransactionsDebit;
                    }
                    totalEquity += totalAcc;
                    subheadBalanceSheetSummary.__children.Add(new AccNameBalanceSheetSummary()
                    {
                        accName = @$"Total {subhead}",
                        amount = totalAcc.ToString(),
                        credit = null,
                        debit = null
                    });

                    headBalanceSheetSummary.__children.Add(subheadBalanceSheetSummary);
                }
                headBalanceSheetSummary.__children.Add(new SubHeadBalanceSheetSummary()
                {
                    accName = "Total Equity",
                    debit = null,
                    credit = null,
                    amount = totalEquity.ToString(),
                });
                return headBalanceSheetSummary;
            }
            catch (Exception)
            {
                return new GetBalanceSheetSummaryResponse();
            }
        }
        private GetBalanceSheetSummaryResponse GetBalanceSheetSummaryLiability(List<GetAccountTransactionBalanceSheetResponse> data, ref decimal totalliabilities)
        {
            try
            {
                var subheads = data.Select(x => x.MasterAccountsTableHead).Distinct().ToList();
                var headBalanceSheetSummary = new GetBalanceSheetSummaryResponse();
                headBalanceSheetSummary.head = "Liabilities";
                headBalanceSheetSummary.debit = null;
                headBalanceSheetSummary.credit = null;
                headBalanceSheetSummary.amount = null;
                headBalanceSheetSummary.accName = null;
                headBalanceSheetSummary.__children = new List<SubHeadBalanceSheetSummary>();
                foreach (var subhead in subheads)
                {
                    var subheadBalanceSheetSummary = new SubHeadBalanceSheetSummary();
                    subheadBalanceSheetSummary.debit = null;
                    subheadBalanceSheetSummary.credit = null;
                    subheadBalanceSheetSummary.amount = null;
                    subheadBalanceSheetSummary.accName = subhead;
                    subheadBalanceSheetSummary.__children = new List<AccNameBalanceSheetSummary>();
                    decimal totalAcc = 0;
                    foreach (var income in data.Where(x => x.MasterAccountsTableHead == subhead))
                    {
                        subheadBalanceSheetSummary.__children.Add(new AccNameBalanceSheetSummary()
                        {
                            accName = income.AccountsTransactionsAccName,
                            credit = income.AccountsTransactionsCredit.ToString(),
                            debit = income.AccountsTransactionsDebit.ToString(),
                            amount = (income.AccountsTransactionsCredit - income.AccountsTransactionsDebit).ToString()
                        });
                        totalAcc += income.AccountsTransactionsCredit - income.AccountsTransactionsDebit;
                    }
                    totalliabilities += totalAcc;
                    subheadBalanceSheetSummary.__children.Add(new AccNameBalanceSheetSummary()
                    {
                        accName = @$"Total {subhead}",
                        amount = totalAcc.ToString(),
                        credit = null,
                        debit = null
                    });
                    headBalanceSheetSummary.__children.Add(subheadBalanceSheetSummary);
                }
                headBalanceSheetSummary.__children.Add(new SubHeadBalanceSheetSummary()
                {
                    accName = "Total Liabilities",
                    debit = null,
                    credit = null,
                    amount = totalliabilities.ToString(),
                });
                return headBalanceSheetSummary;
            }
            catch (Exception)
            {
                return new GetBalanceSheetSummaryResponse();
            }
        }
        private async Task<Response<List<GetAccountTransactionBalanceSheetResponse>>> GetBalanceSheetByFilter(GenericGridViewModel model)
        {
            try
            {
                var result = await _balanceSheet.GetBySPWithParameters<GetAccountTransactionBalanceSheetResponse>(@$" exec GetAccountTransactionBalanceSheet  {model.Skip},{model.Take},{model.Search},{model.Field},{model.Dir},{model.Filter},{model.Total}", x => new GetAccountTransactionBalanceSheetResponse
                {
                    AccountsTransactionsAccNo = x.AccountsTransactionsAccNo,
                    AccountsTransactionsDebit = x.AccountsTransactionsDebit,
                    AccountsTransactionsCredit = x.AccountsTransactionsCredit,
                    AccountsTransactionsAllocBalance = x.AccountsTransactionsAllocBalance,
                    MasterAccountsTableMainHead = x.MasterAccountsTableMainHead,
                    MasterAccountsTableHead = x.MasterAccountsTableHead,
                    AccountsTransactionsTransDate = Convert.ToDateTime(x.AccountsTransactionsTransDate),
                    AccountsTransactionsTransDateString = Convert.ToDateTime(x.AccountsTransactionsTransDate).ToString("MM-dd-yyyy"),
                    RefNo = x.RefNo,
                    MasterAccountsTableSubHead = x.MasterAccountsTableRelativeNo,
                    AccountsTransactionsTransSno = x.AccountsTransactionsTransSno,
                    AccountsTransactionsAllocDebit = x.AccountsTransactionsAllocDebit,
                    AccountsTransactionsAllocCredit = x.AccountsTransactionsAllocCredit,
                    AccountsTransactionsAccName = x.AccountsTransactionsAccName,
                });
                return Response<List<GetAccountTransactionBalanceSheetResponse>>.Success(result, "Added");
            }
            catch (Exception ex)
            {
                return Response<List<GetAccountTransactionBalanceSheetResponse>>.Fail(new List<GetAccountTransactionBalanceSheetResponse>(), ex.Message);
            }
        }
        public async Task<Response<ReturnPrintResponse>> BalanceSheetPrint(GenericGridViewModel model)
        {
            try
            {
                string fileName = "BalanceSheet";
                ReturnPrintResponse response = new ReturnPrintResponse();
                var dbResult = await GetAccountTransactionsBalanceSheetDetail(model);
                if (dbResult.Result.Data.Count < 1)
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No records");
                }
                string cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Styles", "Account", "BalanceSheetStyle.css");
                string outPutPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Files", @$"{fileName}.pdf");
                await _fileService.CheckFileExist(outPutPath);
                AddPDFResponse pDFResponse = new AddPDFResponse()
                {
                    Html = GetHTMLString(dbResult.Result.Data),
                    CssPath = cssPath,
                    OutputPath = outPutPath
                };
                var pdf = await _fileService.CreatePDFFromHtml(pDFResponse);
                if (pdf.Result.Trim(' ') == "")
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No PDF Generated");
                }
                if (model.Extension == "PDF")
                {
                    var downloadPDF = await _fileService.DownloadFile(pdf.Result);
                    var pdfContentType = await _fileService.GetContentType(pdf.Result);
                    response.stream = downloadPDF.Result;
                    response.ContentType = pdfContentType.Result;
                    response.Path = pdf.Result;
                    return Response<ReturnPrintResponse>.Success(response, "File Created");
                }
                var file = await _fileService.CreateFileFromExtension(new GetFileFromExtensionResponse()
                {
                    Extension = model.Extension,
                    Path = pdf.Result,
                    FileName = fileName
                });
                if (file.Result.Trim(' ') == "")
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No File Generated");
                }
                var download = await _fileService.DownloadFile(file.Result);
                var ContentType = await _fileService.GetContentType(file.Result);
                response.stream = download.Result;
                response.ContentType = ContentType.Result;
                response.Path = file.Result;
                return Response<ReturnPrintResponse>.Success(response, "File Created");
            }
            catch (Exception ex)
            {
                return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), ex.Message);
            }
        }
        private string GetHTMLString(List<GetBalanceSheetDetailResponse> records)
        {
            var results = records;
            var sb = new StringBuilder();
            sb.Append(@" <h1>ACORTEC STRUCTURAL STEEL COATING & TR</h1>
                            <h4>AL QUOZ,DUBAI,UAE</h4>
                            <h4>97142824538,</h4>
                            <h4>97142824538</h4>
                            <h4>info@horizonmsuae.com</h4>
                            <h2> Balance Sheet </h2>
                            <h3> From 01/01/2022 To 31/12/2022 </h3>
<div style='background-color: #fff;margin-left: 10%;height: 100vh;'>
<div style='width:100%;display:flex;justify-content:space-between'>
                <div style='background-color: green;color: #fff; font-size: 14pt;'>
                    Particulars</div>
            </div>
                            ");
                //<div style='background-color: green;color: #fff; font-size: 14pt;'>
                //    (AED)</div>
            foreach (var dataObject in results)
            {
                sb.Append($"<h2>{dataObject?.mainHead}</h2>" +
                    $"<div style='background-color: #c8cec9;width:100%'>");
                foreach (var head in dataObject?.__children)
                {
                    if (head?.head != "Total Assets" && head?.head != "Total Equity" && head?.head != "Total Liabilities")
                    {
                        sb.Append(@$"<h2>{head?.head}</h2>");
                    }
                    if (head?.__children != null)
                        foreach (var head1 in head?.__children)
                        {
                            sb.Append($@"<div style='display: flex;justify-content: space-between;'>
                            <h4>{head1?.head}</h4>
                            <h4 style='margin-right:4rem'>{head1?.amount}</h4>
                        </div>");
                            {
                                if (head1?.__children != null)
                                    foreach (var head2 in head1?.__children)
                                {
                                    sb.Append(@$"<div style='width:100%;display:flex;padding:2rem;'>");
                                    sb.Append(@$"<div style='width:22%;'></div>
                            <div style='width:56%;font-family: Arial, Helvetica, sans-serif;font-size: 12pt;'>
                                {head2?.accName}
                            </div>
                            <div style='width:22%;font-family: Arial, Helvetica, sans-serif;font-size: 12pt;'>
                                {head2?.amount}
                            </div>
                            </div>
                            ");
                                }
                            }
                        }
                }
                sb.Append($"</div></div>");
            }
            return sb.ToString();
        }
        public async Task<Response<ReturnPrintResponse>> BalanceSheetPrintSummary(GenericGridViewModel model)
        {
            try
            {
                string fileName = "BalanceSheet";
                ReturnPrintResponse response = new ReturnPrintResponse();
                var dbResult = await GetAccountTransactionsBalanceSheetSummary(model);
                if (dbResult.Result.Data.Count < 1)
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No records");
                }
                string cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Styles", "Account", "BalanceSheetStyle.css");
                string outPutPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Files", @$"{fileName}.pdf");
                await _fileService.CheckFileExist(outPutPath);
                AddPDFResponse pDFResponse = new AddPDFResponse()
                {
                    Html = GetHTMLStringSummary(dbResult.Result.Data),
                    CssPath = cssPath,
                    OutputPath = outPutPath
                };
                var pdf = await _fileService.CreatePDFFromHtml(pDFResponse);
                if (pdf.Result.Trim(' ') == "")
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No PDF Generated");
                }
                if (model.Extension == "PDF")
                {
                    var downloadPDF = await _fileService.DownloadFile(pdf.Result);
                    var pdfContentType = await _fileService.GetContentType(pdf.Result);
                    response.stream = downloadPDF.Result;
                    response.ContentType = pdfContentType.Result;
                    response.Path = pdf.Result;
                    return Response<ReturnPrintResponse>.Success(response, "File Created");
                }
                var file = await _fileService.CreateFileFromExtension(new GetFileFromExtensionResponse()
                {
                    Extension = model.Extension,
                    Path = pdf.Result,
                    FileName = fileName
                });
                if (file.Result.Trim(' ') == "")
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No File Generated");
                }
                var download = await _fileService.DownloadFile(file.Result);
                var ContentType = await _fileService.GetContentType(file.Result);
                response.stream = download.Result;
                response.ContentType = ContentType.Result;
                response.Path = file.Result;
                return Response<ReturnPrintResponse>.Success(response, "File Created");
            }
            catch (Exception ex)
            {
                return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), ex.Message);
            }
        }
        private string GetHTMLStringSummary(List<GetBalanceSheetSummaryResponse> records)
        {
            var results = records;
            var sb = new StringBuilder();
            sb.Append(@" <h1>ACORTEC STRUCTURAL STEEL COATING & TR</h1>
                            <h4>AL QUOZ,DUBAI,UAE</h4>
                            <h4>97142824538,</h4>
                            <h4>97142824538</h4>
                            <h4>info@horizonmsuae.com</h4>
                            <h2> Balance Sheet </h2>
                            <h3> From 01/01/2022 To 31/12/2022 </h3>
<div style='background-color: #fff;margin-left: 10%;height: 100vh;'>
<div style='width:100%;display:flex;justify-content:space-between'>
                <div style='background-color: green;color: #fff; font-size: 14pt;'>
                    Particulars</div>
            </div>
                            ");
            //<div style='background-color: green;color: #fff; font-size: 14pt;'>
            //    (AED)</div>
            foreach (var dataObject in results)
            {
                sb.Append($"<h2>{dataObject?.head}</h2>" +
                    $"<div style='background-color: #c8cec9;width:100%'>");
                if (dataObject?.__children != null)
                    foreach (var head in dataObject?.__children)
                {
                    sb.Append(@$"<h2>{head?.accName}</h2>&nbsp; {head?.amount}");
                    sb.Append(@$"<div style='width:100%;display:flex;'>");
                    if (head?.__children != null)
                        foreach (var head1 in head?.__children)
                    {
                        sb.Append(@$"
<div style='font-family: Arial, Helvetica, sans-serif; font-size: 12pt;wdith:25%'>
<div style='display:flex'>             
<h5> Acc Name </h5>
<h5>
{head1?.accName}
</h5>
</div>
</div>
<div style='font-family: Arial, Helvetica, sans-serif; font-size: 12pt;wdith:25%'>
<div style='display:flex'>
<h5> Amount </h5>
<h5>
{head1?.amount}
</h5>
</div>
</div>
<div style='font-family: Arial, Helvetica, sans-serif; font-size: 12pt;wdith:25%'>
<div style='display:flex'>
<h5> Credit </h5>
<h5>{head1?.credit}</h5>
</div>
</div>
<div style='font-family: Arial, Helvetica, sans-serif; font-size: 12pt;wdith:25%'>
<div style='display:flex'>
<h5>Debit</h5>
<h5>{head1?.debit}</h5>
</div>
</div>");
                    }
                }
                sb.Append($"</div></div>");
            }
            return sb.ToString();
        }
    }
}
