using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Procurement.Interfaces
{
    public interface IPurchaseQuotationService
    {

        
        ////public IEnumerable<ReportPurchaseQuotation> PurchaseQuotation_GetReportPurchaseQuotation();
        //public IEnumerable<ItemMaster> PurchaseQuotation_GetAllItemMaster();
        //public IEnumerable<UnitMaster> PurchaseQuotation_GetAllUnitMaster();

        //public IEnumerable<SuppliersMaster> PurchaseQuotation_GetAllSuppliersMaster();
        //public IEnumerable<LocationMaster> PurchaseQuotation_GetAllLocationMaster();

        //public PurchaseQuotation InsertPurchaseQuotation(PurchaseQuotation purchaseQuotation,
        //  List<PurchaseQuotationDetails> purchaseQuotationDetails);

        public PurchaseQuotationModel InsertPurchaseQuotation(PurchaseQuotation purchaseQuotation, List<AccountsTransactions> accountsTransactions, List<PurchaseQuotationDetails> purchaseQuotationDetails
            //, List<StockRegister> stockRegister
            );
        public PurchaseQuotationModel UpdatePurchaseQuotation(PurchaseQuotation purchaseQuotation, List<AccountsTransactions> accountsTransactions, List<PurchaseQuotationDetails> purchaseQuotationDetails
            //, List<StockRegister> stockRegister
            );
        public int DeletePurchaseQuotation(PurchaseQuotation purchaseQuotation, List<AccountsTransactions> accountsTransactions, List<PurchaseQuotationDetails> purchaseQuotationDetails
            //, List<StockRegister> stockRegister
            );
        public IEnumerable<AccountsTransactions> GetAllTransaction();
  

        
        public PurchaseQuotationModel GetSavedPurchaseQuotationDetails(string pvno);
        public IEnumerable<PurchaseQuotation> GetPurchaseQuotation();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);

        public VouchersNumbers GetVouchersNumbers(string pvno);

    }
}
