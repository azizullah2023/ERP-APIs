using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inspire.Erp.Domain.Entities.POS;
using System.Linq;
using System.Transactions;
using Inspire.Erp.Domain.Models;

namespace Inspire.Erp.Application.Master
{
    public interface IItemMasterService
    {
        IEnumerable<ItemMaster> GetAllItemMaster();
        public Task<IEnumerable<ItemMaster>> InsertItem(ItemMaster itemMaster);
        public Task<ItemMaster> NewItem(ItemMaster itemMaster);
        public ItemMaster UpdateNewItem(ItemMaster itemMaster);
        public IEnumerable<ItemMaster> UpdateItem(ItemMaster itemMaster);
        public IEnumerable<ItemMaster> DeleteItem(ItemMaster itemMaster);
        public List<ItemMaster> GetAllItem();
        public IEnumerable<ItemMaster> GetAllItemById(int id);
        public Task<Response<itemAvgPrice>> GetAveragePrice(int id);
        
        public List<ItemMaster> GetAllItemNotGroup();
        public IEnumerable<ItemStockType> GetAllStockType();
        public IEnumerable<ItemMaster> GetItemMastersByName(string name);
        public IEnumerable<ItemMaster> GetAllItemSearchFilter(string group, string name);
        public ItemMaster GetAllItemMasterById(int id);
        public ItemMaster GetAllItemMasterByBarCode(string  barCode);
        //   public IEnumerable<ItemMaster> GetAllItemSearchFilter(string group, string name);
        public IEnumerable<ItemMaster> GetAllItemGroupAndSubGroup();
        public int ItemMasterIdMax();
        Task<Response<List<ItemResponse>>> GetItemsBySearch(string filter);

        //POS
        public Task<Response<ItemMasterResponse>> SearchItemByBarcode(ItemBarCodeSearchFilter model);
        public Task<Response<POS_WorkPeriod>> GetWorkPeriod(WorkPeriodFilter model);
        public Task<Response<List<ItemMasterResponse>>> GetItems(string? barCode);


        public Task<Response<List<CardType>>> GetCardTypes();
        public Task<Response<ItemMasterResponse>> SearchItemByUnitDetailsId(ItemBarCodeSearchFilter model);
        public Task<Response<List<ItemMasterResponse>>>  SearchHoldBill(string voucherNo);

        public Task<Response<BillRecallResponse>> BillRecall(string voucherNo);

        public Task<Response<SaleVoucherResponse>> Resettlement(string voucherNo);
        public Task<Response<List<SaleVoucherTempResponse>>> LoadHoldBills(int workPeriodId);

        public Task<Response<VoidResponse>> VoidBill(VoidRequest model);

        public Task<Response<List<SettlementDetailsResponse>>> AddSettlementDetails(SettlementDetailsRequest model);
        public Task<Response<CardAmountResponse>> CalculateCardAmount(CardAmountRequest model);
        public Task<Response<ProcessCashCardResponse>> ProcessCard(CardRequest model);

        public Task<Response<ProcessCashCardResponse>> ProcessCardResettlement(CardResettlementRequest model);
        public Task<Response<ProcessCashCardResponse>> ProcessCashResettlement(CardResettlementRequest model);
        public Task<Response<ProcessCashCardResponse>> ProcessCashCardResettlement(CardResettlementRequest model);
        public Task<Response<bool>> Hold(CardRequest model);
        public Task<Response<CardAmountResponse>> ProcessCashTransaction(CardRequest model);

        public Task<PrintData> PrintReciept(string voucherNo);

        public Task<PrintData> SummaryDateWise(DateTime fromDate, DateTime toDate, int wpId, int stationId);
        public Task<PrintData> SummaryReport( int wpId, int stationId);
        public Task<PrintData> DayEndReport(DateTime fromDate, DateTime toDate,int wpId, int stationId);

        public Task<PrintData> TodayItemSales(DateTime fromDate, DateTime toDate, int wpId, int stationId);
        public Task<PrintData> TransactionDateWise(DateTime fromDate, DateTime toDate, int wpId, int stationId);

        public Task<PrintData> ZReport(DateTime fromDate, DateTime toDate, int wpId, int stationId);


    }
}