using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public partial class PurchaseOrderModel
    {
        public PurchaseOrder purchaseOrder { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<PurchaseOrderDetails> purchaseOrderDetails { get; set; }
        public List<StockRegister> stockRegister { get; set; }
    }
    public class PurchaseOrderDTO
    {
        public PurchaseOrder PurchaseOrder { get; set; }
        public List<PurchaseOrderDetailsDTO> PurchaseOrderDetails { get; set; }

    }
    public class PurchaseOrderDetailsDTO
    {
        public decimal? itemId { get; set; }
        public string? itemname { get; set; }
        public decimal? unitId { get; set; }
        public string? unit { get; set; }
        public decimal? price { get; set; }
        public decimal? poQty { get; set; }
    }

    public class PODetailsViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int POId { get; set; }
        public decimal? UnitId { get; set; }
        public string UnitName { get; set; }
        public int PODId { get; set; }
        public string PONo { get; set; }
        public DateTime PODate { get; set; }
        public string SupplierName { get; set; }
        public decimal? Rate { get; set; }
        public int Quantity { get; set; }
        public int BalanceQuantity { get; set; }

        public int DeliveredQuantity { get; set; }
    }
}
