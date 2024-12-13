using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Inspire.Erp.Domain.Models;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class AssetsMasterService : IAssetsMasterService
    {
        private readonly IRepository<MasterAccountsTable> _masterAccountTable;
        private readonly IRepository<AssetMaster> _asetMaster;
        public AssetsMasterService(IRepository<MasterAccountsTable> masterAccountTable, IRepository<AssetMaster> asetMaster)
        {
            _masterAccountTable = masterAccountTable;
            _asetMaster = asetMaster;
        }
        public async Task<Response<List<MasterAccountsTable>>> GetFixedAccountForAssetMaster(GenericGridViewModel model)
        {
            try
            {
                var response = new List<MasterAccountsTable>();
                var fixedAccount = await _masterAccountTable.GetAsQueryable().Where(x => x.MaAccName == "Fixed Asset").FirstOrDefaultAsync();
                var result = await _masterAccountTable.GetAsQueryable().Where(x => x.MaRelativeNo == fixedAccount.MaAccNo).ToListAsync();
                //var data = RecursionMasterAccountTable(result);
                return Response<List<MasterAccountsTable>>.Success(result, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<MasterAccountsTable>>.Fail(new List<MasterAccountsTable>(), ex.Message);
            }
        }
        public async Task<Response<List<MasterAccountsTable>>> GetRelativeAccountAssetMaster(string accNo)
        {
            try
            {
                var result = await _masterAccountTable.GetAsQueryable().Where(x => x.MaAccNo == accNo).ToListAsync();

                return Response<List<MasterAccountsTable>>.Success(result, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<MasterAccountsTable>>.Fail(new List<MasterAccountsTable>(), ex.Message);
            }
        }
        private List<MasterAccountsTable> RecursionMasterAccountTable(List<MasterAccountsTable> model)
        {
            var result = new List<MasterAccountsTable>();
            foreach (var account in model)
            {
                var masteraccount = _masterAccountTable.GetAsQueryable().Where(x => x.MaRelativeNo == account.MaAccNo).ToList();
                if (masteraccount.Count > 0)
                {
                    result.AddRange(RecursionMasterAccountTable(masteraccount));
                }
                result.Add(account);
            }
            return result;
        }

        public async Task<Response<AssetMaster>> GetAssetMaster(int id)
        {
            try
            {
                var result = await _asetMaster.GetAsQueryable().Where(x => x.AssetMasterAssetId == id).FirstOrDefaultAsync();

                return Response<AssetMaster>.Success(result, "Data found");
            }
            catch (Exception ex)
            {
                return Response<AssetMaster>.Fail(new AssetMaster(), ex.Message);
            }
        }

        public async Task<Response<List<AssetMaster>>> GetRelativeAssetMasterRecord(int accNo)
        {
            try
            {
                var result = await _asetMaster.GetAsQueryable().Where(x => x.AssetMasterAssetRelativeNo == accNo).ToListAsync();

                return Response<List<AssetMaster>>.Success(result, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<AssetMaster>>.Fail(new List<AssetMaster>(), ex.Message);
            }
        }

        public async Task<Response<bool>> AddEditAssetMaster(AssetMasterViewModel model)
        {
            try
            {
                if (model.AssetMasterAssetId == 0)
                {
                    var result = new AssetMaster()
                    {
                        AssetMasterAssetCode = model.AssetMasterAssetCode,
                        AssetMasterAssetName = model.AssetMasterAssetName,
                        AssetMasterAssetType = model.AssetMasterAssetType,
                        AssetMasterAssetCreatedDate = DateTime.Now,
                        AssetMasterAssetRelativeNo = model.AssetMasterAssetRelativeNo,
                        AssetMasterAssetAccountNo="",
                        AssetMasterAssetDepExpAccount= "",
                        AssetMasterAssetDepLibAccount= "",
                    };

                    _asetMaster.Insert(result);
                }
                else
                {
                    var result = await _asetMaster.GetAsQueryable().Where(x => x.AssetMasterAssetId == model.AssetMasterAssetId).FirstOrDefaultAsync();
                    result.AssetMasterAssetCode = model.AssetMasterAssetCode;
                    result.AssetMasterAssetName = model.AssetMasterAssetName;
                    result.AssetMasterAssetType = model.AssetMasterAssetType;
                    result.AssetMasterAssetRelativeNo =model.AssetMasterAssetRelativeNo;
                    _asetMaster.Update(result);
                }
                
                return Response<bool>.Success(true, "Data found");
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(false, ex.Message);
            }
        }
    }
}
