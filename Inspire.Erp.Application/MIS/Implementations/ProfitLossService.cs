using Inspire.Erp.Application.MIS.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.MIS;
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using Spire.Pdf.Exporting.XPS.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.MIS.Implementations
{
    public class ProfitLossService : IProfitLossService
    {
        private readonly IRepository<FinancialPeriods> _financialPeriods;
        private readonly IRepository<AccountsTransactions> _accountTrans;
        private readonly IRepository<MasterAccountsTable> _masterAccount;
        private readonly IRepository<StationMaster> _stationMaster;
        private readonly IRepository<GetAccountTransactionProfitLossResponse> _profitLoss;
        public readonly InspireErpDBContext _Context;
        public ProfitLossService(IRepository<FinancialPeriods> financialPeriods, IRepository<GetAccountTransactionProfitLossResponse> profitLoss,
            IRepository<AccountsTransactions> accountTrans, IRepository<StationMaster> stationMaster,
            IRepository<MasterAccountsTable> masterAccount,
            InspireErpDBContext Context)
        {
            _financialPeriods = financialPeriods;
            _masterAccount = masterAccount;
            _accountTrans = accountTrans;
            _profitLoss = profitLoss;
            _stationMaster = stationMaster;
            _Context = Context;
        }
        public async Task<Response<GetFinancialYearResponse>> GetFinancialYearResponse()
        {
            try
            {
                var response = await _financialPeriods.FirstOrDefaultAsync(x => x.FinancialPeriodsStatus == "R", x => new GetFinancialYearResponse
                {
                    FinancialPeriodsDelStatus = x.FinancialPeriodsDelStatus,
                    FinancialPeriodsEndDate = x.FinancialPeriodsEndDate,
                    FinancialPeriodsFsno = x.FinancialPeriodsFsno,
                    FinancialPeriodsStartDate = x.FinancialPeriodsStartDate,
                    FinancialPeriodsStatus = x.FinancialPeriodsStatus,
                    FinancialPeriodsYearEndFile = x.FinancialPeriodsYearEndFile,
                    FinancialPeriodsYearEndJv = x.FinancialPeriodsYearEndJv
                });
                return Response<GetFinancialYearResponse>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GetFinancialYearResponse>.Fail(new GetFinancialYearResponse(), ex.Message);
            }
        }
        public async Task<Response<WrapperProfitLossPrintMonthWiseResponse>> GetAccountTransactionsProfitLossPrintMonthWise(GenericGridViewModel model)
        {
            try
            {
                WrapperProfitLossPrintMonthWiseResponse gridModel = new WrapperProfitLossPrintMonthWiseResponse();
                var location = await _stationMaster.GetAsQueryable().FirstOrDefaultAsync();
                string incomeQuery = @$"  {model.Filter} and ( mat.MA_MainHead= 'Income') ";
                string expenseQuery = @$"  {model.Filter}  and (mat.MA_MainHead= 'Expenses' ) ";
                decimal totalExpense = 0;
                decimal netTotal = 0;
                decimal totalIncome = 0;
                var profitLossList = new List<GetProfitLossResponse>();

                var incomes = await GetProfitLossByFilter(new GenericGridViewModel() { Filter = incomeQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                var expenses = await GetProfitLossByFilter(new GenericGridViewModel() { Filter = expenseQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });

                gridModel.Incomes = new List<ProfitLossPrintMonthWiseResponse>();
                foreach (var accounts in incomes.Result.GroupBy(x => x.AccountsTransactionsAccNo))
                {
                    decimal jan = 0;
                    decimal feb = 0;
                    decimal march = 0;
                    decimal april = 0;
                    decimal may = 0;
                    decimal june = 0;
                    decimal july = 0;
                    decimal august = 0;
                    decimal sept = 0;
                    decimal oct = 0;
                    decimal nov = 0;
                    decimal dec = 0;
                    foreach (var item in accounts)
                    {
                        if (item.AccountsTransactionsTransDate != null)
                        {
                            switch (item.AccountsTransactionsTransDate.ToString("MMM"))
                            {
                                case "Jan":
                                    jan += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Feb":
                                    feb += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Mar":
                                    march += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Apr":
                                    april += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "May":
                                    may += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Jun":
                                    june += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Jul":
                                    july += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Aug":
                                    august += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Sep":
                                    sept += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Oct":
                                    oct += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Nov":
                                    nov += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Dec":
                                    dec += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                            }
                        }
                        gridModel.Incomes.Add(new ProfitLossPrintMonthWiseResponse()
                        {
                            AccName = item.AccountsTransactionsAccName,
                            AccNo = item.AccountsTransactionsAccNo,
                            Jan = jan.ToString(),
                            Feb = feb.ToString(),
                            March = march.ToString(),
                            April = april.ToString(),
                            May = may.ToString(),
                            June = june.ToString(),
                            July = july.ToString(),
                            Aug = august.ToString(),
                            Sept = sept.ToString(),
                            Oct = sept.ToString(),
                            Nov = nov.ToString(),
                            Dec = nov.ToString(),
                            Total = (jan + feb + march + april + may + june + july + august + sept + oct + nov + dec).ToString()
                        });
                    }
                }
                gridModel.Incomes.Add(new ProfitLossPrintMonthWiseResponse()
                {
                    AccName = "Total Expense",
                    Jan = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Jan)).ToString(),
                    Feb = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Feb)).ToString(),
                    March = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.March)).ToString(),
                    April = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.April)).ToString(),
                    May = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.May)).ToString(),
                    June = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.June)).ToString(),
                    July = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.July)).ToString(),
                    Aug = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Aug)).ToString(),
                    Sept = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Sept)).ToString(),
                    Oct = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Oct)).ToString(),
                    Nov = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Nov)).ToString(),
                    Dec = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Dec)).ToString(),
                    Total = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Total)).ToString(),
                });
                totalIncome += gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Total));
                gridModel.Expenses = new List<ProfitLossPrintMonthWiseResponse>();
                foreach (var accounts in expenses.Result.GroupBy(x => x.AccountsTransactionsAccNo))
                {
                    decimal jan = 0;
                    decimal feb = 0;
                    decimal march = 0;
                    decimal april = 0;
                    decimal may = 0;
                    decimal june = 0;
                    decimal july = 0;
                    decimal august = 0;
                    decimal sept = 0;
                    decimal oct = 0;
                    decimal nov = 0;
                    decimal dec = 0;
                    foreach (var item in accounts)
                    {
                        if (item.AccountsTransactionsTransDate != null)
                        {
                            switch (item.AccountsTransactionsTransDate.ToString("MMM"))
                            {
                                case "Jan":
                                    jan += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Feb":
                                    feb += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Mar":
                                    march += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Apr":
                                    april += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "May":
                                    may += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Jun":
                                    june += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Jul":
                                    july += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Aug":
                                    august += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Sep":
                                    sept += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Oct":
                                    oct += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Nov":
                                    nov += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                                case "Dec":
                                    dec += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                                    break;
                            }
                        }
                        gridModel.Expenses.Add(new ProfitLossPrintMonthWiseResponse()
                        {
                            AccName = item.AccountsTransactionsAccName,
                            AccNo = item.AccountsTransactionsAccNo,
                            Jan = jan.ToString(),
                            Feb = feb.ToString(),
                            March = march.ToString(),
                            April = april.ToString(),
                            May = may.ToString(),
                            June = june.ToString(),
                            July = july.ToString(),
                            Aug = august.ToString(),
                            Sept = sept.ToString(),
                            Oct = sept.ToString(),
                            Nov = nov.ToString(),
                            Dec = nov.ToString(),
                            Total = (jan + feb + march + april + may + june + july + august + sept + oct + nov + dec).ToString()
                        });
                    }
                }
                gridModel.Expenses.Add(new ProfitLossPrintMonthWiseResponse()
                {
                    AccName = "Total Expense",
                    Jan = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Jan)).ToString(),
                    Feb = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Feb)).ToString(),
                    March = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.March)).ToString(),
                    April = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.April)).ToString(),
                    May = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.May)).ToString(),
                    June = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.June)).ToString(),
                    July = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.July)).ToString(),
                    Aug = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Aug)).ToString(),
                    Sept = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Sept)).ToString(),
                    Oct = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Oct)).ToString(),
                    Nov = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Nov)).ToString(),
                    Dec = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Dec)).ToString(),
                    Total = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Total)).ToString(),
                });
                totalExpense += gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Total));
                gridModel.StationMasterStationName = location != null ? location.StationMasterStationName : "";
                gridModel.StationMasterEmail = location != null ? location.StationMasterEmail : "";
                gridModel.StationMasterFax = location != null ? location.StationMasterFax : "";
                gridModel.StationMasterTele1 = location != null ? location.StationMasterTele1 : "";
                gridModel.StationMasterAddress = location != null ? location.StationMasterAddress : "";
                gridModel.StationMasterCity = location != null ? location.StationMasterCity : "";
                gridModel.StationMasterCode = location != null ? location.StationMasterCode : 0;
                gridModel.StationMasterCountry = location != null ? location.StationMasterCountry : "";
                gridModel.StationMasterLogoPath = location != null ? location.StationMasterLogoPath : "";
                gridModel.StationMasterPostOffice = location != null ? location.StationMasterPostOffice : "";
                gridModel.StationMasterSignPath = location != null ? location.StationMasterSignPath : "";
                gridModel.StationMasterVatNo = location != null ? location.StationMasterVatNo : "";
                gridModel.StationMasterWebSite = location != null ? location.StationMasterWebSite : "";
                gridModel.TotalIncome = totalIncome.ToString();
                gridModel.TotalExpense = totalExpense.ToString();
                gridModel.NetLoss = totalIncome - totalExpense < 0 ? (totalIncome - totalExpense).ToString() : 0.ToString();
                return Response<WrapperProfitLossPrintMonthWiseResponse>.Success(gridModel, "Data found");
            }
            catch (Exception ex)
            {
                return Response<WrapperProfitLossPrintMonthWiseResponse>.Fail(new WrapperProfitLossPrintMonthWiseResponse(), ex.Message);
            }
        }
        public async Task<Response<WrapperProfitLossPrintSimpleResponse>> GetAccountTransactionsProfitLossPrintSimple(GenericGridViewModel model)
        {
            try
            {
                WrapperProfitLossPrintSimpleResponse gridModel = new WrapperProfitLossPrintSimpleResponse();
                var location = await _stationMaster.GetAsQueryable().FirstOrDefaultAsync();
                string incomeQuery = @$"  {model.Filter} and ( mat.MA_MainHead= 'Income') ";
                string expenseQuery = @$"  {model.Filter}  and (mat.MA_MainHead= 'Expenses' ) ";
                decimal totalExpense = 0;
                decimal netTotal = 0;
                decimal totalIncome = 0;
                var profitLossList = new List<GetProfitLossResponse>();

                var incomes = await GetProfitLossByFilter(new GenericGridViewModel() { Filter = incomeQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                var expenses = await GetProfitLossByFilter(new GenericGridViewModel() { Filter = expenseQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });

                gridModel.Incomes = new List<ProfitLossPrintSimpleResponse>();
                foreach (var item in incomes.Result)
                {
                    gridModel.Incomes.Add(new ProfitLossPrintSimpleResponse()
                    {
                        AccName = item.AccountsTransactionsAccName,
                        Balance = (item.AccountsTransactionsCredit - item.AccountsTransactionsDebit).ToString(),
                        Credit = item.AccountsTransactionsCredit.ToString(),
                        Debit = item.AccountsTransactionsDebit.ToString(),
                    });
                }
                totalIncome += gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Balance));
                gridModel.Incomes.Add(new ProfitLossPrintSimpleResponse()
                {
                    AccName = "Total Income",
                    Balance = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Balance)).ToString(),
                    Credit = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Credit)).ToString(),
                    Debit = gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Debit)).ToString(),
                });
               
                gridModel.Expenses = new List<ProfitLossPrintSimpleResponse>();
                foreach (var item in expenses.Result)
                {
                    gridModel.Expenses.Add(new ProfitLossPrintSimpleResponse()
                    {
                        AccName = item.AccountsTransactionsAccName,
                        Balance = (item.AccountsTransactionsCredit - item.AccountsTransactionsDebit).ToString(),
                        Credit = item.AccountsTransactionsCredit.ToString(),
                        Debit = item.AccountsTransactionsDebit.ToString(),
                    });
                }
                totalExpense += gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Balance));
                gridModel.Expenses.Add(new ProfitLossPrintSimpleResponse()
                {
                    AccName = "Total Expense",
                    Balance = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Balance)).ToString(),
                    Credit = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Credit)).ToString(),
                    Debit = gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Debit)).ToString(),
                });
                gridModel.StationMasterStationName = location != null ? location.StationMasterStationName : "";
                gridModel.StationMasterEmail = location != null ? location.StationMasterEmail : "";
                gridModel.StationMasterFax = location != null ? location.StationMasterFax : "";
                gridModel.StationMasterTele1 = location != null ? location.StationMasterTele1 : "";
                gridModel.StationMasterAddress = location != null ? location.StationMasterAddress : "";
                gridModel.StationMasterCity = location != null ? location.StationMasterCity : "";
                gridModel.StationMasterCode = location != null ? location.StationMasterCode : 0;
                gridModel.StationMasterCountry = location != null ? location.StationMasterCountry : "";
                gridModel.StationMasterLogoPath = location != null ? location.StationMasterLogoPath : "";
                gridModel.StationMasterPostOffice = location != null ? location.StationMasterPostOffice : "";
                gridModel.StationMasterSignPath = location != null ? location.StationMasterSignPath : "";
                gridModel.StationMasterVatNo = location != null ? location.StationMasterVatNo : "";
                gridModel.StationMasterWebSite = location != null ? location.StationMasterWebSite : "";
                gridModel.TotalIncome = totalIncome.ToString();
                gridModel.TotalExpense = totalExpense.ToString();
                gridModel.NetLoss = totalIncome - totalExpense < 0 ? (totalIncome - totalExpense).ToString() : 0.ToString();
                return Response<WrapperProfitLossPrintSimpleResponse>.Success(gridModel, "Data found");
            }
            catch (Exception ex)
            {
                return Response<WrapperProfitLossPrintSimpleResponse>.Fail(new WrapperProfitLossPrintSimpleResponse(), ex.Message);
            }
        }
        public async Task<Response<ProfitLossWrapper>> GetAccountTransactionsProfitLoss(GenericGridViewModel model)
        {
            try
            {
                ProfitLossWrapper gridModel = new ProfitLossWrapper();
                var location = await _stationMaster.GetAsQueryable().FirstOrDefaultAsync();
                string incomeQuery = @$"  {model.Filter} and ( mat.MA_MainHead= 'Income') ";
                string expenseQuery = @$"  {model.Filter}  and (mat.MA_MainHead= 'Expenses' ) ";
                decimal totalExpense = 0;
                decimal netTotal = 0;
                decimal totalIncome = 0;
                var profitLossList = new List<GetProfitLossResponse>();

                var incomes = await GetProfitLossByFilter(new GenericGridViewModel() { Filter = incomeQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                var expenses = await GetProfitLossByFilter(new GenericGridViewModel() { Filter = expenseQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                profitLossList.Add(GetProfitLossIncome(incomes.Result, ref totalIncome));
                profitLossList.Add(GetProfitLossExpense(expenses.Result, ref totalExpense));
                profitLossList.Add(new GetProfitLossResponse()
                {
                    head = null,
                    accName = null,
                    amount = (totalIncome - totalExpense).ToString(),
                    credit = null,
                    debit = null,
                    subHead = "Total",
                    __children = new List<SubHeadProfitLoss>()
                });
                gridModel.Details = new List<GetProfitLossResponse>();
                gridModel.Details = profitLossList;
                gridModel.StationMasterStationName = location != null ? location.StationMasterStationName : "";
                gridModel.StationMasterEmail = location != null ? location.StationMasterEmail : "";
                gridModel.StationMasterFax = location != null ? location.StationMasterFax : "";
                gridModel.StationMasterTele1 = location != null ? location.StationMasterTele1 : "";
                gridModel.StationMasterAddress = location != null ? location.StationMasterAddress : "";
                gridModel.StationMasterCity = location != null ? location.StationMasterCity : "";
                gridModel.StationMasterCode = location != null ? location.StationMasterCode : 0;
                gridModel.StationMasterCountry = location != null ? location.StationMasterCountry : "";
                gridModel.StationMasterLogoPath = location != null ? location.StationMasterLogoPath : "";
                gridModel.StationMasterPostOffice = location != null ? location.StationMasterPostOffice : "";
                gridModel.StationMasterSignPath = location != null ? location.StationMasterSignPath : "";
                gridModel.StationMasterVatNo = location != null ? location.StationMasterVatNo : "";
                gridModel.StationMasterWebSite = location != null ? location.StationMasterWebSite : "";
                gridModel.TotalIncome = totalIncome.ToString();
                gridModel.TotalExpense = totalExpense.ToString();
                gridModel.NetLoss = totalIncome - totalExpense < 0 ? (totalIncome - totalExpense).ToString() : 0.ToString();
                return Response<ProfitLossWrapper>.Success(gridModel, "Data found");
            }
            catch (Exception ex)
            {
                return Response<ProfitLossWrapper>.Fail(new ProfitLossWrapper(), ex.Message);
            }
        }
        public async Task<Response<ProfitAndLossWrapper>> GetAccountTransactionsProfitAndLoss(GenericGridViewModel model)
        {
            try
            {
                ProfitAndLossWrapper gridModel = new ProfitAndLossWrapper();
                var location = await _stationMaster.GetAsQueryable().FirstOrDefaultAsync();

                //var trans = _accountTrans.GetAsQueryable().Where(x => (model.FromDate == null || x.AccountsTransactionsTransDate >= model.FromDate)
                //                               && (model.ToDate == null || x.AccountsTransactionsTransDate <= model.ToDate));
                //&& x.AccountsTransactionsAccNo== "IN33");

                //gridModel.TotalSalesDebit = trans.Sum(survey => survey.AccountsTransactionsDebit);
                //gridModel.TotalSalesCredit = trans.Sum(survey => survey.AccountsTransactionsCredit);
                //gridModel.SalesAccount = "Sales Income";

                //var transSalesR = _accountTrans.GetAsQueryable().Where(x => (model.FromDate == null || x.AccountsTransactionsTransDate >= model.FromDate)
                //                               && (model.ToDate == null || x.AccountsTransactionsTransDate <= model.ToDate));
              //&& x.AccountsTransactionsAccNo == "IN3492");

                //gridModel.TotalSalesReturnDebit = transSalesR.Sum(survey => survey.AccountsTransactionsDebit);
                //gridModel.TotalSalesReturnCredit = transSalesR.Sum(survey => survey.AccountsTransactionsCredit);

                gridModel.IncomeDetails = await (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaMainHead.ToUpper() == "INCOME")
                                                  join at in _accountTrans.GetAsQueryable()
                                                                 .Where(x => (model.FromDate == null || x.AccountsTransactionsTransDate >= model.FromDate)
                                               && (model.ToDate == null || x.AccountsTransactionsTransDate <= model.ToDate))
                                                  on ma.MaAccNo equals at.AccountsTransactionsAccNo into g
                                                  from grd in g.DefaultIfEmpty()
                                                  group grd by new
                                                  {
                                                      grd.AccountsTransactionsAccNo,
                                                      ma.MaAccName,
                                                      ma.MaMainHead,
                                                      ma.MaSubHead
                                                  } into gp

                                                  where gp.Sum(x => x.AccountsTransactionsCredit) > 0 || gp.Sum(x => x.AccountsTransactionsDebit) > 0
                                                  select new GetProfitAndLossResponse
                                                  {
                                                      Head = gp.Key.MaSubHead,
                                                      SubHead = gp.Key.MaSubHead,
                                                      AccName = gp.Key.MaAccName,
                                                      Debit = gp.Sum(pc => pc.AccountsTransactionsDebit),
                                                      Credit = gp.Sum(pc => pc.AccountsTransactionsCredit)
                                                  })

                         .OrderBy(t => t.AccName).ToListAsync();


                gridModel.ExpenseDetails = await (from ma in _masterAccount.GetAsQueryable().Where(y => y.MaMainHead.ToUpper() == "EXPENSES")
                                                  join at in _accountTrans.GetAsQueryable()
                                                                .Where(x => (model.FromDate == null || x.AccountsTransactionsTransDate >= model.FromDate)
                                               && (model.ToDate == null || x.AccountsTransactionsTransDate <= model.ToDate))
                                                  on ma.MaAccNo equals at.AccountsTransactionsAccNo into g
                                                  from grd in g.DefaultIfEmpty()
                                                  group grd by new
                                                  {
                                                      grd.AccountsTransactionsAccNo,
                                                      ma.MaAccName,
                                                      ma.MaMainHead,
                                                      ma.MaSubHead
                                                  } into gp

                                                  where gp.Sum(x => x.AccountsTransactionsCredit) > 0 || gp.Sum(x => x.AccountsTransactionsDebit) > 0
                                                  select new GetProfitAndLossResponse
                                           {
                                               Head = gp.Key.MaSubHead,
                                               SubHead = gp.Key.MaSubHead,
                                               AccName = gp.Key.MaAccName,
                                               Debit = gp.Sum(pc => pc.AccountsTransactionsDebit),
                                               Credit = gp.Sum(pc => pc.AccountsTransactionsCredit)
                                           })
                  
                         .OrderBy(t => t.AccName).ToListAsync();

                //              var list = _accountTrans.GetAsQueryable().GroupBy(t => t.Id)
                //.Select(t => new { ID = t.Key, Value = t.Sum(u => u.Value) }).ToList();


                //decimal totalExpense = 0;
                //decimal netTotal = 0;
                //decimal totalIncome = 0;
                //var profitLossList = new List<GetProfitAndLossResponse>();

        
                //gridModel.StationMasterStationName = location != null ? location.StationMasterStationName : "";
                //gridModel.StationMasterEmail = location != null ? location.StationMasterEmail : "";
                //gridModel.StationMasterFax = location != null ? location.StationMasterFax : "";
                //gridModel.StationMasterTele1 = location != null ? location.StationMasterTele1 : "";
                //gridModel.StationMasterAddress = location != null ? location.StationMasterAddress : "";
                //gridModel.StationMasterCity = location != null ? location.StationMasterCity : "";
                //gridModel.StationMasterCode = location != null ? location.StationMasterCode : 0;
                //gridModel.StationMasterCountry = location != null ? location.StationMasterCountry : "";
                //gridModel.StationMasterLogoPath = location != null ? location.StationMasterLogoPath : "";
                //gridModel.StationMasterPostOffice = location != null ? location.StationMasterPostOffice : "";
                //gridModel.StationMasterSignPath = location != null ? location.StationMasterSignPath : "";
                //gridModel.StationMasterVatNo = location != null ? location.StationMasterVatNo : "";
                //gridModel.StationMasterWebSite = location != null ? location.StationMasterWebSite : "";
                //gridModel.TotalIncome = totalIncome.ToString();
                //gridModel.TotalExpense = totalExpense.ToString();
                //gridModel.NetLoss = totalIncome - totalExpense < 0 ? (totalIncome - totalExpense).ToString() : 0.ToString();
                return Response<ProfitAndLossWrapper>.Success(gridModel, "Data found");
            }
            catch (Exception ex)
            {
                return Response<ProfitAndLossWrapper>.Fail(new ProfitAndLossWrapper(), ex.Message);
            }
        }
        private GetProfitLossResponse GetProfitLossIncome(List<GetAccountTransactionProfitLossResponse> incomes, ref decimal totalIncome)
        {
            try
            {
                var incomesubhead = incomes.Select(x => x.MasterAccountsTableSubHead).Distinct().ToList();
                var headprofitLoss = new GetProfitLossResponse();
                headprofitLoss.head = "Income";
                headprofitLoss.subHead = null;
                headprofitLoss.debit = null;
                headprofitLoss.credit = null;
                headprofitLoss.amount = null;
                headprofitLoss.__children = new List<SubHeadProfitLoss>(); ;
                foreach (var subhead in incomesubhead)
                {
                    var subheadprofitLoss = new SubHeadProfitLoss();
                    subheadprofitLoss.subHead = subhead;
                    subheadprofitLoss.debit = null;
                    subheadprofitLoss.credit = null;
                    subheadprofitLoss.amount = null;
                    subheadprofitLoss.__children = new List<AccNameProfitLoss>();
                    decimal totalAcc = 0;
                    foreach (var income in incomes.Where(x => x.MasterAccountsTableSubHead == subhead))
                    {

                        subheadprofitLoss.__children.Add(new AccNameProfitLoss()
                        {
                            accName = income.AccountsTransactionsAccName,
                            credit = income.AccountsTransactionsCredit.ToString(),
                            debit = income.AccountsTransactionsDebit.ToString(),
                            amount = (income.AccountsTransactionsCredit - income.AccountsTransactionsDebit).ToString()
                        });
                        totalAcc += income.AccountsTransactionsCredit - income.AccountsTransactionsDebit;
                    }
                    totalIncome += totalAcc;
                    subheadprofitLoss.__children.Add(new AccNameProfitLoss()
                    {
                        accName = @$"Total {subhead}",
                        amount = totalAcc.ToString(),
                        credit = null,
                        debit = null
                    });

                    headprofitLoss.__children.Add(subheadprofitLoss);
                }
                headprofitLoss.__children.Add(new SubHeadProfitLoss()
                {
                    subHead = "Total Income",
                    debit = null,
                    credit = null,
                    amount = totalIncome.ToString(),
                });
                return headprofitLoss;
            }
            catch (Exception)
            {
                return new GetProfitLossResponse();
            }
        }
        private GetProfitLossResponse GetProfitLossExpense(List<GetAccountTransactionProfitLossResponse> incomes, ref decimal totalExpense)
        {
            try
            {
                var subheads = incomes.Select(x => x.MasterAccountsTableSubHead).Distinct().ToList();
                var headprofitLoss = new GetProfitLossResponse();
                headprofitLoss.head = "Expenses";
                headprofitLoss.subHead = null;
                headprofitLoss.debit = null;
                headprofitLoss.credit = null;
                headprofitLoss.amount = null;
                headprofitLoss.__children = new List<SubHeadProfitLoss>();
                foreach (var subhead in subheads)
                {
                    var subheadprofitLoss = new SubHeadProfitLoss();
                    subheadprofitLoss.subHead = subhead;
                    subheadprofitLoss.debit = null;
                    subheadprofitLoss.credit = null;
                    subheadprofitLoss.amount = null;
                    subheadprofitLoss.__children = new List<AccNameProfitLoss>();
                    decimal totalAcc = 0;
                    foreach (var income in incomes.Where(x => x.MasterAccountsTableSubHead == subhead))
                    {

                        subheadprofitLoss.__children.Add(new AccNameProfitLoss()
                        {
                            accName = income.AccountsTransactionsAccName,
                            credit = income.AccountsTransactionsCredit.ToString(),
                            debit = income.AccountsTransactionsDebit.ToString(),
                            amount = (income.AccountsTransactionsCredit - income.AccountsTransactionsDebit).ToString()
                        });
                        totalAcc += income.AccountsTransactionsCredit - income.AccountsTransactionsDebit;
                    }
                    totalExpense += totalAcc;
                    subheadprofitLoss.__children.Add(new AccNameProfitLoss()
                    {
                        accName = @$"Total {subhead}",
                        amount = totalAcc.ToString(),
                        credit = null,
                        debit = null
                    });

                    headprofitLoss.__children.Add(subheadprofitLoss);
                }
                headprofitLoss.__children.Add(new SubHeadProfitLoss()
                {
                    subHead = "Total Income",
                    debit = null,
                    credit = null,
                    amount = totalExpense.ToString(),
                });
                return headprofitLoss;
            }
            catch (Exception)
            {
                return new GetProfitLossResponse();
            }
        }
        private async Task<Response<List<GetAccountTransactionProfitLossResponse>>> GetProfitLossByFilter(GenericGridViewModel model)
        {
            try
            {
                var result = await _profitLoss.GetBySPWithParameters<GetAccountTransactionProfitLossResponse>(@$" exec GetAccountTransactionsProfitLoss  {model.Skip},{model.Take},{model.Search},{model.Field},{model.Dir},{model.Filter},{model.Total}", x => new GetAccountTransactionProfitLossResponse
                {
                    AccountsTransactionsAccNo = x.AccountsTransactionsAccNo,
                    AccountsTransactionsDebit = x.AccountsTransactionsDebit,
                    AccountsTransactionsCredit = x.AccountsTransactionsCredit,
                    AccountsTransactionsAllocBalance = x.AccountsTransactionsAllocBalance,
                    MasterAccountsTableSubHead = x.MasterAccountsTableSubHead,
                    MasterAccountsTableHead = x.MasterAccountsTableHead,
                    AccountsTransactionsTransDate = Convert.ToDateTime(x.AccountsTransactionsTransDate),
                    AccountsTransactionsTransDateString = Convert.ToDateTime(x.AccountsTransactionsTransDate).ToString("MM-dd-yyyy"),
                    RefNo = x.RefNo,
                    AccountsTransactionsTransSno = x.AccountsTransactionsTransSno,
                    AccountsTransactionsAllocDebit = x.AccountsTransactionsAllocDebit,
                    AccountsTransactionsAllocCredit = x.AccountsTransactionsAllocCredit,
                    AccountsTransactionsAccName = x.AccountsTransactionsAccName,
                    MARelativeNo = x.MARelativeNo,
                    AccountsTransactionsParticulars=x.AccountsTransactionsParticulars
                });
                return Response<List<GetAccountTransactionProfitLossResponse>>.Success(result, "Added");
            }
            catch (Exception ex)
            {
                return Response<List<GetAccountTransactionProfitLossResponse>>.Fail(new List<GetAccountTransactionProfitLossResponse>(), ex.Message);
            }
        }
        public async Task<Response<WrapperProfitLossPrintSummaryResponse>> GetAccountTransactionsProfitLossPrintSummary(GenericGridViewModel model)
        {
            try
            {
                WrapperProfitLossPrintSummaryResponse gridModel = new WrapperProfitLossPrintSummaryResponse();
                var location = await _stationMaster.GetAsQueryable().FirstOrDefaultAsync();
                string incomeQuery = @$"  {model.Filter} and ( mat.MA_MainHead= 'Income') ";
                string expenseQuery = @$"  {model.Filter}  and (mat.MA_MainHead= 'Expenses' ) ";
                decimal totalExpense = 0;
                decimal netTotal = 0;
                decimal totalIncome = 0;

                var incomes = await GetProfitLossByFilter(new GenericGridViewModel() { Filter = incomeQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                var expenses = await GetProfitLossByFilter(new GenericGridViewModel() { Filter = expenseQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                gridModel.Incomes = new List<ProfitLossPrintSummaryResponse>();
                foreach (var items in incomes.Result.GroupBy(x => x.MARelativeNo))
                {
                    foreach (var item in items)
                    {
                        gridModel.Incomes.Add(new ProfitLossPrintSummaryResponse()
                        {
                            AccName = item.AccountsTransactionsParticulars,
                            Credit = item.AccountsTransactionsCredit.ToString(),
                            Debit = item.AccountsTransactionsDebit.ToString(),
                            RunningBalance = (item.AccountsTransactionsCredit - item.AccountsTransactionsDebit).ToString()
                        });
                        totalIncome += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                    }
                    gridModel.Incomes.Add(new ProfitLossPrintSummaryResponse()
                    {
                        AccName = "Total",
                        Credit = (gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Credit))).ToString(),
                        Debit = (gridModel.Incomes.Sum(x => Convert.ToDecimal(x.Debit))).ToString(),
                        RunningBalance = (gridModel.Incomes.Sum(x=>Convert.ToDecimal(x.RunningBalance))).ToString()
                    });
                }

                gridModel.Expenses = new List<ProfitLossPrintSummaryResponse>();
                foreach (var items in expenses.Result.GroupBy(x => x.MARelativeNo))
                {
                    foreach (var item in items)
                    {
                        gridModel.Expenses.Add(new ProfitLossPrintSummaryResponse()
                        {
                            AccName = item.AccountsTransactionsParticulars,
                            Credit = item.AccountsTransactionsCredit.ToString(),
                            Debit = item.AccountsTransactionsDebit.ToString(),
                            RunningBalance = (item.AccountsTransactionsCredit - item.AccountsTransactionsDebit).ToString()
                        });
                        totalExpense += item.AccountsTransactionsCredit - item.AccountsTransactionsDebit;
                    }
                    gridModel.Expenses.Add(new ProfitLossPrintSummaryResponse()
                    {
                        AccName = "Total",
                        Credit = (gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Credit))).ToString(),
                        Debit = (gridModel.Expenses.Sum(x => Convert.ToDecimal(x.Debit))).ToString(),
                        RunningBalance = (gridModel.Expenses.Sum(x => Convert.ToDecimal(x.RunningBalance))).ToString()
                    });
                }

                gridModel.StationMasterStationName = location != null ? location.StationMasterStationName : "";
                gridModel.StationMasterEmail = location != null ? location.StationMasterEmail : "";
                gridModel.StationMasterFax = location != null ? location.StationMasterFax : "";
                gridModel.StationMasterTele1 = location != null ? location.StationMasterTele1 : "";
                gridModel.StationMasterAddress = location != null ? location.StationMasterAddress : "";
                gridModel.StationMasterCity = location != null ? location.StationMasterCity : "";
                gridModel.StationMasterCode = location != null ? location.StationMasterCode : 0;
                gridModel.StationMasterCountry = location != null ? location.StationMasterCountry : "";
                gridModel.StationMasterLogoPath = location != null ? location.StationMasterLogoPath : "";
                gridModel.StationMasterPostOffice = location != null ? location.StationMasterPostOffice : "";
                gridModel.StationMasterSignPath = location != null ? location.StationMasterSignPath : "";
                gridModel.StationMasterVatNo = location != null ? location.StationMasterVatNo : "";
                gridModel.StationMasterWebSite = location != null ? location.StationMasterWebSite : "";
                gridModel.TotalIncome = totalIncome.ToString();
                gridModel.TotalExpense = totalExpense.ToString();
                gridModel.NetLoss = totalIncome - totalExpense < 0 ? (totalIncome - totalExpense).ToString() : 0.ToString();
                return Response<WrapperProfitLossPrintSummaryResponse>.Success(gridModel, "Data found");
            }
            catch (Exception ex)
            {
                return Response<WrapperProfitLossPrintSummaryResponse>.Fail(new WrapperProfitLossPrintSummaryResponse(), ex.Message);
            }
        }
    }
}
