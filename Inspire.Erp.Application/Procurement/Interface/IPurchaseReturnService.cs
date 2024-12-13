using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Procurement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Procurement.Interface
{
   public interface IPurchaseReturnService
    {
        Task<Response<bool>> AddEditPurchaseReturn(AddEditPurchaseReturnResponse model);
        Task<Response<PurchaseReturnVoucherResponse>> GetSpecificPurchaseVoucher(string id);
        Task<Response<GridWrapperResponse<List<GetPurchaseVoucherResponse>>>> GetPurchaseVoucherList(GenericGridViewModel model);
        Task<Response<PurchaseReturnVoucherResponse>> GetSpecificPurchaseReturn(string id = null);
        Task<Response<GridWrapperResponse<List<PurchaseReturnVoucherResponse>>>> GetPurchaseReturnList(GenericGridViewModel model);
        Task<Response<List<DropdownResponse>>> GetLocationMasterDropdown();
        Task<Response<List<DropdownResponse>>> GetJObMasterDropdown();
        Task<Response<List<DropdownResponse>>> GetSupplierMasterDropdown();
        Task<Response<List<DropdownResponse>>> GetVoucherTypeDropdown();

        List<PurchaseReturn> GetPurchaseReturn();
        PurchaseReturnModel GetSavedPurchaseReturnDetails(string pvno);

        PurchaseReturnModel InsertPurchaseReturn(PurchaseReturn purchaseReturn,
                                 List<AccountsTransactions> accountsTransactions,
                                 List<PurchaseReturnDetails> purchaseReturnDetails,
                                 List<StockRegister> stockRegister);
        PurchaseReturnModel UpdatePurchaseReturn(PurchaseReturn purchaseReturn,
                                 List<AccountsTransactions> accountsTransactions,
                                 List<PurchaseReturnDetails> purchaseReturnDetails,
                                 List<StockRegister> stockRegister);
        int DeletePurchaseReturn(PurchaseReturn purchaseReturn, 
                                 List<AccountsTransactions> accountsTransactions, 
                                 List<PurchaseReturnDetails> purchaseReturnDetails, 
                                 List<StockRegister> stockRegister);

        VouchersNumbers GetVouchersNumbers(string pvno);

        decimal? CurrencyConversion(PurchaseReturn purchaseReturn);
        decimal? GetCurrencyRate(PurchaseReturn pr);
    }
}
