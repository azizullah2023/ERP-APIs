using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Procurement;
using Inspire.Erp.Domain.Modals;

namespace Inspire.Erp.Application.Master.Interfaces
{
    public interface IProgressiveInvoiceService
    {
     public ProgressiveInvoice InsertProgressInvoice(ProgressiveInvoice progressiveInvoice,List<AccountsTransactions> accountsTransactions);
    public ProgressiveInvoice UpdateProgressInvoice(ProgressiveInvoice progressiveInvoice, List<AccountsTransactions> accountsTransactions);
    public int DeleteProgressInvoice(ProgressiveInvoice progressiveInvoice, List<AccountsTransactions> accountsTransactions);
     public IQueryable GetProgressInvoiceReport();
    public ProgressiveInvoice GetSavedProgressiveInoices(string id);

    }
}
  
