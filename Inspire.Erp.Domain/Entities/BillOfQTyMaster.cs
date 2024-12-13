using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inspire.Erp.Domain.Entities
{
    public class BillofQtyMaster
    {
        public BillofQtyMaster()
        {
            BillOfQTyDetails = new HashSet<BillOfQTyDetails>();
        }

        public decimal Id { get; set; }
        public string BillofqtyNo { get; set; }
        public DateTime? Date { get; set; }
        public decimal? CustomerEnqId { get; set; }
        public string Remarks { get; set; }
        [NotMapped]
        public virtual ICollection<BillOfQTyDetails> BillOfQTyDetails { get; set; }
    }

    public class BillofQtyMasterModel
    {

        public decimal Id { get; set; }
        public string BillofqtyNo { get; set; }
        public DateTime? Date { get; set; }
        public decimal? CustomerEnqId { get; set; }
        public string Remarks { get; set; }
        public virtual List<BillOfQTyDetailsModel> BillOfQTyDetails { get; set; }
    }
    public class BoqDetailDto
    {
        public decimal Id { get; set; }
        public decimal dId { get; set; }
        public string BillofqtyNo { get; set; }
        public DateTime? Date { get; set; }
        public decimal? CustomerEnqId { get; set; }
        public decimal? ItemCode { get; set; }
        public decimal? Qty { get; set; }
        public decimal? UnitCode { get; set; }
    }

}
