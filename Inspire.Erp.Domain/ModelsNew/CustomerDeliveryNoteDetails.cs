using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.ModelsNew
{
    public partial class CustomerDeliveryNoteDetails
    {
        public long CustomerDeliveryNoteDetailsDetId { get; set; }
        public long? CustomerDeliveryNoteDetailsDeliveryNo { get; set; }
        public int? CustomerDeliveryNoteDetailsSlno { get; set; }
        public long? CustomerDeliveryNoteDetailsItemId { get; set; }
        public string CustomerDeliveryNoteDetailsDescription { get; set; }
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

        public virtual CustomerDeliveryNote CustomerDeliveryNoteDetailsDeliveryNoNavigation { get; set; }
    }
}
