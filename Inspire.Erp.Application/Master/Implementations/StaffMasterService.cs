using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class StaffMasterService : IStaffMasterService
    {
        private IRepository<StaffMaster> staffrepository;
        public StaffMasterService(IRepository<StaffMaster> _staffrepository)
        {
            staffrepository = _staffrepository;
        }
        public IEnumerable<StaffMaster> InsertStaff(StaffMaster staffMaster)
        {
            bool valid = false;
            try
            {
                valid = true;
                int mxc = 0;
                mxc = staffrepository.GetAsQueryable().Where(k => k.StaffMasterStaffId != null).Select(x => x.StaffMasterStaffId).Max();
                if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }

                staffMaster.StaffMasterStaffId = mxc;
                staffrepository.Insert(staffMaster);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //staffrepository.Dispose();
            }
            return this.GetAllStaff();
        }
        public IEnumerable<StaffMaster> UpdateStaff(StaffMaster staffMaster)
        {
            bool valid = false;
            try
            {
                staffrepository.Update(staffMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //staffrepository.Dispose();
            }
            return this.GetAllStaff();
        }
        public IEnumerable<StaffMaster> DeleteStaff(StaffMaster staffMaster)
        {
            bool valid = false;
            try
            {
                staffrepository.Delete(staffMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //staffrepository.Dispose();
            }
            return this.GetAllStaff();
        }

        public IEnumerable<StaffMaster> GetAllStaff()
        {
            IEnumerable<StaffMaster> staffMasters;
            try
            {
                staffMasters = staffrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //staffrepository.Dispose();
            }
            return staffMasters;
        }
        public IEnumerable<StaffMaster> GetAllStaffById(int id)
        {
            IEnumerable<StaffMaster> staffMasters;
            try
            {
                staffMasters = staffrepository.GetAsQueryable().Where(k => k.StaffMasterStaffId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return staffMasters;

        }
    }
}
