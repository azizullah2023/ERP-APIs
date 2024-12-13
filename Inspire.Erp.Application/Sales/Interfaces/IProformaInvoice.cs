using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Sales.Interface
{
    public interface IProformaInvoice
    {
        public IEnumerable<ProformaInvoice> GetProformaInvoices();
        public ProformaInvoice InsertProformaInvoice(ProformaInvoice proformaInvoice);
        public ProformaInvoice UpdateProformaInvoice(ProformaInvoice proformaInvoice);
        public IEnumerable<ProformaInvoice> DeleteProformaInvoice(int Id);
        public ProformaInvoice GetProformaInvoiceById(int? id);
    }
}
