using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Store.Interface
{
    public interface IvoucherPrinting
    {
        // public IEnumerable<Inspire.Erp.Domain.Modals.VoucherPrintingResponse> VoucherPrintingResponse(VoucherPrintingRequest obj);
        public Task<string> VoucherPrintingRequest(VoucherPrintingRequest obj);

    }
}
