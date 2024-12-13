using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IAccountTransactionsServices
    {
        public Task<ApiResponse<List<AllocationDetails>>> GetReceiptVoucherAllocations(string accNo, string refNo, string vType);

        //public Task<ResponseInfo> getAllocationforPrint(string accNo, DateTime refNo, string vType);
        public Task<ResponseInfo> getAllocationforPrint(int? Id);

        

    }
}
