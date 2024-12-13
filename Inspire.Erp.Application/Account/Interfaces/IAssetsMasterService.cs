using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IAssetsMasterService
    {
        Task<Response<List<MasterAccountsTable>>> GetFixedAccountForAssetMaster(GenericGridViewModel model);
        Task<Response<List<MasterAccountsTable>>> GetRelativeAccountAssetMaster(string accNo);
        Task<Response<AssetMaster>> GetAssetMaster(int id);
        Task<Response<List<AssetMaster>>> GetRelativeAssetMasterRecord(int accNo);
        Task<Response<bool>> AddEditAssetMaster(AssetMasterViewModel model);
    }
}
