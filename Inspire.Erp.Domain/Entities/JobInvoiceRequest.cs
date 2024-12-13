using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class JobInvoiceRequest
    {
        public string? vno { get; set; }
        public string? vnonu { get; set; }
        public string? vtype { get; set; }
        public string? fsno { get; set; }
        public string? status { get; set; }
        public int? jobId { get; set; }
        public string? cname { get; set; }
        public string? cid { get; set; }
        public string? loactionId { get; set; }
        public string? salesId { get; set; }
        public string? discount { get; set; }
        public string? grossAmt { get; set; }
        public string? netAmt { get; set; }
        public string? remarks { get; set; }
        public string? uId { get; set; }
        public string? currId { get; set; }
        public string? lpoNo { get; set; }
        public string? lpoDate { get; set; }
        public string? saleAccount { get; set; }
        public string? itemId { get; set; }
        public string? unitId { get; set; }
        public string? unit { get; set; }
        public string? soldQty { get; set; }
        public string? unitPrice { get; set; }
        public string? qty { get; set; }
        public string? sin { get; set; }
        public string? sout { get; set; }
        public string? rate { get; set; }
        public string? amount { get; set; }
        public string? vdate { get; set; }
        public string? accNo { get; set; }
        public string? transDate { get; set; }
        public string? particular { get; set; }
        public string? debit { get; set; }
        public string? credit { get; set; }
        public string? depId { get; set; }
        public string? address { get; set; }
        public string? coutPerson { get; set; }
        public List<VouchersNumbers>? vn { get; set; }
        public List<AccountsTransactions>? at { get; set; }
        public List<JobInvoice>? ji { get; set; }
        public List<JobInvoiceDetails>? jid { get; set; }


    }
}
