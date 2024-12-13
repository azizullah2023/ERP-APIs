using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inspire.Erp.Domain.Entities
{
    public class BillOfQTyDetails
    {
        public decimal Id { get; set; }
        public decimal? BillQtyId { get; set; }
        public decimal? ItemCode { get; set; }
        public decimal? UnitCode { get; set; }
        public decimal? Qty { get; set; }
        public string Remarks { get; set; }
        public bool? IsEdit { get; set; }
        public virtual BillofQtyMaster BillofQtyMaster { get; set; }

    }

    public class BillOfQTyDetailsModel
    {
        public decimal Id { get; set; }
        public decimal? BillQtyId { get; set; }
        public decimal? ItemCode { get; set; }
        public decimal? UnitCode { get; set; }
        public decimal? Qty { get; set; }
        public string Remarks { get; set; }
        public bool? IsEdit { get; set; }        

    }
}
