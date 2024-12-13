using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class StoreWareHouse
    {
        public class StockLedgerReportModel
        {
            public string itemGroup { get; set; }
            public string itemName { get; set; }
            public string dateFrom { get; set; }
            public string dateTo { get; set; }
            public string brand { get; set; }
            public string location { get; set; }
            public string job { get; set; }
            public string detailsType { get; set; }
            public string dateType { get; set; }
        }

        public class ItemMasterViewModel
        {
            public long? ItemMasterItemId { get; set; }
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
            public bool? ItemMasterStockType { get; set; }
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
            public string ItemMasterSupplierName { get; set; }
            public string ItemMasterManufactureName { get; set; }
            public decimal? ItemMasterRedeemRewardPoint { get; set; }
            public decimal? ItemMasterProPurchaseRate { get; set; }
            public decimal? ItemMasterProSaleRate { get; set; }
            public decimal? ItemMasterProMrp { get; set; }
            public bool? ItemMasterDelStatus { get; set; }

            public List<ItemPriceLevelDetailsViewModel> ItemPriceLevelDetails { get; set; }
            public List<UnitDetailsViewModel> UnitDetails { get; set; }
        }
        public class ItemPriceLevelDetailsViewModel
        {
            public int? ItemPriceLevelId { get; set; }
            public long? ItemId { get; set; }
            public long? LevelId { get; set; }
            public double? LevelRate { get; set; }
            public double? LevelAmt { get; set; }
            public int? UnitId { get; set; }
        }
        public class UnitDetailsViewModel
        {
            public int? UnitDetailsId { get; set; }
            public long? UnitDetailsItemId { get; set; }
            public int? UnitDetailsUnitId { get; set; }
            public double? UnitDetailsConversionType { get; set; }
            public double? UnitDetailsRate { get; set; }
            public double? UnitDetailsWrate { get; set; }
            public bool? UnitDetailsDefUnit { get; set; }
            public bool? UnitDetailsDefWunit { get; set; }
            public decimal? UnitDetailsHeight { get; set; }
            public decimal? UnitDetailsWidth { get; set; }
            public decimal? UnitDetailsReorder { get; set; }
            public bool? UnitDetailsDelStatus { get; set; }
        }
    }
}
