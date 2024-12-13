using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inspire.Erp.Application.Common;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IAssetsOpeningService
    {
        Task<Response<List<DropdownResponse>>> GetCurrencyMaster();
        Task<Response<List<GetAssetsMasterList>>> GetAssetMaster();
        Task<AssetPurchaseVoucherModel> InsertAssetPurchaseVoucher(AssetPurchaseVoucher assetPurchaseVoucher
            //, List<AccountsTransactions> accountsTransactions
            );
        AssetPurchaseVoucherModel UpdateAssetPurchaseVoucher(AssetPurchaseVoucher assetPurchaseVoucher, List<AccountsTransactions> accountsTransactions);
        int DeleteAssetPurchaseVoucher(AssetPurchaseVoucher assetPurchaseVoucher, List<AccountsTransactions> accountsTransactions);
        public AssetPurchaseVoucherModel GetSavedAssetPurchaseVoucherDetails(string pvno);
        public IEnumerable<AssetPurchaseVoucher> GetAssetPurchaseVouchers();
        Task<Response<bool>> AddEditAssetOpening(AddEditAssetPurchaseVoucher model);
        Task<Response<AddEditAssetPurchaseVoucher>> GetAssetOpening(int id = 0);
        Task<Response<GridWrapperResponse<List<GetAssetPurchaseVoucher>>>> GetAssetOpeningList(GenericGridViewModel model);
    }
}
