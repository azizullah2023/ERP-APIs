using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class TermsAndConditionService: ITermsAndConditionService
    {
        private IRepository<TermsAndCondition> termsrepository;
        public TermsAndConditionService(IRepository<TermsAndCondition> _termsrepository)
        {
            termsrepository = _termsrepository;
        }
        public IEnumerable<TermsAndCondition> InsertTerms(TermsAndCondition termsAndCondition)
        {
            bool valid = false;
            try
            {
                valid = true;
                ////int mxc = 0;
                ////mxc = Convert.ToInt32( termsrepository.GetAsQueryable().Where(k => k.TermsAndConditionTermsId != null).Select(x => x.TermsAndConditionTermsId).Max());
                ////if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }

                int vnoMaxVal = Convert.ToInt32(termsrepository.GetAsQueryable()
                                                        .Where(x => x.TermsAndConditionTermsId > 0)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.TermsAndConditionTermsId)) + 1;


                termsAndCondition.TermsAndConditionTermsId = vnoMaxVal;
                termsrepository.Insert(termsAndCondition);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //termsrepository.Dispose();
            }
            return this.GetAllTerms();
        }
        public IEnumerable<TermsAndCondition> UpdateTerms(TermsAndCondition termsAndCondition)
        {
            bool valid = false;
            try
            {
                termsrepository.Update(termsAndCondition);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //termsrepository.Dispose();
            }
            return this.GetAllTerms();
        }
        public IEnumerable<TermsAndCondition> DeleteTerms(TermsAndCondition termsAndCondition)
        {
            bool valid = false;
            try
            {
                termsrepository.Delete(termsAndCondition);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //termsrepository.Dispose();
            }
            return this.GetAllTerms();
        }
        public IEnumerable<TermsAndCondition> GetAllTerms()
        {
            IEnumerable<TermsAndCondition> termsAndConditions;
            try
            {
                termsAndConditions = termsrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //termsrepository.Dispose();
            }
            return termsAndConditions;
        }
        public IEnumerable<TermsAndCondition> GetAllTermsById(int id)
        {
            IEnumerable<TermsAndCondition> termsAndConditions;
            try
            {
                termsAndConditions = termsrepository.GetAsQueryable().Where(k => k.TermsAndConditionTermsId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return termsAndConditions;

        }

    }
}
    
