using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Store.Interface
{
    public interface IStockTransferService
    {
        public Task<List<StockItemResponse>> getStockItemResponse();
        public Task<StockTransferModel> InsertStockTransfer(StockTransfer stockTransfer, List<StockTransferDetails> stockTransferDetails
             , List<StockRegister> stockRegister);
        //public Task<bool>  ddeleteStockTransfer(StockTransferRequestModel obj);

        public Task<bool> DeleteStockTransfer(StockTransfer stockTransfer, List<StockTransferDetails> stockTransferDetails
         , List<StockRegister> stockRegister);
        //public Task<bool> UpdateStockTransfer(StockTransferRequestModel obj);

        public Task<StockTransferModel> UpdateStockTransfer(StockTransfer stockTransfer, List<StockTransferDetails> stockTransferDetails
            , List<StockRegister> stockRegister);
        public IEnumerable<StockTransferResponseModel> getStockTransferReport();

        public IEnumerable<StockTransfer> GetStockTransfer();
        public StockTransferModel GetSavedStockTransferDetails(string pvno);
        public IEnumerable<StockTransfer> DeleteStockTransfers(string Id);
    }
}