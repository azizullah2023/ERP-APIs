using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PurOrderRegister
    {
        public int PurOrderRegisterId { get; set; }
        public string PurOrderRegisterRefVoucherNo { get; set; }
        public string PurOrderRegisterOrderNo { get; set; }
        public int? PurOrderRegisterMaterialId { get; set; }
        public int? PurOrderRegisterUnitId { get; set; }
        public decimal? PurOrderRegisterQtyOrder { get; set; }
        public decimal? PurOrderRegisterQtyIssued { get; set; }
        public decimal? PurOrderRegisterRate { get; set; }
        public decimal? PurOrderRegisterAmount { get; set; }
        public decimal? PurOrderRegisterFcAmount { get; set; }
        public int? PurOrderRegisterLocationId { get; set; }
        public DateTime? PurOrderRegisterAssignedDate { get; set; }
        public string PurOrderRegisterTransType { get; set; }
        public int? PurOrderRegisterFsno { get; set; }
        public int? PurOrderRegisterSupplierId { get; set; }
        public string PurOrderRegisterStatus { get; set; }
        public int? PurOrderRegisterDetailId { get; set; }
        public bool? PurOrderRegisterDelStatus { get; set; }
    }
}
