using Inspire.Erp.Application.Account.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using System.Threading.Tasks;
using static Inspire.Erp.Domain.Modals.PDCResponse;
using Microsoft.EntityFrameworkCore;
using Inspire.Erp.Domain.Enums;
using System.Security.Cryptography.Xml;

namespace Inspire.Erp.Application.Account.Implementation
{
    public class PDCService:IPDC
    {
        private readonly IRepository<PDCGetList> _PDCGetListResponse;
        private readonly IRepository<PDCPostedList> _PDCPostedListRepo;
        private readonly IRepository<MasterAccountsTable> _MasterAccountsTableRepo;
        private readonly IRepository<ProgramSettings> _programsettingsRepository;
        private readonly IRepository<PdcDetails> _pdcDetails;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private IRepository<PdcVoucher> _pdcVoucherRepository;
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<PdcVoucherDetails> _pdcVoucherDetailsRepository;


        private static string prefix;
        public PDCService(
            IRepository<MasterAccountsTable> MasterAccountsTableRepo
            , IRepository<PDCGetList> PDCGetListResponse,
            IRepository<PDCPostedList> PDCPostedListRepo,
            IRepository<PdcDetails> pdcDetails,
            IRepository<ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumbersRepository,
            IRepository<PdcVoucher> pdcVoucherRepository,
            IRepository<AccountsTransactions> accountTransactionRepository,
            IRepository<PdcVoucherDetails> pdcVoucherDetailsRepository)
        {
            _MasterAccountsTableRepo = MasterAccountsTableRepo;
            _programsettingsRepository = programsettingsRepository;
            _PDCPostedListRepo = PDCPostedListRepo;
            _PDCGetListResponse = PDCGetListResponse;
            _pdcDetails = pdcDetails;
            _voucherNumbersRepository = voucherNumbersRepository;
            _pdcVoucherRepository = pdcVoucherRepository;
            _accountTransactionRepository = accountTransactionRepository;
            _pdcVoucherDetailsRepository = pdcVoucherDetailsRepository;
        }

