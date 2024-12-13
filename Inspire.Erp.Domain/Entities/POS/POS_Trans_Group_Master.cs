namespace Inspire.Erp.Domain.Entities.POS
{
    public class POS_Trans_Group_Master
    {
        public int Id { get; set; }
        public string Trans_GroupName { get; set; }

    }
    public class POS_TransactionCodes
    {

        public int Id { get; set; }
        public string TransCode { get; set; }
        public string TransDescription { get; set; }
        public int TransGroup { get; set; }
        public string MainGroup { get; set; }
        public string CRDR { get; set; }
        public bool? Taxable { get; set; }
        public bool? ManualTransaction { get; set; }
        public string AccCode { get; set; }
        public string InvCode { get; set; }
        public string PrefixCode { get; set; }
        public string Refcodefield { get; set; }
        public string Reportfilename { get; set; }

        public bool Printvoucher { get; set; }

        public bool PosttoAccounts { get; set; }

        public float Taxsvc { get; set; }

        public float Tax { get; set; }

        public float Svc { get; set; }

        public int? Guestledger { get; set; }

        public int? Dailysummary { get; set; }

        public bool? InHouse { get; set; }

        public int? DeptId { get; set; }

        public int? SortOrder { get; set; }

        public bool? ShowInInv { get; set; }
        public bool? FilterBillSummHead { get; set; }
    }
}


