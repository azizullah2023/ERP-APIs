using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TrackingRegister
    {
        public int TrackingRegisterTrackId { get; set; }
        public string TrackingRegisterVoucherNo { get; set; }
        public int? TrackingRegisterDetailsId { get; set; }
        public int? TrackingRegisterSno { get; set; }
        public int? TrackingRegisterMaterialId { get; set; }
        public string TrackingRegisterVoucherType { get; set; }
        public decimal? TrackingRegisterQty { get; set; }
        public decimal? TrackingRegisterQtyin { get; set; }
        public decimal? TrackingRegisterQtyout { get; set; }
        public decimal ? TrackingRegisterRate { get; set; }
        public DateTime? TrackingRegisterTrackDate { get; set; }
        public string TrackingRegisterRefVno { get; set; }
        public long? TrackingRegisterFsno { get; set; }
        public bool? TrackingRegisterDelStatus { get; set; }
    }
}
