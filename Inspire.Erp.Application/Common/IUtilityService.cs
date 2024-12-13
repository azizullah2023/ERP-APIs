using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Common
{
 public   interface IUtilityService
    {
        string ConvertNumbertoWords(int number);
        Task<Response<GetUserViewModel>> GetCurrentUser();
        Task<Response<bool>> AddUserTrackingLog(AddActivityLogViewModel model);
        Task<Response<DropdownResponse>> AddVoucherNumber(string type, string prefix);
        Task<Response<FinancialPeriods>> GetFinancialPeriods();
        Task<Response<bool>> SaveAccountTransaction(AccountsTransactions model);
        Task<Response<List<ItemMaster>>> GetItemMaster();
        Task<Response<bool>> SaveMasterAccountTable(MasterAccountsTable model);

        Task<decimal> GetBasicUnitConversion(int? ItemId, int? UnitId);

        Task<decimal> GetStockQuantity(long? itemId, int? locationId);
        Task<bool> SendEmailAsync(EmailRequestViewModel mailRequest);
    }
}
