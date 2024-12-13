using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models
{
    public class AssetMasterViewModel
    {
        public int AssetMasterAssetId { get; set; }
        public string AssetMasterAssetCode { get; set; }
        public int? AssetMasterAssetRelativeNo { get; set; }
        public string AssetMasterAssetName { get; set; }
        public string AssetMasterAssetType { get; set; }
        public string AssetMasterAssetAccountNo { get; set; }
        public string AssetMasterAssetDepLibAccount { get; set; }
        public string AssetMasterAssetDepExpAccount { get; set; }
        public DateTime? AssetMasterAssetCreatedDate { get; set; }
        public bool? AssetMasterAssetDelStatus { get; set; }
    }
}
