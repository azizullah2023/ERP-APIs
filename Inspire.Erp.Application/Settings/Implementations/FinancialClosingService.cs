using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.MIS.Interfaces;
using Inspire.Erp.Application.Settings.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Settings;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Settings.Implementations
{
   public class FinancialClosingService: IFinancialClosingService
    {
        private readonly IRepository<AccountsTransactions> _accountTransaction;
        private readonly IUtilityService _utilityService;
        private readonly IProfitLossService _profitLoss;
        private readonly IRepository<GetATFinancialClosingResponse> _financialClosing;
        public FinancialClosingService(IUtilityService utilityService, IProfitLossService profitLoss, IRepository<AccountsTransactions> accountTransaction,
            IRepository<GetATFinancialClosingResponse> financialClosing)
        {
            _utilityService = utilityService;
            _accountTransaction = accountTransaction;
            _financialClosing = financialClosing;
            _profitLoss = profitLoss;
        }
        private async Task<Response<List<GetATFinancialClosingResponse>>> GetFinancialClossingByFilter(GenericGridViewModel model)
        {
            try
            {
                var result = await _financialClosing.GetBySPWithParameters<GetATFinancialClosingResponse>(@$" exec GetAccountTransactionFPClosing  {model.Skip},{model.Take},{model.Search},{model.Field},{model.Dir},{model.Filter},{model.Total}", x => new GetATFinancialClosingResponse
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
                return Response<List<GetATFinancialClosingResponse>>.Success(result, "Added");
            }
            catch (Exception ex)
            {
                return Response<List<GetATFinancialClosingResponse>>.Fail(new List<GetATFinancialClosingResponse>(), ex.Message);
            }
        }
        public async Task<Response<GridWrapperResponse<List<FinancialClosingResponse>>>> GetFinancialClosingAsset(GenericGridViewModel model)
        {
            try
            {
                string assetsQuery = @$"  {model.Filter} and ( mat.MA_MainHead= 'Assets') ";
                decimal totalasset = 0;
                var BalanceSheetSummaryList = new List<FinancialClosingResponse>();
                var assets = await GetFinancialClossingByFilter(new GenericGridViewModel() { Filter = assetsQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                BalanceSheetSummaryList.Add(GetFinancialClossingAsset(assets.Result, ref totalasset));
                var gridReponse = new GridWrapperResponse<List<FinancialClosingResponse>>();
                gridReponse.Data = BalanceSheetSummaryList;
                var total = 0;
                gridReponse.Total = Convert.ToInt32(total);
                return Response<GridWrapperResponse<List<FinancialClosingResponse>>>.Success(gridReponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<FinancialClosingResponse>>>.Fail(new GridWrapperResponse<List<FinancialClosingResponse>>(), ex.Message);
            }
        }
        public async Task<Response<GridWrapperResponse<List<FinancialClosingResponse>>>> GetFinancialClosingLiability(GenericGridViewModel model)
        {
            try
            {
                string liabilitiesQuery = @$"  {model.Filter}  and (mat.MA_MainHead= 'Liabilities' ) ";
                decimal totalLiabilities = 0;
                var BalanceSheetSummaryList = new List<FinancialClosingResponse>();
                var liabilities = await GetFinancialClossingByFilter(new GenericGridViewModel() { Filter = liabilitiesQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                var profitloss = await _profitLoss.GetAccountTransactionsProfitLoss(new GenericGridViewModel() { Filter = model.Filter, Search = model.Search, Take = model.Take, Skip = model.Skip });
             
                BalanceSheetSummaryList.Add(GetFinancialClossingLiability(liabilities.Result, ref totalLiabilities));
                var gridReponse = new GridWrapperResponse<List<FinancialClosingResponse>>();
                gridReponse.Data = BalanceSheetSummaryList;
                var total = 0;
                gridReponse.Total = Convert.ToInt32(total);
                return Response<GridWrapperResponse<List<FinancialClosingResponse>>>.Success(gridReponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<FinancialClosingResponse>>>.Fail(new GridWrapperResponse<List<FinancialClosingResponse>>(), ex.Message);
            }
        }
        public async Task<Response<GridWrapperResponse<List<FinancialClosingResponse>>>> GetFinancialClosingEquity(GenericGridViewModel model)
        {
            try
            {

                string equityQuery = @$"  {model.Filter}  and (mat.MA_MainHead= 'Equity' ) ";
                decimal totalequity = 0;
                var BalanceSheetSummaryList = new List<FinancialClosingResponse>();
                var equitys = await GetFinancialClossingByFilter(new GenericGridViewModel() { Filter = equityQuery, Search = model.Search, Take = model.Take, Skip = model.Skip });
                var profitloss = await _profitLoss.GetAccountTransactionsProfitLoss(new GenericGridViewModel() { Filter = model.Filter, Search = model.Search, Take = model.Take, Skip = model.Skip });
                var listequity = new List<GetATFinancialClosingResponse>();
                listequity.AddRange(equitys.Result);
                listequity.Add(new GetATFinancialClosingResponse()
                {
                    MasterAccountsTableMainHead = "Equity",
                    AccountsTransactionsAccName = profitloss.Result.Details[2].accName,
                    MasterAccountsTableHead = "Profit Loss",
                    MasterAccountsTableSubHead = "Profit Loss",
                    MasterAccountsTableRelativeNo = "Profit Loss",
                    AccountsTransactionsCredit = Convert.ToDecimal(Convert.ToDecimal(profitloss.Result.Details[2].amount) + Convert.ToDecimal(profitloss.Result.Details[2].amount) / 2),
                    AccountsTransactionsDebit = Convert.ToDecimal(Convert.ToDecimal(profitloss.Result.Details[2].amount) - Convert.ToDecimal(profitloss.Result.Details[2].amount) / 2)
                });
                BalanceSheetSummaryList.Add(GetFinancialClossingEquity(listequity, ref totalequity));
                var gridReponse = new GridWrapperResponse<List<FinancialClosingResponse>>();
                gridReponse.Data = BalanceSheetSummaryList;
                var total = 0;
                gridReponse.Total = Convert.ToInt32(total);
                return Response<GridWrapperResponse<List<FinancialClosingResponse>>>.Success(gridReponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<FinancialClosingResponse>>>.Fail(new GridWrapperResponse<List<FinancialClosingResponse>>(), ex.Message);
            }
        }
        private FinancialClosingResponse GetFinancialClossingAsset(List<GetATFinancialClosingResponse> data, ref decimal totalAsset)
        {
            try
            {
                var financialClosing = new FinancialClosingResponse();
                var heads = data.Select(x => x.MasterAccountsTableHead).Distinct().ToList();
                var subheads = data.Select(x => x.MasterAccountsTableSubHead).Distinct().ToList();

             
                return financialClosing;
            }
            catch (Exception)
            {
                return new FinancialClosingResponse();
            }
        }
        private FinancialClosingResponse GetFinancialClossingEquity(List<GetATFinancialClosingResponse> data, ref decimal totalAsset)
        {
            try
            {
                var financialClosing = new FinancialClosingResponse();
                var heads = data.Select(x => x.MasterAccountsTableHead).Distinct().ToList();
                var subheads = data.Select(x => x.MasterAccountsTableSubHead).Distinct().ToList();


                return financialClosing;
            }
            catch (Exception)
            {
                return new FinancialClosingResponse();
            }
        }
        private FinancialClosingResponse GetFinancialClossingLiability(List<GetATFinancialClosingResponse> data, ref decimal totalAsset)
        {
            try
            {
                var financialClosing = new FinancialClosingResponse();
                var heads = data.Select(x => x.MasterAccountsTableHead).Distinct().ToList();
                var subheads = data.Select(x => x.MasterAccountsTableSubHead).Distinct().ToList();


                return financialClosing;
            }
            catch (Exception)
            {
                return new FinancialClosingResponse();
            }
        }

    }
}
