using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Erp.Application.Account.Implementations
{
   public class AgeingReport: IAgeingReport
    {
        private readonly IRepository<AccountTransacionsAgeingReport> _accountTransactions;
        public AgeingReport(IRepository<AccountTransacionsAgeingReport> accountTransactions)
        {
            _accountTransactions = accountTransactions;
        }
        public async Task<Response<AgeingReportResponse>> GetAgeingReport(GenericGridViewModel model)
        {
            try
            {
                AgeingReportResponse gridmodel = new AgeingReportResponse();
                string query = @$" AccountsTransactionsAllocBalance >0  {model.Filter}";
                var result = await _accountTransactions.GetBySPWithParameters<AccountTransacionsAgeingReport>(@$" exec GetAccountTransactionsAgeingReport {model.Skip},{model.Take},{model.Search},{model.Field},{model.Dir},{model.Filter},{model.Total}", x => new AccountTransacionsAgeingReport
                {
                    AccountsTransactionsAccNo = x.AccountsTransactionsAccNo,
                    AccountsTransactionsAccName=x.AccountsTransactionsAccName,
                    AccountsTransactionsDebit = Convert.ToDecimal(x.AccountsTransactionsDebit),
                    AccountsTransactionsCredit = Convert.ToDecimal(x.AccountsTransactionsCredit),
                    AccountsTransactionsAllocBalance = x.AccountsTransactionsAllocBalance,
                  //  AccountsTransactionsVoucherNo = x.AccountsTransactionsVoucherNo,
                    AccountsTransactionsVoucherType = x.AccountsTransactionsVoucherType,
                    AccountsTransactionsDescription = x.AccountsTransactionsDescription,
                    AccountsTransactionsTransDate = Convert.ToDateTime(x.AccountsTransactionsTransDate),
                  //  AccountsTransactionsParticulars = x.AccountsTransactionsParticulars,
                    RefNo = x.RefNo,
                    AccountsTransactionsTransSno = Convert.ToInt32(x.AccountsTransactionsTransSno),
                    AccountsTransactionsAllocDebit = x.AccountsTransactionsAllocDebit,
                    AccountsTransactionsAllocCredit = x.AccountsTransactionsAllocCredit
                });

                gridmodel.Details = new List<AgeingReportDetailsResponse>();
                foreach (var items in result.GroupBy(x=>x.AccountsTransactionsAccNo))
                {
                    string accName = string.Empty;
                    string accNo = string.Empty;
                    decimal CrBal = 0;
                    decimal CrDys = 0;
                    decimal CrLimit = 0;
                    decimal UnAllocated = 0;
                    decimal outBalance = 0;
                    string tel1 = string.Empty;
                    string Contact_Person = string.Empty;
                    string CT_Name = string.Empty;
                    string Mobile = string.Empty;
                    string Remarks = string.Empty;
                    decimal runningBalance = 0;
                    decimal ZeroToThirty = 0;
                    decimal ThirtyOneToSixty = 0;
                    decimal SixtyOneToNinety = 0;
                    decimal NinetyOneToOneEighty = 0;
                    decimal OneEightOneToTwoSeventy = 0;
                    decimal TwoSeventyOneToThreeSixty = 0;
                    decimal aboveThreeSixty = 0;
                    foreach (var accounts in items)
                    {

                        CrBal += Convert.ToDecimal(accounts.AccountsTransactionsAllocBalance);
                        CrLimit += 0;
                        accName = accounts.AccountsTransactionsAccName;
                        accNo = accounts.AccountsTransactionsAccNo;
                        Remarks = accounts.AccountsTransactionsDescription;

                        //decimal totalDebit = accounts.AccountsTransactionsDebit;
                        //decimal totalCredit = accounts.AccountsTransactionsCredit;
                        //runningBalance = (totalDebit - totalCredit) + (runningBalance);
                        var days = Math.Round(((DateTime.Now - Convert.ToDateTime(accounts.AccountsTransactionsTransDate)).TotalDays));
                        CrDys += Convert.ToDecimal(days);
                        if (days >= 0 && days < 31)
                        {
                            ZeroToThirty += Convert.ToDecimal(accounts.AccountsTransactionsAllocBalance);
                        }
                        else if (days >= 31 && days < 60)
                        {
                            ThirtyOneToSixty += Convert.ToDecimal(accounts.AccountsTransactionsAllocBalance);
                        }
                        else if (days >= 61 && days < 90)
                        {
                            SixtyOneToNinety += Convert.ToDecimal(accounts.AccountsTransactionsAllocBalance);
                        }
                        else if (days >= 91 && days < 180)
                        {
                            NinetyOneToOneEighty += Convert.ToDecimal(accounts.AccountsTransactionsAllocBalance);
                        }
                        else if (days >= 181 && days < 270)
                        {
                            OneEightOneToTwoSeventy += Convert.ToDecimal(accounts.AccountsTransactionsAllocBalance);
                        }
                        else if (days >= 271 && days < 360)
                        {
                            TwoSeventyOneToThreeSixty += Convert.ToDecimal(accounts.AccountsTransactionsAllocBalance);
                        }
                        else if (days > 360)
                        {
                            aboveThreeSixty += Convert.ToDecimal(accounts.AccountsTransactionsAllocBalance);
                        }
                    }
                    gridmodel.Details.Add(new AgeingReportDetailsResponse()
                    {
                        CrBal = CrBal.ToString(),
                        CrDys = CrDys.ToString(),
                        CrLimit = CrLimit.ToString(),
                        Name = accName,
                        AccNo = accNo,
                        UnAllocated=UnAllocated.ToString(),
                        Contact_Person= Contact_Person,
                        CT_Name=CT_Name,
                        Mobile=Mobile,
                        NinetyOneToOneEighty=NinetyOneToOneEighty.ToString(),
                        OneEightOneToTwoSeventy=OneEightOneToTwoSeventy.ToString(),
                        OutBalance=outBalance.ToString(),
                        OverThreeSixty= aboveThreeSixty.ToString(),
                        Remarks= Remarks,
                        SixtyOneNinety=SixtyOneToNinety.ToString(),
                        Tel1=tel1,
                        ThirtyOneSixty=ThirtyOneToSixty.ToString(),
                        TwoSeventyOneToThreeSixty=TwoSeventyOneToThreeSixty.ToString(),
                        ZeroThirty=ZeroToThirty.ToString(),
                    });

                }
                return Response<AgeingReportResponse>.Success(gridmodel, "RecordFound");
            }
            catch (Exception ex)
            {
                return Response<AgeingReportResponse>.Fail(new AgeingReportResponse(), ex.Message);
            }
        }
    }
}