        public async Task<IEnumerable<PDCPostedList>> PDCPostedReturnList(string chequeStatus)
        {
            try
            {
                //C  or // R
                 
             //var result = await (from pdc in _pdcDetails.GetAsQueryable()
             //       join acc in _MasterAccountsTableRepo.GetAsQueryable()
             //       on pdc.PDC_Details_Bank_Account_No equals acc.MaAccNo
             //       where pdc.PDC_Details_Cheque_Status == chequeStatus
             //       select new PDCPostedList
             //       {
             //           BAccNo = acc.MaAccName, // Assuming AccountName is the field in Accounts table
             //           CAmount = pdc.PDC_Details_Cheque_Amount,
             //           CBName = pdc.PDC_Details_Cheque_Bank_Name,
             //           CNO = pdc.PDC_Details_Cheque_No,
             //           PartyName = pdc.PDC_Details_PartyAccNo,
             //           PDate = pdc.PDC_Details_PDC_Date,
             //           PID = pdc.PDC_Details_ID,
             //           POSTDt = pdc.PDC_Details_PDC_Voucher_Date,
             //           TDate = pdc.PDC_Details_Trans_Date,
             //           VNO = pdc.PDC_Details_V_No,
             //           VType = pdc.PDC_Details_V_Type,
             //           PDCVNO = pdc.PDC_Details_V_No,
             //           Tran_Type = pdc.PDC_Details_Trans_Type,
             //           ChequeStatus = pdc.PDC_Details_Cheque_Status,
             //       }).ToListAsync();

                var result = await (from pdc in _pdcDetails.GetAsQueryable()
                                    join acc in _MasterAccountsTableRepo.GetAsQueryable()
                                    on pdc.PDC_Details_Bank_Account_No equals acc.MaAccNo into accGroup
                                    from acc in accGroup.DefaultIfEmpty()
                                    where pdc.PDC_Details_Cheque_Status == chequeStatus
                                    select new PDCPostedList
                                    {
                                        BAccNo = acc != null ? acc.MaAccName : null, // Handling null case
                                        CAmount = pdc.PDC_Details_Cheque_Amount,
                                        CBName = pdc.PDC_Details_Cheque_Bank_Name,
                                        CNO = pdc.PDC_Details_Cheque_No,
                                        PartyName = pdc.PDC_Details_PartyAccNo,
                                        PDate = pdc.PDC_Details_PDC_Date,
                                        PID = pdc.PDC_Details_ID,
                                        POSTDt = pdc.PDC_Details_PDC_Voucher_Date,
                                        TDate = pdc.PDC_Details_Trans_Date,
                                        VNO = pdc.PDC_Details_V_No,
                                        VType = pdc.PDC_Details_V_Type,
                                        PDCVNO = pdc.PDC_Details_V_No,
                                        Tran_Type = pdc.PDC_Details_Trans_Type,
                                        ChequeStatus = pdc.PDC_Details_Cheque_Status,
                                    }).ToListAsync();


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<PDCGetList>> PDCGetList()
        {
            try
            {
                //NC
                var result = await _pdcDetails.GetAsQueryable().Where(x => x.PDC_Details_Cheque_Status == "NC").Select(x => new PDCGetList
                {
                    BAccNo = x.PDC_Details_Bank_Account_No,
                    CAmount = x.PDC_Details_Cheque_Amount,
                    CBName = x.PDC_Details_Cheque_Bank_Name,
                    CNO = x.PDC_Details_Cheque_No,
                    PartyName = x.PDC_Details_PartyAccNo,
                    PDate = x.PDC_Details_PDC_Date,
                    PID = x.PDC_Details_ID,
                    AccName=!string.IsNullOrEmpty(x.PDC_Details_PartyAccNo)? _MasterAccountsTableRepo.GetAsQueryable().FirstOrDefault(c => c.MaAccNo == x.PDC_Details_PartyAccNo).MaAccName :"",
                    TDate = x.PDC_Details_Trans_Date,
                    VNO = x.PDC_Details_V_No,
                    VType = x.PDC_Details_V_Type,
                    Narration = x.PDC_Details_PDC_Voucher_Narration,
                    Tran_Type = x.PDC_Details_Trans_Type,
                }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<PDCGetList>> PDCGetReturnList(PDCFilters obj)
        {
            try
            {
                //NC

                string s = "EXEC getFilteredPDCList @postDate='" + obj.postDate + "',@dateFrom='" + obj.dateFrom + "'" +
     ",@dateTo='" + obj.dateTo + "',@pdcType='" + obj.pdcType + "',@keyWords='" +
     "" + obj.keyWords[0] + "',@searchType='" + obj.searchType + "'";
                var result = await _pdcDetails.GetAsQueryable()
                   .Where(x => (obj.postDate != null ? x.PDC_Details_Trans_Date == obj.postDate : true) && (x.PDC_Details_PDC_Date >= obj.dateFrom && x.PDC_Details_PDC_Date <= obj.dateFrom) &&
                   (obj.keyWords != null && obj.keyWords.Count > 0 && !string.IsNullOrEmpty(obj.keyWords[0]) ? x.PDC_Details_Bank_Account_No.Contains(obj.keyWords[0]) : true) &&
                   (!string.IsNullOrEmpty(obj.pdcType) ? x.PDC_Details_Trans_Type == obj.pdcType : true))
                   .Select(x => new PDCGetList
                   {
                       BAccNo = x.PDC_Details_Bank_Account_No,
                       CAmount = x.PDC_Details_Cheque_Amount,
                       CBName = x.PDC_Details_Cheque_Bank_Name,
                       CNO = x.PDC_Details_Cheque_No,
                       PartyName = x.PDC_Details_PartyAccNo,
                       PDate = x.PDC_Details_PDC_Date,
                       PID = x.PDC_Details_ID,
                       AccName = !string.IsNullOrEmpty(x.PDC_Details_PartyAccNo) ? _MasterAccountsTableRepo.GetAsQueryable().FirstOrDefault(c => c.MaAccNo == x.PDC_Details_PartyAccNo).MaAccName : "",
                       TDate = x.PDC_Details_Trans_Date,
                       VNO = x.PDC_Details_V_No,
                       VType = x.PDC_Details_V_Type,
                       Narration = x.PDC_Details_PDC_Voucher_Narration
                   }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<PDCGetList>> getFilteredPDCList(PDCFilters obj)
        {
            try
            {
                string s = "EXEC getFilteredPDCList @postDate='" + obj.postDate+ "',@dateFrom='" + obj.dateFrom + "'" +
     ",@dateTo='" + obj.dateTo + "',@pdcType='" + obj.pdcType + "',@keyWords='" +
     "" + obj.keyWords[0] + "',@searchType='" + obj.searchType + "'";
               
                 var result = await _pdcDetails.GetAsQueryable()
                    .Where(x => 
                                  (x.PDC_Details_PDC_Date>=obj.dateFrom  && x.PDC_Details_PDC_Date <= obj.dateTo) &&
                    //(obj.keyWords != null && obj.keyWords.Count >0 && !string.IsNullOrEmpty(obj.keyWords[0]) ? x.PDC_Details_Bank_Account_No.Contains(obj.keyWords[0]):true) && 
                    (!string.IsNullOrEmpty(obj.pdcType) ? x.PDC_Details_Trans_Type== obj.pdcType :true) && x.PDC_Details_Cheque_Status == "NC")
                    .Select(x => new PDCGetList
                {
                    BAccNo = x.PDC_Details_Bank_Account_No,
                    CAmount = x.PDC_Details_Cheque_Amount,
                    CBName = x.PDC_Details_Cheque_Bank_Name,
                    CNO = x.PDC_Details_Cheque_No,
                    PartyName = x.PDC_Details_PartyAccNo,
                    PDate = x.PDC_Details_PDC_Date,
                    PID = x.PDC_Details_ID,
                    AccName = !string.IsNullOrEmpty(x.PDC_Details_PartyAccNo) ? _MasterAccountsTableRepo.GetAsQueryable().FirstOrDefault(c => c.MaAccNo == x.PDC_Details_PartyAccNo).MaAccName : "",
                    TDate = x.PDC_Details_Trans_Date,
                    VNO = x.PDC_Details_V_No,
                    VType = x.PDC_Details_V_Type,
                    Narration = x.PDC_Details_PDC_Voucher_Narration,
                    Tran_Type = x.PDC_Details_Trans_Type,
                    }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<BankNamesModel>> GetBankNames()
        {
            try
            {
                List<BankNamesModel> l = new List<BankNamesModel>();
                 l = (from an in _MasterAccountsTableRepo.GetAsQueryable()
                            where (an.MaStatus ?? "R") == "R" && an.MaRelativeNo == "01001002"
                      select new BankNamesModel
                            {
                                AccName = an.MaAccName
                            }).ToList();
                return l;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<PDCGetList>> PostPDC(List<PDCGetList> list)
        {
            try
            {
                _pdcVoucherRepository.BeginTransaction();
                List<PdcVoucher> pdcVouchers = new List<PdcVoucher>();
                List<AccountsTransactions> accountsTransactions = new List<AccountsTransactions>();
                List<PdcDetails> pdcDetails = new List<PdcDetails>();
                List<PdcVoucherDetails> pdcVoucherDetails = new List<PdcVoucherDetails>();
                foreach (var item in list)
                {
                    var pd = _pdcDetails.GetAsQueryable().Where(x => x.PDC_Details_ID == item.PID).FirstOrDefault();                  
                    string bankAccNo = _MasterAccountsTableRepo.GetAsQueryable().FirstOrDefault(c => c.MaAccName == item.postingBank)?.MaAccNo;
                   
                    pd.PDC_Details_Bank_Account_No = bankAccNo;
                    pd.PDC_Details_PDC_Voucher_Date = DateTime.Now;
                    if (item.Type == "Post")
                    {

                         pd.PDC_Details_Cheque_Status = "C";
                        _pdcDetails.Update(pd);
                       
                        string pdcVoucherNumber = this.GenerateVoucherNo(item.PDate).VouchersNumbersVNo;

                        //pdcVouchers.Add(new PdcVoucher()
                        //{
                        //    PdcVoucherVid = pdcVoucherNumber,
                        //    PdcVoucherRef = item.VNO,
                        //    PdcVoucherPdcDate = item.PDate,
                        //    PdcVoucherPdcAmount = item.CAmount,
                        //    PdcVoucherFcPdcAmount = item.CAmount,
                        //    PdcVoucherDelStatus = false,
                        //    PdcVoucherNarration = item.Narration,

                        //});



                        if (item.Tran_Type == "ISSUED")
                        {
                            //Credit
                            accountsTransactions.Add(new AccountsTransactions()
                            {
                                AccountsTransactionsAccNo = item.BAccNo,
                                AccountsTransactionsCheqNo = item.CNO,
                                AccountsTransactionsDebit = (decimal)item.CAmount,
                                AccountsTransactionsFcDebit = (decimal)item.CAmount,
                                AccountsTransactionsVoucherNo = pdcVoucherNumber,
                                AccountsTransactionsVoucherType = VoucherType.PDC_TYPE,
                                AccountsTransactionsStatus = AccountStatus.Approved,
                                AccountsTransactionsTransDate = DateTime.Now,
                                AccountsTransactionsParticulars = item.Narration,
                                AccountsTransactionsTstamp = DateTime.Now,
                                RefNo = item.VNO,
                                AccountsTransactionsUserId = 1,

                            });

                            // Debit

                            accountsTransactions.Add(new AccountsTransactions()
                            {

                                AccountsTransactionsAccNo = bankAccNo,
                                AccountsTransactionsCheqNo = item.CNO,
                                AccountsTransactionsCredit = (decimal)item.CAmount,
                                AccountsTransactionsFcCredit = (decimal)item.CAmount,
                                AccountsTransactionsVoucherNo = pdcVoucherNumber,
                                AccountsTransactionsVoucherType = VoucherType.PDC_TYPE,
                                AccountsTransactionsStatus = AccountStatus.Approved,
                                AccountsTransactionsTransDate = DateTime.Now,
                                AccountsTransactionsParticulars = item.Narration,
                                AccountsTransactionsTstamp = DateTime.Now,
                                RefNo = item.VNO,
                                AccountsTransactionsUserId = 1,
                            });

                        }
                        else
                        {
                            //Credit
                            accountsTransactions.Add(new AccountsTransactions()
                            {
                                AccountsTransactionsAccNo = item.BAccNo,
                                AccountsTransactionsCheqNo = item.CNO,
                                AccountsTransactionsCredit = (decimal)item.CAmount,
                                AccountsTransactionsFcCredit = (decimal)item.CAmount,
                                AccountsTransactionsVoucherNo = pdcVoucherNumber,
                                AccountsTransactionsVoucherType = VoucherType.PDC_TYPE,
                                AccountsTransactionsStatus = AccountStatus.Approved,
                                AccountsTransactionsTransDate = DateTime.Now,
                                AccountsTransactionsParticulars = item.Narration,
                                AccountsTransactionsTstamp = DateTime.Now,
                                RefNo = item.VNO,
                                AccountsTransactionsUserId = 1,

                            });

                            // Debit

                            accountsTransactions.Add(new AccountsTransactions()
                            {

                                AccountsTransactionsAccNo = bankAccNo,
                                AccountsTransactionsCheqNo = item.CNO,
                                AccountsTransactionsDebit = (decimal)item.CAmount,
                                AccountsTransactionsFcDebit = (decimal)item.CAmount,
                                AccountsTransactionsVoucherNo = pdcVoucherNumber,
                                AccountsTransactionsVoucherType = VoucherType.PDC_TYPE,
                                AccountsTransactionsStatus = AccountStatus.Approved,
                                AccountsTransactionsTransDate = DateTime.Now,
                                AccountsTransactionsParticulars = item.Narration,
                                AccountsTransactionsTstamp = DateTime.Now,
                                RefNo= item.VNO,
                                AccountsTransactionsUserId = 1,
                            });

                        }



                        
                        //pdcVoucherDetails.Add(new PdcVoucherDetails()
                        //{
                        //    PdcVoucherDetailsAccno = item.BAccNo,
                        //    PdcVoucherDetailsAmount = item.CAmount,
                        //    PdcVoucherDetailsFcAmount = item.CAmount,
                        //    PdcVoucherDetailsNarration = item.Narration,
                        //    PdcVoucherDetailsDelStatus = false,
                        //});
                    }
                    else
                    {
                        
                         pd.PDC_Details_Cheque_Status = "R";
                        _pdcDetails.Update(pd);
                        

                    }
                }                 
                if (accountsTransactions.Count > 0)
                {
                    _accountTransactionRepository.InsertList(accountsTransactions);
                }
                if (pdcVouchers.Count > 0)
                {
                    _pdcVoucherRepository.InsertList(pdcVouchers);
                }
                if (pdcVoucherDetails.Count > 0)
                {
                    _pdcVoucherDetailsRepository.InsertList(pdcVoucherDetails);
                }
               
                _pdcVoucherRepository.TransactionCommit();
                return list;
            }
            catch (Exception ex)
            {
                _pdcVoucherRepository.TransactionRollback();
                throw;
            }
                       
        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.PDC_Voucher_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.PDC_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.PDC_TYPE,
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

        public async Task<IEnumerable<PDCPostedList>> PDCReturnLists()
        {
            try
            {
                //R
                var result = await (from pdc in _pdcDetails.GetAsQueryable()
                                    join acc in _MasterAccountsTableRepo.GetAsQueryable()
                                    on pdc.PDC_Details_Bank_Account_No equals acc.MaAccNo
                                    where pdc.PDC_Details_Cheque_Status == "R"
                                    select new PDCPostedList
                                    {
                                        BAccNo = acc.MaAccName, // Assuming AccountName is the field in Accounts table
                                        CAmount = pdc.PDC_Details_Cheque_Amount,
                                        CBName = pdc.PDC_Details_Cheque_Bank_Name,
                                        CNO = pdc.PDC_Details_Cheque_No,
                                        PartyName = pdc.PDC_Details_PartyAccNo,
                                        PDate = pdc.PDC_Details_PDC_Date,
                                        PID = pdc.PDC_Details_ID,
                                        POSTDt = pdc.PDC_Details_PDC_Voucher_Date,
                                        TDate = pdc.PDC_Details_Trans_Date,
                                        VNO = pdc.PDC_Details_V_No,
                                        VType = pdc.PDC_Details_V_Type,
                                        PDCVNO = pdc.PDC_Details_V_No,
                                        Tran_Type = pdc.PDC_Details_Trans_Type,
                                    }).ToListAsync();

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
