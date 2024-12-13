using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IVoucherAllocationService
    {
        Task<Response<List<DropdownResponse>>> GetCustomerMasterDropdown();
        Task<Response<List<DropdownResponse>>> GetSupplierMasterDropdown();
        Task<Response<bool>> AddEditVoucherAllocation(AllocationVoucher model);
        Task<Response<GridWrapperResponse<List<GetAccountTransactionMutilAccountResponse>>>> GetAccountTransactions(GenericGridViewModel model);
        Task<Response<GridWrapperResponse<List<GetVoucherAllocationListResponse>>>> GetVoucherAllocationsList(GenericGridViewModel model);
        Task<Response<GetVoucherAllocationListResponse>> GetSpecificVoucherAllocation(string id);
        IQueryable GetVoucherAllocation();
        Response<AllocationVoucher> DeleteAllocationVoucher(string id,string type);
    }
}
