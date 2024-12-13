using System;

namespace Inspire.Erp.Domain.Entities
{
    public class OrderApprovals
    {
        public int Id { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherType { get; set; }
        public DateTime ApprovelDate { get; set; }
        public DateTime ApprovelTime { get; set; }
        public int ApprovedBy { get; set; }
        public int IsAllowed { get; set; }
        public int Level1 { get; set; }
        public int Level2 { get; set; }
        public int Level3 { get; set; }
        public int UserId { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

    }
}
