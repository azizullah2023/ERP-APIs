using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Models.Procurement.PurchaseOrderTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Procurement.Interfaces
{
    public interface IPurchaseRequisitionService
    {

        
        ////     public IEnumerable<ReportPurchaseRequisition> PurchaseRequisition_GetReportPurchaseRequisition();
        ////public IEnumerable<ItemMaster> PurchaseRequisition_GetAllItemMaster();
        ////public IEnumerable<UnitMaster> PurchaseRequisition_GetAllUnitMaster();

        ////public IEnumerable<SuppliersMaster> PurchaseRequisition_GetAllSuppliersMaster();
        ////public IEnumerable<LocationMaster> PurchaseRequisition_GetAllLocationMaster();

        //public PurchaseRequisition InsertPurchaseRequisition(PurchaseRequisition purchaseRequisition,
        //  List<PurchaseRequisitionDetails> purchaseRequisitionDetails);

        public PurchaseRequisitionModel InsertPurchaseRequisition(PurchaseRequisition purchaseRequisition, List<AccountsTransactions> accountsTransactions, List<PurchaseRequisitionDetails> purchaseRequisitionDetails
            //, List<StockRegister> stockRegister
            );
        public PurchaseRequisitionModel UpdatePurchaseRequisition(PurchaseRequisition purchaseRequisition, List<AccountsTransactions> accountsTransactions, List<PurchaseRequisitionDetails> purchaseRequisitionDetails
            //, List<StockRegister> stockRegister
            );
        public int DeletePurchaseRequisition(PurchaseRequisition purchaseRequisition, List<AccountsTransactions> accountsTransactions, List<PurchaseRequisitionDetails> purchaseRequisitionDetails
            //, List<StockRegister> stockRegister
            );
        public IEnumerable<AccountsTransactions> GetAllTransaction();
  

        
        public PurchaseRequisitionModel GetSavedPurchaseRequisitionDetails(string pvno);
        public IEnumerable<PurchaseRequisition> GetPurchaseRequisition();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);

        public VouchersNumbers GetVouchersNumbers(string pvno);

        public Task<Response<List<PurChaseRequisitionStatus>>> GetPurchaseRequisitionStatus(PurChaseRequisitionStatusFilterReport filter);
        public Task<Response<List<PurChaseReqFields>>> GetPurchaseReqJobId(PurChaseRequisitionFilterReport filter);
    }
}
