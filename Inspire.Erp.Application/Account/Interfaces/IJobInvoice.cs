using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Account.Interface
{
    public interface IJobInvoice
    {
        public IEnumerable<JobInvoice> Getjobinvoice();
        public JobInvoice GetJobInvoiceById(int? id);
        public JobInvoice InsertJobInvoice(JobInvoice JobInvoiceViewModel);
        public JobInvoice UpdateJobInvoice(JobInvoice JobInvoiceViewModel);
        public IEnumerable<JobInvoice> DeleteJobInvoice(string Id);
        public JobInvoice GetJobInvoiceVoucherNo(string? id);
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);

    }
}
