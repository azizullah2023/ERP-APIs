using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class DepartmentMasterService : IDepartmentMasterService
    {
        private IRepository<DepartmentMaster> departmentrepository;
        public DepartmentMasterService(IRepository<DepartmentMaster> _departmentrepository)
        {
            departmentrepository = _departmentrepository;
        }
        public IEnumerable<DepartmentMaster> InsertDepartment(DepartmentMaster departmentMaster)
        {
            bool valid = false;
            try
            {
                valid = true;
            
                int mxc = Convert.ToInt32(departmentrepository.GetAsQueryable()
                                        .Where(x => x.DepartmentMasterDepartmentId > 0)
                                        .DefaultIfEmpty()
                                        .Max(o => o == null ? 0 : o.DepartmentMasterDepartmentId)) + 1;

                departmentMaster.DepartmentMasterDepartmentId = mxc;
                departmentrepository.Insert(departmentMaster);
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
            return this.GetAllDepartment();
        }
        public IEnumerable<DepartmentMaster> UpdateDepartment(DepartmentMaster departmentMaster)
        {
            bool valid = false;
            try
            {
                departmentrepository.Update(departmentMaster);
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
            return this.GetAllDepartment();
        }
        public IEnumerable<DepartmentMaster> DeleteDepartment(DepartmentMaster departmentMaster)
        {
            bool valid = false;
            try
            {
                departmentrepository.Delete(departmentMaster);
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
            return this.GetAllDepartment();
        }

        public IEnumerable<DepartmentMaster> GetAllDepartment()
        {
            IEnumerable<DepartmentMaster> departmentMasters;
            try
            {
                departmentMasters = departmentrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return departmentMasters;
        }
        public IEnumerable<DepartmentMaster> GetAllDepartmentById(int id)
        {
            IEnumerable<DepartmentMaster> departmentMasters;
            try
            {
                departmentMasters = departmentrepository.GetAsQueryable().Where(k => k.DepartmentMasterDepartmentId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return departmentMasters;

        }

    }
}
