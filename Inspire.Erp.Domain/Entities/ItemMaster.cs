﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ItemMaster
    {
        public ItemMaster()
        {

            ItemImages = new HashSet<ItemImages>();
            ItemPriceLevelDetails = new HashSet<ItemPriceLevelDetails>();
            ItemSupplierDetails = new HashSet<ItemSupplierDetails>();
            UnitDetails = new List<UnitDetails>();
        }

        public long ItemMasterItemId { get; set; }
        public string ItemMasterMaterialCode { get; set; }
        public long? ItemMasterAccountNo { get; set; }
        public string ItemMasterItemName { get; set; }
        public string ItemMasterItemType { get; set; }
        public string ItemMasterBatchCode { get; set; }
        public long? ItemMasterVenderId { get; set; }
        public long? ItemMasterLocationId { get; set; }
        public int? ItemMasterReOrderLevel { get; set; }
        public decimal? ItemMasterUnitPrice { get; set; }
        public decimal? ItemMasterUnitPrice1 { get; set; }
        public decimal? ItemMasterUnitPrice2 { get; set; }
        public string ItemMasterItemSize { get; set; }
        public string ItemMasterPartNo { get; set; }
        public string ItemMasterColor { get; set; }
        public string ItemMasterPacking { get; set; }
        public string ItemMasterWeight { get; set; }
        public string ItemMasterShape { get; set; }
        public string ItemMasterRef1 { get; set; }
        public string ItemMasterRef2 { get; set; }
        public int? ItemMasterStockType { get; set; }
        public long? ItemMasterUnitId { get; set; }
        public string ItemMasterAssetAcc { get; set; }
        public string ItemMasterExpenseAcc { get; set; }
        public byte[] ItemMasterImage { get; set; }
        public bool? ItemMasterActive { get; set; }
        public decimal? ItemMasterLastPurchasePrice { get; set; }
        public bool? ItemMasterServices { get; set; }
        public string ItemMasterSuplierCode { get; set; }
        public string ItemMasterBarcode { get; set; }
        public string ItemMasterAliasName { get; set; }
        public long? ItemMasterUserId { get; set; }
        public string ItemMasterGroupDebitAcc { get; set; }
        public string ItemMasterGroupCreditAcc { get; set; }
        public double? ItemMasterLandingCost { get; set; }
        public double? ItemMasterBaseValue { get; set; }
        public double? ItemMasterHeightN { get; set; }
        public double? ItemMasterWeightN { get; set; }
        public long? ItemMasterRackId { get; set; }
        public double? ItemMasterCurrentStock { get; set; }
        public bool? ItemMasterStatus { get; set; }
        public string ItemMasterAvgCostYel { get; set; }
        public long? ItemMasterVat { get; set; }
        public string ItemMasterTypeId { get; set; }
        public long? ItemMasterModelId { get; set; }
        public decimal? ItemMasterVatPercentage { get; set; }
        public bool? ItemMasterVatInclues { get; set; }
        public bool? ItemMasterReorderCheck { get; set; }
        public string ItemMasterDefaultLocationName { get; set; }
        public int? ItemMasterDefaultLocation { get; set; }
        public int? ItemMasterCountryId { get; set; }
        public string ItemMasterPackageId { get; set; }
        public string ItemMasterGenericName { get; set; }
        public int? ItemMasterSupplierId { get; set; }
        public string ItemMasterManufactureName { get; set; }
        public decimal? ItemMasterRedeemRewardPoint { get; set; }
        public decimal? ItemMasterProPurchaseRate { get; set; }
        public decimal? ItemMasterProSaleRate { get; set; }
        public decimal? ItemMasterProMrp { get; set; }
        public int? ItemMasterVATID { get; set; }
        public bool? ItemMasterDelStatus { get; set; }


        public virtual ICollection<ItemImages> ItemImages { get; set; }
        public virtual ICollection<ItemPriceLevelDetails> ItemPriceLevelDetails { get; set; }
        public virtual ICollection<ItemSupplierDetails> ItemSupplierDetails { get; set; }
        public virtual ICollection<UnitDetails> UnitDetails { get; set; }

    }
    public class itemAvgPrice
    {
        public int ItemMasterItemId { get; set; }
        public decimal stockIn { get; set; }
        public decimal stockout { get; set; }
        public decimal InAmount { get; set; }


    }

    public class AltItemMaster
    {
        public int DocID { get; set; }
        public DateTime? DocDate { get; set; }
        public string Remarks { get; set; }

        [NotMapped]
        public virtual List<AltItemDetails> AltItemDetails { get; set; }
    }
    public class AltItemDetails
    {        
        public int DetID { get; set; }
        public int DocID { get; set; }
        public int ItemID { get; set; }
        public int? UnitID { get; set; }
        public int AltItemID { get; set; }
        public int AltItemUnitID { get; set; }       
        public string Remarks { get; set; }
    }
}
