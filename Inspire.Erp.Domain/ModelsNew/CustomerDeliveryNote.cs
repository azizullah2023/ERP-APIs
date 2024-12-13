using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.ModelsNew
{
    public partial class CustomerDeliveryNote
    {
        public CustomerDeliveryNote()
        {
            CustomerDeliveryNoteDetails = new HashSet<CustomerDeliveryNoteDetails>();
        }

        public long CustomerDeliveryNoteDeliveryId { get; set; }
        public DateTime? CustomerDeliveryNoteDeliveryDate { get; set; }
        public long? CustomerDeliveryNoteCpoId { get; set; }
        public DateTime? CustomerDeliveryNoteCpoDate { get; set; }
        public long? CustomerDeliveryNoteLocationId { get; set; }
        public long? CustomerDeliveryNoteCustomerCode { get; set; }
        public string CustomerDeliveryNoteCustomerName { get; set; }
        public long? CustomerDeliveryNoteSalesManId { get; set; }
        public long? CustomerDeliveryNoteCurrencyId { get; set; }
        public string CustomerDeliveryNoteDeliveryAddress { get; set; }
        public string CustomerDeliveryNoteRemarks { get; set; }
        public long? CustomerDeliveryNoteFsno { get; set; }
        public long? CustomerDeliveryNoteUserId { get; set; }
        public string CustomerDeliveryNoteNote { get; set; }
        public string CustomerDeliveryNoteWarranty { get; set; }
        public string CustomerDeliveryNoteTraining { get; set; }
        public string CustomerDeliveryNoteTechnicalDetails { get; set; }
        public string CustomerDeliveryNoteTerms { get; set; }
        public string CustomerDeliveryNotePacking { get; set; }
        public string CustomerDeliveryNoteQuality { get; set; }
        public bool? CustomerDeliveryNoteIssuedStatus { get; set; }
        public string CustomerDeliveryNoteAttention { get; set; }
        public long? CustomerDeliveryNotePodId { get; set; }
        public long? CustomerDeliveryNoteDelDetId { get; set; }
        public long? CustomerDeliveryNoteManuelPo { get; set; }
        public string CustomerDeliveryNoteDeliveryStatus { get; set; }
        public bool? CustomerDeliveryNoteDelStatus { get; set; }

        public virtual ICollection<CustomerDeliveryNoteDetails> CustomerDeliveryNoteDetails { get; set; }
    }
}
