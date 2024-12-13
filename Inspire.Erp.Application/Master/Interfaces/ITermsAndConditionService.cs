using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface ITermsAndConditionService
    {
        public IEnumerable<TermsAndCondition> InsertTerms(TermsAndCondition termsAndCondition);
        public IEnumerable<TermsAndCondition> UpdateTerms(TermsAndCondition termsAndCondition);
        public IEnumerable<TermsAndCondition> DeleteTerms(TermsAndCondition termsAndCondition);
        public IEnumerable<TermsAndCondition> GetAllTerms();
        public IEnumerable<TermsAndCondition> GetAllTermsById(int id);
    }
}
