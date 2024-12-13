using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Inspire.Erp.Domain.Modals.PDCResponse;

namespace Inspire.Erp.Application.Account.Interface
{
    public interface IPDC
    {
        public Task<IEnumerable<PDCPostedList>> PDCPostedReturnList(string chequeSatus);
        public Task<IEnumerable<PDCPostedList>> PDCReturnLists();
        public Task<IEnumerable<PDCGetList>> PDCGetList();
        public Task<IEnumerable<PDCGetList>> getFilteredPDCList(PDCFilters obj);
        public Task<IEnumerable<PDCGetList>> PostPDC(List<PDCGetList> list);

        public Task<List<BankNamesModel>> GetBankNames();
        Task<IEnumerable<PDCGetList>> PDCGetReturnList(PDCFilters obj);
    }
}
