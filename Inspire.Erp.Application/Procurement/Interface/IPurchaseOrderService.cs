using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Application.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Inspire.Erp.Domain.Modals;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Procurement.Interfaces
{
    public interface IPurchaseOrderService
    {
        public IEnumerable<UserFile> GetAllUserFile();
        public IEnumerable<ItemMaster> PurchaseOrder_GetAllItemMaster();
        public IEnumerable<UnitMaster> PurchaseOrder_GetAllUnitMaster();
        public IEnumerable<SuppliersMaster> PurchaseOrder_GetAllSuppliersMaster();
        public IEnumerable<LocationMaster> PurchaseOrder_GetAllLocationMaster();
        public PurchaseOrderModel InsertPurchaseOrder(PurchaseOrder purchaseOrder, List<AccountsTransactions> accountsTransactions, List<PurchaseOrderDetails> purchaseOrderDetails);
        public PurchaseOrderModel UpdatePurchaseOrder(PurchaseOrder purchaseOrder, List<AccountsTransactions> accountsTransactions, List<PurchaseOrderDetails> purchaseOrderDetails );
        public int DeletePurchaseOrder(PurchaseOrder purchaseOrder, List<AccountsTransactions> accountsTransactions, List<PurchaseOrderDetails> purchaseOrderDetails);
        public IEnumerable<AccountsTransactions> GetAllTransaction();
        public PurchaseOrderModel GetSavedPurchaseOrderDetails(string pvno);
        public IEnumerable<PurchaseOrder> GetPurchaseOrder();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);
        public VouchersNumbers GetVouchersNumbers(string pvno);
        public VouchersNumbers GetVouchersNumbers(string pvno,string vType);
        public IQueryable GetPODetailsForGRN();
        public List<PurchaseOrderDTO> GetAllPOForGRN(decimal id);


        public Task<Response<List<PODetailsViewModel>>> GetGRMPOs(int? supplierId);
    }
}
