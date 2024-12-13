using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class MaterialsMaster
    {
        public int? MaterialsMasterId { get; set; }
        public string MaterialsMasterRelativeNo { get; set; }
        public string MaterialsMasterCode { get; set; }
        public string MaterialsMasterManualCode { get; set; }
        public string MaterialsMasterManufactureCode { get; set; }
        public string MaterialsMasterSupplierCode { get; set; }
        public string MaterialsMasterName { get; set; }
        public double? MaterialsMasterPrice { get; set; }
        public string MaterialsMasterAccType { get; set; }
        public string MaterialsMasterMainHead { get; set; }
        public string MaterialsMasterSubHead { get; set; }
        public string MaterialsMasterImageKey { get; set; }
        public string MaterialsMasterSystemAcc { get; set; }
        public int? MaterialsMasterFsno { get; set; }
        public string MaterialsMasterStatus { get; set; }
        public int? MaterialsMasterUserId { get; set; }
        public DateTime? MaterialsMasterDateCreated { get; set; }
        public string MaterialsMasterGpAcc { get; set; }
        public string MaterialsMasterAcAcc { get; set; }
        public string MaterialsMasterEdAcc { get; set; }
        public double? MaterialsMasterReorderLevel { get; set; }
        public string MaterialsMasterGroupAccNo { get; set; }
        public string MaterialsMasterGroupPurchaseAcc { get; set; }
        public double? MaterialsMasterOpeningStock { get; set; }
        public int? MaterialsMasterRackId { get; set; }
        public string MaterialsMasterPack { get; set; }
        public string MaterialsMasterAssetAcc { get; set; }
        public string MaterialsMasterExpenseAcc { get; set; }
        public int? MaterialsMasterLocationId { get; set; }
        public int? MaterialsMasterSupplierId { get; set; }
        public DateTime? MaterialsMasterPriceDate { get; set; }
        public bool? MaterialsMasterDelStatus { get; set; }
    }
}
