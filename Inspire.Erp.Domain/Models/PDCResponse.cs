using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class PDCResponse
    {
        public class PDCPostedList {
            public string? VNO { get; set; }
            public string? PartyName { get; set; }
            public string? VType { get; set; }
            public DateTime? TDate { get; set; }
            public DateTime? PDate { get; set; }
            public string? CNO { get; set; }
            public double? CAmount { get; set; }
            public string? CBName { get; set; }
            public string? BAccNo { get; set; }
            public string? PDCVNO { get; set; }
            public DateTime? POSTDt { get; set; }
            public int? PID { get; set; }
            public string Tran_Type { get; set; }
            public string ChequeStatus { get; set; }
        }

        public class PDCGetList {
            public bool Pdc { get; set; }
            public string? VNO { get; set; }
            public string? PartyName { get; set; }
            public string? VType { get; set; }
            public DateTime? TDate { get; set; }
            public DateTime? PDate { get; set; }
            public string? CNO { get; set; }
            public double? CAmount { get; set; }
            public string? CBName { get; set; }
            public string? BAccNo { get; set; }
            public string? AccName { get; set; }
            public int? PID { get; set; }
            public string Narration { get; set; }
            public  string Type { get; set; }
            public string postingBank { get; set; }
            public string Tran_Type { get; set; }
        }
    }
}
