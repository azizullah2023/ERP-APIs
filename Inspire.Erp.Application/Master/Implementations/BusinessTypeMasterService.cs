using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class BusinessTypeMasterService : IBusinessTypeMasterService
    {
        private IRepository<BusinessTypeMaster> businesstyperepository;
        public BusinessTypeMasterService(IRepository<BusinessTypeMaster> _countryrepository)
        {
            businesstyperepository = _countryrepository;
        }
        public IEnumerable<BusinessTypeMaster> InsertBusinessType(BusinessTypeMaster BusinessTypeMaster)
        {
            bool valid = false;
            try
            {
                valid = true;
                int mxc = 0;
                mxc = (int)businesstyperepository.GetAsQueryable().Where(k => k.BusinessTypeMasterBusinessTypeId != null).Select(x => x.BusinessTypeMasterBusinessTypeId).Max();
                if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }

                BusinessTypeMaster.BusinessTypeMasterBusinessTypeId = mxc;

                businesstyperepository.Insert(BusinessTypeMaster);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //businesstyperepository.Dispose();
            }
            return this.GetAllBusinessType();
        }
        public IEnumerable<BusinessTypeMaster> UpdateBusinessType(BusinessTypeMaster BusinessTypeMaster)
        {
            bool valid = false;
            try
            {
                businesstyperepository.Update(BusinessTypeMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //businesstyperepository.Dispose();
            }
            return this.GetAllBusinessType();
        }
        public IEnumerable<BusinessTypeMaster> DeleteBusinessType(BusinessTypeMaster BusinessTypeMaster)
        {
            bool valid = false;
            try
            {
                businesstyperepository.Delete(BusinessTypeMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //businesstyperepository.Dispose();
            }
            return this.GetAllBusinessType();
        }

        public IEnumerable<BusinessTypeMaster> GetAllBusinessType()
        {
            IEnumerable<BusinessTypeMaster> businesstypeMasters;
            try
            {
                businesstypeMasters = businesstyperepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //businesstyperepository.Dispose();
            }
            return businesstypeMasters;
        }
        public IEnumerable<BusinessTypeMaster> GetAllBusinessTypeById(int id)
        {
            IEnumerable<BusinessTypeMaster> businesstypeMasters;
            try
            {
                businesstypeMasters = businesstyperepository.GetAsQueryable().Where(k => k.BusinessTypeMasterBusinessTypeId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return businesstypeMasters;

        }

    }
}
