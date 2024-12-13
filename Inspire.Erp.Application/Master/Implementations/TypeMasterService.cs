using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class TypeMasterService : ITypeMasterService
    {
        private IRepository<TypeMaster> typeMasterrepository;
        public TypeMasterService(IRepository<TypeMaster> _typeMasterrepository)
        {
            typeMasterrepository = _typeMasterrepository;
        }
        public IEnumerable<TypeMaster> InsertTypeMast(TypeMaster typeMaster)
        {
            bool valid = false;
            try
            {
                valid = true;
                int mxc = 0;
                mxc = (int)typeMasterrepository.GetAsQueryable().Where(k => k.TypeMasterTypeId != null).Select(x => x.TypeMasterTypeId).Max();
                if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }

                typeMaster.TypeMasterTypeId = mxc;
                typeMasterrepository.Insert(typeMaster);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllTypeMast();
        }
        public IEnumerable<TypeMaster> UpdateTypeMast(TypeMaster typeMaster)
        {
            bool valid = false;
            try
            {
                typeMasterrepository.Update(typeMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllTypeMast();
        }
        public IEnumerable<TypeMaster> DeleteTypeMast(TypeMaster typeMaster)
        {
            bool valid = false;
            try
            {
                typeMasterrepository.Delete(typeMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllTypeMast();
        }

        public IEnumerable<TypeMaster> GetAllTypeMast()
        {
            IEnumerable<TypeMaster> typeMaster;
            try
            {
                typeMaster = typeMasterrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return typeMaster;
        }

        public IEnumerable<TypeMaster> GetAllTypeMastById(int id)
        {
            IEnumerable<TypeMaster> typeMaster;
            try
            {
                typeMaster = typeMasterrepository.GetAsQueryable().Where(k => k.TypeMasterTypeId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return typeMaster;

        }

    }
}
