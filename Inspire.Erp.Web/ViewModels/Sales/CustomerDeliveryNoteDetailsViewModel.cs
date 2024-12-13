using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Sales
{
    public partial class CustomerDeliveryNoteDetailsViewModel
    {
        public long CustomerDeliveryNoteDetailsDetId { get; set; }
        public long? CustomerDeliveryNoteDetailsDeliveryNo { get; set; }
        public int? CustomerDeliveryNoteDetailsSlno { get; set; }
        public long? CustomerDeliveryNoteDetailsItemId { get; set; }
        public string? CustomerDeliveryNoteDetailsDescription { get; set; }
        public long? CustomerDeliveryNoteDetailsUnitId { get; set; }
        public decimal? CustomerDeliveryNoteDetailsQty { get; set; }
        public bool? CustomerDeliveryNoteDetailsIsEdited { get; set; }
        public long? CustomerDeliveryNoteDetailsFsno { get; set; }
        public long? CustomerDeliveryNoteDetailsCustomerPoNo { get; set; }
        public long? CustomerDeliveryNoteDetailsCpoSlno { get; set; }
        public long? CustomerDeliveryNoteDetailsPodId { get; set; }
        public int CustomerDeliveryNoteDetailsDeliveryDetailId { get; set; }
        public long? CustomerDeliveryNoteDetailsMatId2 { get; set; }
        public long? CustomerDeliveryNoteDetailsUnitId2 { get; set; }
        public double? CustomerDeliveryNoteDetailsBaseValue { get; set; }
        public double? CustomerDeliveryNoteDetailsFoc { get; set; }
        public bool? CustomerDeliveryNoteDetailsDelStatus { get; set; }


    }
}
