using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class VoucherAllocationService : IVoucherAllocationService
    {
        private readonly IRepository<CustomerMaster> _customerMaster;
        private readonly IRepository<SuppliersMaster> _supplierMaster;
        private readonly IRepository<AccountsTransactions> _accountTransaction;
        private readonly IRepository<MasterAccountsTable> _masterAccountTable;
        private readonly IRepository<AllocationVoucher> _voucherAllocation;
        private readonly IRepository<AllocationVoucherDetails> _voucherAllocationDetails;
        private readonly IUtilityService _utilityService;
        private readonly IRepository<CurrencyMaster> _currencyMaster;
        private IRepository<BankReceiptVoucher> _bankReceiptVoucherRepository;
        private IRepository<ReceiptVoucherMaster> _receiptVoucherMasterRepository;
        private IRepository<PaymentVoucher> _paymentVoucherRepository;
        private IRepository<BankPaymentVoucher> _bankPaymentVoucherRepository;



        public VoucherAllocationService(IRepository<CustomerMaster> customerMaster, IRepository<AccountsTransactions> accountTransaction,
            IRepository<SuppliersMaster> supplierMaster, IRepository<AllocationVoucherDetails> voucherAllocationDetails,
            IRepository<MasterAccountsTable> masterAccountTable, IRepository<AllocationVoucher> voucherAllocation, IUtilityService utilityService, IRepository<CurrencyMaster> currencyMaster,
            IRepository<BankReceiptVoucher> bankReceiptVoucherRepository, IRepository<ReceiptVoucherMaster> receiptVoucherMasterRepository,
            IRepository<PaymentVoucher> paymentVoucherRepository, IRepository<BankPaymentVoucher> bankPaymentVoucherRepository)
        {
            _customerMaster = customerMaster;
            _supplierMaster = supplierMaster;
            _accountTransaction = accountTransaction;
            _masterAccountTable = masterAccountTable;
            _voucherAllocation = voucherAllocation;
            _voucherAllocationDetails = voucherAllocationDetails;
            _utilityService = utilityService;
            _currencyMaster = currencyMaster;
            _bankPaymentVoucherRepository = bankPaymentVoucherRepository;
            _bankReceiptVoucherRepository = bankReceiptVoucherRepository;
            _paymentVoucherRepository = paymentVoucherRepository;
            _receiptVoucherMasterRepository = receiptVoucherMasterRepository;
        }
        public async Task<Response<List<DropdownResponse>>> GetCustomerMasterDropdown()
        {
            try
            {
                var response = new List<DropdownResponse>();
                //response.Add(new DropdownResponse()
                //{
                //    Value = "",
                //    Name = " All "
                //});
                response.AddRange(await _customerMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Value = x.CustomerMasterCustomerReffAcNo,
                    Name = x.CustomerMasterCustomerName
                }));
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<DropdownResponse>>> GetSupplierMasterDropdown()
        {
            try
            {
                var response = new List<DropdownResponse>();
                //response.Add(new DropdownResponse()
                //{
                //    Value = "",
                //    Name = " All "
                //});
                response.AddRange(await _supplierMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Value = x.SuppliersMasterSupplierReffAcNo,
                    Name = x.SuppliersMasterSupplierName
                }));
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }

        public async Task<Response<GridWrapperResponse<List<GetAccountTransactionMutilAccountResponse>>>> GetAccountTransactions(GenericGridViewModel model)
        {
            try
            {
                string query = @$" AccountstransactionsDelStatus != true  {model.Filter}";


                //var a = await _accountTransaction.GetAsQueryable().Where(x =>( x.AccountsTransactionsTransDate >= Convert.ToDateTime("")) &&  )
                //    .ToListAsync();va
                var result = await _accountTransaction.GetAsQueryable().Where(query).Select(x => new GetAccountTransactionMutilAccountResponse
                {
                    AccountsTransactionsAccNo = x.AccountsTransactionsAccNo,
                    //AccountsTransactionsAccName = _masterAccountTable.GetAsQueryable().FirstOrDefault(c => c.MasterAccountsTableAccNo == x.AccountsTransactionsAccNo).MasterAccountsTableAccName,
                    AccountsTransactionsDebit = Convert.ToDecimal(x.AccountsTransactionsDebit),
                    AccountsTransactionsCredit = Convert.ToDecimal(x.AccountsTransactionsCredit),
                    AccountsTransactionsAllocBalance = x.AccountsTransactionsAllocBalance,
                    AccountsTransactionsVoucherNo = x.AccountsTransactionsVoucherNo,
                    AccountsTransactionsVoucherType = x.AccountsTransactionsVoucherType,
                    AccountsTransactionsDescription = x.AccountsTransactionsDescription,
                    AccountsTransactionsTransDate = x.AccountsTransactionsTransDate != null ? Convert.ToDateTime(x.AccountsTransactionsTransDate) : DateTime.MinValue,
                    AccountsTransactionsTransDateString = Convert.ToDateTime(x.AccountsTransactionsTransDate).ToString("MM-dd-yyyy"),
                    AccountsTransactionsParticulars = x.AccountsTransactionsParticulars,
                    AccountsTransactionsFsno = x.AccountsTransactionsFsno,
                    RefNo = x.RefNo,
                    AccountsTransactionsTransSno = Convert.ToInt32(x.AccountsTransactionsTransSno),
                    AccountsTransactionsAllocDebit = x.AccountsTransactionsAllocDebit > 0 ? x.AccountsTransactionsAllocDebit : x.AccountsTransactionsAllocCredit,
                    AccountsTransactionsAllocCredit = x.AccountsTransactionsAllocCredit,
                    AccountsTransactionsAllocUpdateBal = x.AccountsTransactionsAllocDebit > 0 ? (double)x.AccountsTransactionsAllocDebit : (double)x.AccountsTransactionsAllocCredit,
                    AccountsTransactionsAllocationbalance = x.AccountsTransactionsAllocBalance //AccountsTransactionsAllocDebit > 0 ? x.AccountsTransactionsAllocDebit : x.AccountsTransactionsAllocCredit

                }).ToListAsync();

                var gridReponse = new GridWrapperResponse<List<GetAccountTransactionMutilAccountResponse>>();
                gridReponse.Data = result;
                var total = 0;
                gridReponse.Total = Convert.ToInt32(total);
                return Response<GridWrapperResponse<List<GetAccountTransactionMutilAccountResponse>>>.Success(gridReponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<GetAccountTransactionMutilAccountResponse>>>.Fail(new GridWrapperResponse<List<GetAccountTransactionMutilAccountResponse>>(), ex.Message);
            }
        }
        public async Task<Response<bool>> AddEditVoucherAllocation(AllocationVoucher model)
        {

            using (var trans = new TransactionScope())
            {
                try
                {
                    if (model.AllocationVoucherId == null || model.AllocationVoucherId == 0)
                    {
                        var avID = getMaxVoucherAllocation();
                        var alloModel = new AllocationVoucher()
                        {
                            AllocationVoucherId = avID,
                            AllocationVoucherType = 1,
                            AllocationVoucherVoucherNo = $"AV{avID}",
                            AllocationVoucherVoucherDate = model.AllocationVoucherVoucherDate,
                            AllocationVoucherDescription = model.AllocationVoucherDescription,
                            AllocationVoucherVoucherAccNo = model.AllocationVoucherVoucherAccNo,
                            AllocationVoucherVoucherAccType = "ALLOCATION",
                            AllocationVoucherVoucherFsno = model.AllocationVoucherVoucherFsno,
                            AllocationVoucherStatus = "A",
                            AllocationVoucherUserId = model.AllocationVoucherUserId,
                            AllocationVoucherAvStatus = "B",
                            AllocationVoucherRefVno = model.AllocationVoucherRefVno,
                            AllocationVoucherRefVtype = 1,//"RECEIPT",
                            AllocationVoucherLocationId = 1,
                            AllocationVoucherVcreation = "AccForm",
                            AllocationVoucherDelStatus = false,


                        };

                        _voucherAllocation.Insert(alloModel);

                        foreach (var item in model.VoucherAllocationDetails)
                        {
                            UpdateVoucherAllocationId(item.AllocationVoucherDetailsRefVtype, item.AllocationVoucherDetailsVno, avID);
                        }


                        #region ADD ACTIVITY LOGS
                        AddActivityLogViewModel log = new AddActivityLogViewModel()
                        {
                            Page = "Voucher Allocation",
                            Section = "Add Voucher Allocation ",
                            Entity = "Voucher Allocation ",

                        };
                        //await _utilityService.AddUserTrackingLog(log);
                        #endregion
                        //_voucherAllocation.SaveChangesAsync();

                        if (model.VoucherAllocationDetails != null && model.VoucherAllocationDetails.Count() > 0)
                        {
                            var vouchetAlloc = _voucherAllocation.GetAll().LastOrDefault();
                            Console.WriteLine("************************************************** = " + vouchetAlloc.AllocationVoucherId);
                            List<AllocationVoucherDetails> orderDetails = new List<AllocationVoucherDetails>();

                            foreach (var x in model.VoucherAllocationDetails)
                            {
                                orderDetails.Add(
                                new AllocationVoucherDetails
                                {
                                    AllocationVoucherDetailsVno = x.AllocationVoucherDetailsVno,
                                    AllocationVoucherDetailsAllocDebit = x.AllocationVoucherDetailsAllocDebit > 0 ? x.AllocationVoucherDetailsNetAllocation : 0.0,
                                    AllocationVoucherDetailsAllocCredit = x.AllocationVoucherDetailsAllocCredit > 0 ? x.AllocationVoucherDetailsNetAllocation : 0.0,
                                    AllocationVoucherDetailsVtype = 1,
                                    AllocationVoucherDetailsDelStatus = false,
                                    AllocationVoucherDetailsSno = Convert.ToInt32(vouchetAlloc.AllocationVoucherId),
                                    AllocationVoucherDetailsVFsno = model.AllocationVoucherVoucherFsno,
                                    AllocationVoucherDetailsVLocationId = model.AllocationVoucherLocationId,
                                    AllocationVoucherDetailsTransSno = (int)x.AccountsTransactionsTransSno,
                                    AllocationVoucherDetailsAccNo = x.AllocationVoucherDetailsAccNo,
                                    AllocationVoucherDetailsFcAllocDebit = x.AllocationVoucherDetailsAllocDebit > 0 ? x.AllocationVoucherDetailsNetAllocation * 1 : 0.0, //==>currency rate
                                    AllocationVoucherDetailsFcAllocCredit = x.AllocationVoucherDetailsAllocCredit > 0 ? x.AllocationVoucherDetailsNetAllocation * 1 : 0.0,
                                    AllocationVoucherDetailsRefVno = model.AllocationVoucherRefVno,
                                    AllocationVoucherDetailsRefVtype = x.AllocationVoucherDetailsRefVtype,
                                    AllocationVoucherDetailsRefLocationId = x.AllocationVoucherDetailsRefLocationId,
                                    AllocationVoucherDetailsRefFsno = x.AllocationVoucherDetailsRefFsno,
                                    RefSPAccountNo = x.AllocationVoucherDetailsAccNo,
                                    RefSPDate = x.RefSPDate
                                    //AllocationVoucherDetailsId = getMaxVoucherAllocationDetails()
                                });
                            }


                            _voucherAllocationDetails.InsertList(orderDetails);
                            _voucherAllocationDetails.SaveChangesAsync();

                            //here need to update the accounts transaction table for allocation
                            List<AccountsTransactions> accountsTransactions = new List<AccountsTransactions>();
                            foreach (var item in model.VoucherAllocationDetails)
                            {

                                AccountsTransactions acc = _accountTransaction.GetAsQueryable().Where(x => x.AccountsTransactionsTransSno == item.AccountsTransactionsTransSno).FirstOrDefault();
                                var currency = _currencyMaster.GetAsQueryable().Where(c => c.CurrencyMasterCurrencyId == acc.AccountsTransactionsCurrencyId).Distinct().FirstOrDefault();
                                decimal cRate = currency == null ? 1 : currency.CurrencyMasterCurrencyRate == null ? 1 : (decimal)currency.CurrencyMasterCurrencyRate;
                                if (acc != null)
                                {
                                    if (acc.AccountsTransactionsDebit > 0)
                                    {

                                        decimal debit = acc.AccountsTransactionsDebit == null ? 0 : (decimal)acc.AccountsTransactionsDebit;
                                        decimal alloDebit = acc.AccountsTransactionsAllocDebit == null ? 0 : (decimal)acc.AccountsTransactionsAllocDebit;
                                        decimal fc_alloDebit = acc.AccountsTransactionsFcAllocDebit == null ? 0 : (decimal)acc.AccountsTransactionsFcAllocDebit;
                                        decimal fcdebit = acc.AccountsTransactionsFcDebit == null ? 0 : (decimal)acc.AccountsTransactionsFcDebit;

                                        acc.AccountsTransactionsAllocBalance = debit - (alloDebit + (decimal)item.AllocationVoucherDetailsNetAllocation);
                                        acc.AccountsTransactionsAllocDebit = alloDebit + (decimal)item.AllocationVoucherDetailsNetAllocation;
                                        acc.AccountsTransactionsFcAllocDebit = fc_alloDebit + (decimal)item.AllocationVoucherDetailsNetAllocation * cRate;
                                        acc.AccountsTransactionsFcAllocBalance = fcdebit - (fc_alloDebit + ((decimal)item.AllocationVoucherDetailsNetAllocation) * cRate);

                                    }
                                    else if (acc.AccountsTransactionsCredit > 0)
                                    {
                                        decimal credit = acc.AccountsTransactionsCredit == null ? 0 : (decimal)acc.AccountsTransactionsCredit;
                                        decimal alloCredit = acc.AccountsTransactionsAllocCredit == null ? 0 : (decimal)acc.AccountsTransactionsAllocCredit;
                                        decimal fc_alloCredit = acc.AccountsTransactionsFcAllocCredit == null ? 0 : (decimal)acc.AccountsTransactionsFcAllocCredit;
                                        decimal fcCredit = acc.AccountsTransactionsFcCredit == null ? 0 : (decimal)acc.AccountsTransactionsFcCredit;

                                        acc.AccountsTransactionsAllocBalance = credit - (alloCredit + (decimal)item.AllocationVoucherDetailsNetAllocation);
                                        acc.AccountsTransactionsAllocCredit = alloCredit + (decimal)item.AllocationVoucherDetailsNetAllocation;
                                        acc.AccountsTransactionsFcAllocCredit = fc_alloCredit + (decimal)item.AllocationVoucherDetailsNetAllocation * cRate;
                                        acc.AccountsTransactionsFcAllocBalance = fcCredit - (fc_alloCredit + ((decimal)item.AllocationVoucherDetailsNetAllocation) * cRate);
                                    }
                                    _accountTransaction.Update(acc);

                                }
                            }

                        }

                    }
                    else
                    {

                        var orders = _voucherAllocation.GetAsQueryable().FirstOrDefault(x => x.AllocationVoucherId == model.AllocationVoucherId);
                        orders.AllocationVoucherType = model.AllocationVoucherType;
                        orders.AllocationVoucherDelStatus = false;
                        orders.AllocationVoucherVcreation = model.AllocationVoucherVcreation;
                        orders.AllocationVoucherVoucherNo = model.AllocationVoucherVoucherNo;
                        orders.AllocationVoucherVoucherAccType = model.AllocationVoucherVoucherAccType;
                        orders.AllocationVoucherVoucherAccNo = model.AllocationVoucherVoucherAccNo;
                        orders.AllocationVoucherVoucherDate = model.AllocationVoucherVoucherDate;
                        _voucherAllocation.Update(orders);
                        #region ADD ACTIVITY LOGS
                        AddActivityLogViewModel log = new AddActivityLogViewModel()
                        {
                            Page = "Voucher Allocation",
                            Section = "Update Voucher Allocation ",
                            Entity = "Voucher Allocation ",

                        };
                        await _utilityService.AddUserTrackingLog(log);
                        #endregion
                        _voucherAllocation.SaveChangesAsync();

                        var listOrdersDetails = _voucherAllocationDetails.GetAsQueryable().Where(x => x.AllocationVoucherDetailsSno == model.AllocationVoucherId).ToList();
                        _voucherAllocationDetails.DeleteList(listOrdersDetails);
                        #region ADD ACTIVITY LOGS
                        AddActivityLogViewModel Deletelog = new AddActivityLogViewModel()
                        {
                            Page = "Voucher Allocation",
                            Section = "Delete Voucher Allocation ",
                            Entity = "Voucher Allocation ",

                        };
                        //await _utilityService.AddUserTrackingLog(Deletelog);
                        #endregion
                        _voucherAllocationDetails.SaveChangesAsync();

                        List<AllocationVoucherDetails> orderDetails = new List<AllocationVoucherDetails>();
                        orderDetails.AddRange(model.VoucherAllocationDetails.Select(x => new AllocationVoucherDetails
                        {
                            AllocationVoucherDetailsVno = x.AllocationVoucherDetailsVno,
                            AllocationVoucherDetailsAllocDebit = x.AllocationVoucherDetailsAllocDebit,
                            AllocationVoucherDetailsAllocCredit = x.AllocationVoucherDetailsAllocCredit,
                            AllocationVoucherDetailsId = x.AllocationVoucherDetailsId,
                            AllocationVoucherDetailsVtype = x.AllocationVoucherDetailsVtype,
                            AllocationVoucherDetailsDelStatus = false,
                            AllocationVoucherDetailsSno = Convert.ToInt32(model.AllocationVoucherId),
                            AllocationVoucherDetailsAccNo = x.AllocationVoucherDetailsAccNo,
                            RefSPAccountNo = x.AllocationVoucherDetailsAccNo,
                            RefSPDate = x.RefSPDate
                        }));
                        _voucherAllocationDetails.InsertList(orderDetails);
                        _voucherAllocationDetails.SaveChangesAsync();
                    }
                    trans.Complete();
                    return Response<bool>.Success(true, "Changes Saved Successfully.");

                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    return Response<bool>.Fail(false, ex.Message);
                }
            }
        }


        private int getMaxVoucherAllocationDetails()
        {
            try
            {
                int? maxValue = _voucherAllocationDetails.GetAll().Max(x => x.AllocationVoucherDetailsId);
                int? incrementedValue = maxValue.HasValue ? maxValue + 1 : 1;
                return (int)incrementedValue;
            }
            catch
            {
                return 1;
            }
        }

        private int getMaxVoucherAllocation()
        {
            try
            {
                int? maxValue = _voucherAllocation.GetAll().Max(x => x.AllocationVoucherId);
                int? incrementedValue = maxValue.HasValue ? maxValue + 1 : 1;
                return (int)incrementedValue;
            }
            catch
            {
                return 1;
            }
        }

        private async Task<string> AddVoucherAllocation(AddEditVoucherAllocationResponse model)
        {
            try
            {
                // var maxid = _voucherNumber.GetAsQueryable();
                Random random = new Random();
                int voucherId = random.Next();
                int vouhcerDetailid = random.Next();
                //var a= maxid.Max(x => Math.Round(Convert.ToDecimal(x.VouchersNumbersVsno)))+1;
                AllocationVoucher order = new AllocationVoucher()
                {
                    AllocationVoucherId = voucherId,
                    AllocationVoucherType = model.AllocationVoucherType,
                    AllocationVoucherVoucherAccType = model.AllocationVoucherVoucherAccType,
                    AllocationVoucherDelStatus = false,
                    AllocationVoucherVoucherDate = model.AllocationVoucherVoucherDate,
                    AllocationVoucherVcreation = model.AllocationVoucherVcreation,
                    AllocationVoucherVoucherNo = model.AllocationVoucherVoucherNo,
                    AllocationVoucherVoucherAccNo = model.AllocationVoucherVoucherAccNo,

                };
                _voucherAllocation.Insert(order);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel logs = new AddActivityLogViewModel()
                {
                    Page = "Voucher Allocation",
                    Section = "Add Voucher Allocation",
                    Entity = "Voucher Allocation",

                };
                await _utilityService.AddUserTrackingLog(logs);
                #endregion
                _voucherAllocation.SaveChangesAsync();
                List<AllocationVoucherDetails> orderDetails = new List<AllocationVoucherDetails>();
                orderDetails.AddRange(model.VoucherAllocationDetails.Select(x => new AllocationVoucherDetails
                {
                    AllocationVoucherDetailsVno = x.AllocationVoucherDetailsVno,
                    AllocationVoucherDetailsAllocDebit = x.AllocationVoucherDetailsAllocDebit,
                    AllocationVoucherDetailsAllocCredit = x.AllocationVoucherDetailsAllocCredit,
                    AllocationVoucherDetailsId = vouhcerDetailid,
                    AllocationVoucherDetailsVtype = x.AllocationVoucherDetailsVtype,
                    AllocationVoucherDetailsDelStatus = false,
                    AllocationVoucherDetailsSno = voucherId,
                }));
                _voucherAllocationDetails.InsertList(orderDetails);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel log = new AddActivityLogViewModel()
                {
                    Page = "Voucher Allocation",
                    Section = "Add Voucher Allocation Detail",
                    Entity = "Voucher Allocation Detail",

                };
                await _utilityService.AddUserTrackingLog(logs);
                #endregion
                _voucherAllocationDetails.SaveChangesAsync();
                return "Record Added successfully.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<string> EditVoucherAllocation(AddEditVoucherAllocationResponse model)
        {
            try
            {
                Random random = new Random();
                int vouhcerDetailid = random.Next();
                var orders = _voucherAllocation.GetAsQueryable().FirstOrDefault(x => x.AllocationVoucherId == model.AllocationVoucherId);
                orders.AllocationVoucherType = model.AllocationVoucherType;
                orders.AllocationVoucherDelStatus = false;
                orders.AllocationVoucherVcreation = model.AllocationVoucherVcreation;
                orders.AllocationVoucherVoucherNo = model.AllocationVoucherVoucherNo;
                orders.AllocationVoucherVoucherAccType = model.AllocationVoucherVoucherAccType;
                orders.AllocationVoucherVoucherAccNo = model.AllocationVoucherVoucherAccNo;
                orders.AllocationVoucherVoucherDate = model.AllocationVoucherVoucherDate;
                _voucherAllocation.Update(orders);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel logs = new AddActivityLogViewModel()
                {
                    Page = "Voucher Allocation",
                    Section = "Update Voucher Allocation",
                    Entity = "Voucher Allocation ",

                };
                await _utilityService.AddUserTrackingLog(logs);
                #endregion
                _voucherAllocation.SaveChangesAsync();

                var listOrdersDetails = _voucherAllocationDetails.GetAsQueryable().Where(x => x.AllocationVoucherDetailsSno == model.AllocationVoucherId).ToList();
                _voucherAllocationDetails.DeleteList(listOrdersDetails);
                #region  
                AddActivityLogViewModel log = new AddActivityLogViewModel()
                {
                    Page = "Voucher Allocation",
                    Section = "Delete Voucher Allocation Detail",
                    Entity = "Voucher Allocation Detail",

                };
                await _utilityService.AddUserTrackingLog(log);
                #endregion
                _voucherAllocationDetails.SaveChangesAsync();
                List<AllocationVoucherDetails> orderDetails = new List<AllocationVoucherDetails>();
                orderDetails.AddRange(model.VoucherAllocationDetails.Select(x => new AllocationVoucherDetails
                {
                    AllocationVoucherDetailsVno = x.AllocationVoucherDetailsVno,
                    AllocationVoucherDetailsAllocDebit = x.AllocationVoucherDetailsAllocDebit,
                    AllocationVoucherDetailsAllocCredit = x.AllocationVoucherDetailsAllocCredit,
                    AllocationVoucherDetailsId = vouhcerDetailid,
                    AllocationVoucherDetailsVtype = x.AllocationVoucherDetailsVtype,
                    AllocationVoucherDetailsDelStatus = false,
                    AllocationVoucherDetailsSno = Convert.ToInt32(model.AllocationVoucherId),
                }));
                _voucherAllocationDetails.InsertList(orderDetails);
                #region  
                AddActivityLogViewModel loglogDetail = new AddActivityLogViewModel()
                {
                    Page = "Voucher Allocation",
                    Section = "Delete Voucher Allocation Detail",
                    Entity = "Voucher Allocation Detail",

                };
                await _utilityService.AddUserTrackingLog(log);
                #endregion
                _voucherAllocationDetails.SaveChangesAsync();

                return "Record Updated successfully.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<GridWrapperResponse<List<GetVoucherAllocationListResponse>>>> GetVoucherAllocationsList(GenericGridViewModel model)
        {
            try
            {
                List<GetVoucherAllocationListResponse> response = new List<GetVoucherAllocationListResponse>();
                var orders = await _voucherAllocation.GetAsQueryable().ToListAsync();
                response.AddRange(orders.Select(x => new GetVoucherAllocationListResponse
                {
                    AllocationVoucherId = x.AllocationVoucherId,
                    AllocationVoucherType = x.AllocationVoucherType,
                    AllocationVoucherDelStatus = x.AllocationVoucherDelStatus,
                    AllocationVoucherVcreation = x.AllocationVoucherVcreation,
                    AllocationVoucherVoucherNo = x.AllocationVoucherVoucherNo,
                    AllocationVoucherVoucherAccNo = x.AllocationVoucherVoucherAccNo,
                    AllocationVoucherVoucherDate = x.AllocationVoucherVoucherDate
                }).ToList());
                var gridResponse = new GridWrapperResponse<List<GetVoucherAllocationListResponse>>();
                gridResponse.Data = response;
                gridResponse.Total = 0;
                return Response<GridWrapperResponse<List<GetVoucherAllocationListResponse>>>.Success(gridResponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<GetVoucherAllocationListResponse>>>.Fail(new GridWrapperResponse<List<GetVoucherAllocationListResponse>>(), ex.Message);
            }
        }
        public async Task<Response<GetVoucherAllocationListResponse>> GetSpecificVoucherAllocation(string id)
        {
            try
            {
                GetVoucherAllocationListResponse model = new GetVoucherAllocationListResponse();
                await Task.Run(async () =>
                {
                    var alloId = ExtractInteger(id);
                    var orders = _voucherAllocation.GetAsQueryable().FirstOrDefault(x => x.AllocationVoucherId == alloId);
                    model.AllocationVoucherId = orders.AllocationVoucherId;
                    model.AllocationVoucherType = orders.AllocationVoucherType;
                    model.AllocationVoucherVoucherNo = orders.AllocationVoucherVoucherNo;
                    model.AllocationVoucherVoucherDate = orders.AllocationVoucherVoucherDate;
                    model.AllocationVoucherDescription = orders.AllocationVoucherDescription;
                    model.AllocationVoucherVoucherAccNo = orders.AllocationVoucherVoucherAccNo;
                    model.AllocationVoucherVoucherAccType = orders.AllocationVoucherVoucherAccType;
                    model.AllocationVoucherVoucherFsno = Convert.ToInt32(model.AllocationVoucherVoucherFsno);
                    model.AllocationVoucherStatus = model.AllocationVoucherStatus;
                    model.AllocationVoucherUserId = model.AllocationVoucherUserId;
                    model.AllocationVoucherAvStatus = model.AllocationVoucherAvStatus;
                    model.AllocationVoucherRefVno = model.AllocationVoucherRefVno;
                    model.AllocationVoucherRefVtype = model.AllocationVoucherRefVtype;
                    model.AllocationVoucherLocationId = model.AllocationVoucherLocationId;
                    model.AllocationVoucherVcreation = model.AllocationVoucherVcreation;
                    model.AllocationVoucherDelStatus = model.AllocationVoucherDelStatus;
                    model.VoucherAllocationDetails = _voucherAllocationDetails.GetAsQueryable()
                        .Where(x => x.AllocationVoucherDetailsSno == alloId).Select(x => new VoucherAllocationDetailsResponse
                        {
                            AllocationVoucherDetailsVno = x.AllocationVoucherDetailsVno,
                            AllocationVoucherDetailsAllocDebit = x.AllocationVoucherDetailsAllocDebit,
                            AllocationVoucherDetailsAllocCredit = x.AllocationVoucherDetailsAllocCredit,
                            AllocationVoucherDetailsId = x.AllocationVoucherDetailsId,
                            AllocationVoucherDetailsVtype = x.AllocationVoucherDetailsVtype,
                            AllocationVoucherDetailsSno = Convert.ToInt32(model.AllocationVoucherId),
                        }).ToList();

                    var accTrans = await GetAccountTransactions(new GenericGridViewModel()
                    {
                        Dir = "desc",
                        Field = "AccountsTransactions_TransSno",
                        Filter = $" && (AccountsTransactionsVoucherType != \"SalesReturn\") && (AccountsTransactionsAccNo.Contains(\"{orders.AllocationVoucherVoucherAccNo}\"))",
                        Take = 10000
                    });
                    model.AccountsTransactions = accTrans.Result.Data;




                });
                return Response<GetVoucherAllocationListResponse>.Success(model, "Records FOund.");
            }
            catch (Exception ex)
            {
                return Response<GetVoucherAllocationListResponse>.Fail(new GetVoucherAllocationListResponse(), ex.Message);
            }
        }

        public IQueryable GetVoucherAllocation()
        {
            try
            {
                var dataList = _voucherAllocation.GetAll().ToList().AsQueryable();
                return dataList;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        static int ExtractInteger(string input)
        {
            string pattern = @"\d+";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                int intValue;
                if (int.TryParse(match.Value, out intValue))
                {
                    return intValue;
                }
                else
                {
                    Console.WriteLine("Failed to parse integer value.");
                    return 0; // or throw an exception, return a default value, etc.
                }
            }
            else
            {
                Console.WriteLine("No integer value found in the input string.");
                return 0; // or throw an exception, return a default value, etc.
            }
        }
        public Response<AllocationVoucher> DeleteAllocationVoucher(string id, string type)
        {
            using (var trans = new TransactionScope())
            {
                try
                {
                    int Sno = 0;
                    switch (type)
                    {
                        case "Allocation":
                            Sno = _voucherAllocation.GetAsQueryable().Where(o => o.AllocationVoucherVoucherNo == id).FirstOrDefault().AllocationVoucherId;
                            break;

                        case "Other":
                            Sno = _voucherAllocationDetails.GetAsQueryable().Where(c => c.AllocationVoucherDetailsVno == id).FirstOrDefault().AllocationVoucherDetailsSno;
                            break;

                    }
                    int? getTransId = 0;
                    var allocationDetailsList = _voucherAllocationDetails.GetAsQueryable().Where(o => o.AllocationVoucherDetailsSno == Sno).ToList();
                    if (allocationDetailsList != null && allocationDetailsList.Count() > 0)
                    {
                        _voucherAllocationDetails.DeleteList(allocationDetailsList);
                    }
                    var allocation = _voucherAllocation.GetAsQueryable().Where(o => o.AllocationVoucherId == Sno).FirstOrDefault();
                    if (allocation != null)
                        _voucherAllocation.Delete(allocation);

                    
                    foreach (var item in allocationDetailsList)
                    {
                        UpdateVoucherAllocationId(item.AllocationVoucherDetailsRefVtype, id, 0);

                        //getTransId = item.AllocationVoucherDetailsTransSno;
                        var accTrans = _accountTransaction.GetAsQueryable().Where(x => x.AccountsTransactionsTransSno == item.AllocationVoucherDetailsTransSno).FirstOrDefault();

                        if (accTrans != null)
                        {
                            if (accTrans.AccountsTransactionsCredit > 0)
                            {
                                accTrans.AccountsTransactionsAllocBalance += accTrans.AccountsTransactionsAllocCredit;
      
                            }
                            else
                            {
                                accTrans.AccountsTransactionsAllocBalance += accTrans.AccountsTransactionsAllocDebit;
                            }

                            accTrans.AccountsTransactionsAllocCredit = 0;
                            accTrans.AccountsTransactionsAllocDebit = 0;
                            accTrans.AccountsTransactionsFcAllocCredit = 0;
                            accTrans.AccountsTransactionsFcAllocDebit = 0;



                            _accountTransaction.Update(accTrans);
                        }


                    }




                    /**UPDATE AccountsTransactions SET
                      * Alloc_Credit=0,Alloc_Debit=0,
                      * Fc_Alloc_Credit=0, Fc_Alloc_Debit=0 ,
                      * Alloc_Balance = CASE  
                      * WHEN Debit>0   THEN Debit   WHEN Credit>0  THEN Credit ELSE 0 End 
                      * WHERE AccNo='LB7574' AND transsno 
                      * NOT IN(SELECT Distinct transsno FROM Allocation_Voucher_Details  WHERE Acc_No='LB7574')**/


                    //var filteredTransactionIds = allocationDetailsList
                    //                                            .Where(detail => detail.AllocationVoucherDetailsAccNo == allocationDetails.AllocationVoucherVoucherAccNo)
                    //                                            .Select(detail => detail.AccountsTransactionsTransSno)
                    //                                            .Distinct();

                    //var updatedTransactions = accTrans
                    //    .Where(transaction => transaction.AccountsTransactionsAccNo == allocationDetails.AllocationVoucherVoucherAccNo && !filteredTransactionIds.Contains(transaction.AccountsTransactionsTransSno))
                    //    .Select(transaction =>
                    //    {
                    //        transaction.AccountsTransactionsAllocCredit = 0;
                    //        transaction.AccountsTransactionsAllocDebit = 0;
                    //        transaction.AccountsTransactionsFcAllocCredit = 0;
                    //        transaction.AccountsTransactionsFcAllocDebit = 0;
                    //        transaction.AccountsTransactionsAllocBalance = transaction.AccountsTransactionsDebit > 0 ? transaction.AccountsTransactionsDebit : (transaction.AccountsTransactionsCredit > 0 ? transaction.AccountsTransactionsCredit : 0);
                    //        transaction.AccountsTransactionsFcAllocBalance = transaction.AccountsTransactionsDebit > 0 ? transaction.AccountsTransactionsDebit : (transaction.AccountsTransactionsCredit > 0 ? transaction.AccountsTransactionsCredit : 0);
                    //        return transaction;
                    //    }).ToList();
                    trans.Complete();
                    return Response<AllocationVoucher>.Success(new AllocationVoucher(), "Data Deleted");
                }
                catch (System.Exception ex)
                {
                    trans.Dispose();
                    return Response<AllocationVoucher>.Fail(new AllocationVoucher(), "Data Not Deleted");
                }
            }

        }



        private void UpdateVoucherAllocationId(string voucherType,string voucherNo ,int AllocationId)
        {
            switch (voucherType.ToUpper())
            {
                case "RECEIPT":
                    var receiptVoucher = _receiptVoucherMasterRepository.GetAsQueryable()
                        .Where(r => r.ReceiptVoucherMasterVoucherNo == voucherNo)
                        .FirstOrDefault();
                    if (receiptVoucher != null)
                    {
                        receiptVoucher.ReceiptVoucherMasterAllocId = AllocationId;
                        _receiptVoucherMasterRepository.Update(receiptVoucher);
                        _receiptVoucherMasterRepository.SaveChangesAsync();
                    }
                    break;

                case "PAYMENT":
                    var paymentVoucher = _paymentVoucherRepository.GetAsQueryable()
                        .Where(r => r.PaymentVoucherVoucherNo == voucherNo)
                        .FirstOrDefault();
                    if (paymentVoucher != null)
                    {
                        paymentVoucher.PaymentVoucherAllocationId = AllocationId;
                        _paymentVoucherRepository.Update(paymentVoucher);
                        _paymentVoucherRepository.SaveChangesAsync();
                    }
                    break;

                case "BANK RECEIPT":
                    var bankReceiptVoucher = _bankReceiptVoucherRepository.GetAsQueryable()
                        .Where(r => r.BankReceiptVoucherVoucherNo == voucherNo)
                        .FirstOrDefault();
                    if (bankReceiptVoucher != null)
                    {
                        bankReceiptVoucher.BankReceiptVoucherAllocId = AllocationId;
                        _bankReceiptVoucherRepository.Update(bankReceiptVoucher);
                        _bankReceiptVoucherRepository.SaveChangesAsync();
                    }
                    break;

                case "BANK PAYMENT":
                    var bankPaymentVoucher = _bankPaymentVoucherRepository.GetAsQueryable()
                        .Where(r => r.BankPaymentVoucherVoucherNo == voucherNo)
                        .FirstOrDefault();
                    if (bankPaymentVoucher != null)
                    {
                        bankPaymentVoucher.BankPaymentVoucherAllocId = AllocationId;
                        _bankPaymentVoucherRepository.Update(bankPaymentVoucher);
                        _bankPaymentVoucherRepository.SaveChangesAsync();
                    }
                    break;

            }
        }
    }

}