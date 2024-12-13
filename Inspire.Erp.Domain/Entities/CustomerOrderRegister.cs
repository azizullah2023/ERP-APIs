using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CustomerOrderRegister
    {
        public int? CustomerOrderRegisterId { get; set; }
        public string CustomerOrderRegisterRefVoucherNo { get; set; }
        public string CustomerOrderRegisterOrderNo { get; set; }
        public int? CustomerOrderRegisterMaterialId { get; set; }
        public int? CustomerOrderRegisterUnitId { get; set; }
        public decimal? CustomerOrderRegisterQtyOrder { get; set; }
        public decimal? CustomerOrderRegisterQtyIssued { get; set; }
        public decimal? CustomerOrderRegisterRate { get; set; }
        public decimal? CustomerOrderRegisterAmount { get; set; }
        public decimal? CustomerOrderRegisterFcAmount { get; set; }
        public DateTime? CustomerOrderRegisterAssignedDate { get; set; }
        public int? CustomerOrderRegisterLocationId { get; set; }
        public string CustomerOrderRegisterStatus { get; set; }
        public string CustomerOrderRegisterTransType { get; set; }
        public int? CustomerOrderRegisterCustomerId { get; set; }
        public int? CustomerOrderRegisterFsno { get; set; }
        public bool? CustomerOrderRegisterDelStatus { get; set; }
    }
}
