using Inspire.Erp.Domain.ModelsNew;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{    
    public partial class CustomerDeliveryNote
    {
        public CustomerDeliveryNote()
        {
            CustomerDeliveryNoteDetails = new HashSet<CustomerDeliveryNoteDetails>();
        }
        public long CustomerDeliveryNoteDeliveryID { get; set; }
        public DateTime? CustomerDeliveryNoteDeliveryDate { get; set; }
        public long? CustomerDeliveryNoteCPOID { get; set; }
        public DateTime? CustomerDeliveryNoteCPODate { get; set; }
        public long? CustomerDeliveryNoteLocationID { get; set; }
        public long? CustomerDeliveryNoteCustomerCode { get; set; }
        public string CustomerDeliveryNoteCustomerName { get; set; }
        public long? CustomerDeliveryNoteSalesManID { get; set; }
        public long? CustomerDeliveryNoteCurrencyID { get; set; }
        public string CustomerDeliveryNoteDeliveryAddress { get; set; }
        public string CustomerDeliveryNoteRemarks { get; set; }
        public long? CustomerDeliveryNoteFSNO { get; set; }
        public long? CustomerDeliveryNoteUserID { get; set; }
        public string CustomerDeliveryNoteNote { get; set; }
        public string CustomerDeliveryNoteWarranty { get; set; }
        public string CustomerDeliveryNoteTraining { get; set; }
        public string CustomerDeliveryNoteTechnicalDetails { get; set; }
        public string CustomerDeliveryNoteTerms { get; set; }
        public string CustomerDeliveryNotePacking { get; set; }
        public string CustomerDeliveryNoteQuality { get; set; }
        public bool? CustomerDeliveryNoteIssuedStatus { get; set; }
        public string CustomerDeliveryNoteAttention { get; set; }
        public long? CustomerDeliveryNotePODID { get; set; }
        public long? CustomerDeliveryNoteDelDetID { get; set; }
        public long? CustomerDeliveryNoteManuelPO { get; set; }
        public string CustomerDeliveryNoteDeliveryStatus { get; set; }
        public bool? CustomerDeliveryNoteDelStatus { get; set; }

        public virtual ICollection<CustomerDeliveryNoteDetails> CustomerDeliveryNoteDetails { get; set; }
    }
}