////using Inspire.Erp.Application.Store.Interfaces;
////using Inspire.Erp.Domain.Entities;
////using Inspire.Erp.Domain.Modals;
////using Inspire.Erp.Infrastructure.Database;
////using Inspire.Erp.Infrastructure.Database.Repositoy;
////using Microsoft.Data.SqlClient;
////using Microsoft.EntityFrameworkCore;
////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;
////using static Inspire.Erp.Domain.Entities.Store;

////namespace Inspire.Erp.Application.Store.Implementation
////{
////    public class StoreWareHouses : IStoreWareHouse
////    {

////        private readonly InspireErpDBContext _sr;
////        private readonly IRepository<DamageEntryResponse> repo;
////        private readonly IRepository<ItemMaster> _IMrepo;
////        private readonly IRepository<VouchersNumbers> _vnrepo;
////        public StoreWareHouses(IRepository<ItemMaster> IMrepo,
////            IRepository<VouchersNumbers> vnrepo,
////            InspireErpDBContext sr,
////            IRepository<DamageEntryResponse> _repo)
////        {
////            _vnrepo = vnrepo;
////            _IMrepo = IMrepo;
////            repo = _repo;
////            _sr = sr;
////        }
////        public async Task<List<StockRegisterResponse>> getStockLedgerReport()
////        {
////            try
////            {
////                //return repo.getStoreWareHouse();
////                var response = await _sr.Set<StockRegisterResponse>().FromSqlInterpolated($"EXEC getStockLedgerRpt").ToListAsync();
////                return response;
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public async Task<List<StockMovementRptResponse>> getStockMovementRpt()
////        {
////            try
////            {
////                var response = await _sr.Set<StockMovementRptResponse>().FromSqlInterpolated($"EXEC getStockMovementRpt").ToListAsync();
////                return response;
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public async Task<List<ItemMaster>> getAllItems()
////        {
////            try
////            {
////                var response = _IMrepo.GetAsQueryable().Where(k => k.ItemMasterDelStatus == false).Select(k => new ItemMaster
////                {
////                    ItemMasterItemId = k.ItemMasterItemId,
////                    ItemMasterPartNo = k.ItemMasterPartNo,
////                    ItemMasterBarcode = k.ItemMasterBarcode,
////                    ItemMasterUnitId = k.ItemMasterUnitId,
////                    ItemMasterUnitPrice = k.ItemMasterUnitPrice,
////                    ItemMasterItemName = k.ItemMasterItemName
////                }).ToList();
////                //await _sr.Set<GetAllItemResponse>().FromSqlInterpolated($"EXEC getAllItems").ToListAsync();
////                return response;
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public async Task<List<GetFilteredStockLedgerRptResponse>> getFilteredStockLedgerRpt(StockLedgerReportModel obj)
////        {
////            try
////            {
////                var itemGroup = new SqlParameter("@itemGroup", obj.itemGroup);
////                var itemName = new SqlParameter("@itemName", obj.itemName);
////                var job = new SqlParameter("@job", obj.job);
////                var location = new SqlParameter("@location", obj.location);
////                var dateFrom = new SqlParameter("@dateFrom", obj.dateFrom);
////                var dateTo = new SqlParameter("@dateTo", obj.dateTo);
////                var response = await _sr.Set<GetFilteredStockLedgerRptResponse>().FromSqlInterpolated($"EXEC getFilteredStockLedgerRpt {itemGroup},{itemName},{job},{location},{dateFrom},{dateTo}").ToListAsync();
////                return response;
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public async Task<List<getStockMovementDetailsRptResponse>> getStockMovementDetailsRpt(ItemMasterViewModel obj)
////        {
////            try
////            {
////                var itemId = new SqlParameter("@itemGroup", obj.ItemMasterItemId);
////                var response = await _sr.Set<getStockMovementDetailsRptResponse>().FromSqlInterpolated($"EXEC getStockMovementDetailsRpt {itemId}").ToListAsync();
////                return response;
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public async Task<List<getItemDetailsByIdResponse>> getItemDetailsById(ItemMasterViewModel obj)
////        {
////            try
////            {
////                var itemId = new SqlParameter("@ItemMasterItemId", obj.ItemMasterItemId);
////                var response = await _sr.Set<getItemDetailsByIdResponse>().FromSqlInterpolated($"EXEC getItemDetailsById {itemId}").ToListAsync();
////                return response;
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public async Task<List<getItemDetailsByIdResponse>> getDetailsByItem()
////        {
////            try
////            {
////                var response = await _sr.Set<getItemDetailsByIdResponse>().FromSqlInterpolated($"EXEC getAllItemDetails").ToListAsync();
////                return response;
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public async Task<List<GetStockVoucherDetailsResponse>> getStockVchDetails(StockLedgerReportModel obj)
////        {
////            try
////            {
////                int id = int.Parse(obj.itemGroup);
////                var itemId = new SqlParameter("@itemId", id);
////                var response = await _sr.Set<GetStockVoucherDetailsResponse>().FromSqlInterpolated($"EXEC getStockVchDetails {itemId}").ToListAsync();
////                return response;
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public async Task<List<GetAllDepartmentResponse>> getAllDepartments()
////        {
////            try
////            {
////                var response = await _sr.Set<GetAllDepartmentResponse>().FromSqlInterpolated($"EXEC getAllDepartments").ToListAsync();
////                return response;
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public async Task<VouchersNumbers> getVoucherNumber()
////        {
////            try
////            {
////                var response = _vnrepo.GetAsQueryable().Where(k => k.VouhersNumbersDelStatus == false).OrderByDescending(k => k.VouchersNumbersVNoNu).FirstOrDefault();
////                return response;
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public async Task<List<CustomerEnquiryReportResponse>> customerEnquiryReport()
////        {
////            try
////            {
////                var response = await _sr.Set<CustomerEnquiryReportResponse>().FromSqlInterpolated($"EXEC CustomerEnquiryReport").ToListAsync();
////                return response;
////            }
////            catch (Exception)
////            {
////                throw;
////            }
////        }
////    }
////}
