using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Inspire.Erp.Domain.Entities.StoreWareHouse;

namespace Inspire.Erp.Application.Store.Interfaces
{
    public interface IStoreWareHouse
    {
        public Task<List<StockRegisterResponse>> getStockLedgerReport();
        public Task<List<StockMovementRptResponse>> getStockMovementRpt();
        public Task<List<ItemMaster>> getAllItems();
      //  public Task<dynamic> getAllBrands();
        public Task<List<GetFilteredStockLedgerRptResponse>> getFilteredStockLedgerRpt(StockLedgerReportModel obj);
        public Task<List<getStockMovementDetailsRptResponse>> getStockMovementDetailsRpt(ItemMasterViewModel id);
        public Task<List<getItemDetailsByIdResponse>> getItemDetailsById(ItemMasterViewModel id);
        public Task<List<getItemDetailsByIdResponse>> getDetailsByItem();
        public Task<List<GetStockVoucherDetailsResponse>> getStockVchDetails(StockLedgerReportModel obj);
        public Task<List<GetAllDepartmentResponse>> getAllDepartments();
        public Task<VouchersNumbers> getVoucherNumber();
        public Task<List<CustomerEnquiryReportResponse>> customerEnquiryReport();

    }
}
