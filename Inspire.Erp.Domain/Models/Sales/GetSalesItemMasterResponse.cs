using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Sales
{
  public  class GetSalesItemMasterResponse
    {
        public long ItemMasterItemId { get; set; }
        public string ItemMasterMaterialCode { get; set; }
        public long? ItemMasterAccountNo { get; set; }
        public string ItemMasterItemName { get; set; }
        public string ItemMasterItemType { get; set; }
        public string ItemMasterBatchCode { get; set; }
        public long? ItemMasterVenderId { get; set; }
        public long? ItemMasterLocationId { get; set; }
        public string ItemMasterPartNo { get; set; }
        public int? ItemMasterReOrderLevel { get; set; }
        public decimal? ItemMasterUnitPrice { get; set; }
    }
}
