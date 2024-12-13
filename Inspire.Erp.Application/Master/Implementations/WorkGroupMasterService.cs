
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class WorkGroupMasterService : IWorkGroupMasterService
    {
        private IRepository<WorkGroupMaster> workGrouprepository;
        private readonly IRepository<Menu> _menu;
        private IRepository<WorkGroupPermissions> _groupPermissions;
        private IRepository<WorkGroupMenuDetail> _menuDetail;
        public WorkGroupMasterService(IRepository<WorkGroupMaster> _workGrouprepository, IRepository<Menu> menu, IRepository<WorkGroupPermissions> groupPermissions, IRepository<WorkGroupMenuDetail> menuDetail)
        {
            workGrouprepository = _workGrouprepository;
            _menu = menu;
            _groupPermissions = groupPermissions;
            _menuDetail = menuDetail;
        }
        public IEnumerable<WorkGroupMaster> InsertWorkGroup(WorkGroupMaster workGroupMaster)
        {
            bool valid = false;
            try
            {
                workGrouprepository.BeginTransaction();
                // Insert the new workgroup
                workGrouprepository.Insert(workGroupMaster);
                var menulist = _menu.GetAll().Where(a => a.IsDeleted == false).Select(c => c.Id).ToList();

                // Iterate over each menu in the menu list
                foreach (var menu in menulist)
                {
                    // Insert menu detail for the workgroup
                    //WorkGroupMenuDetail detail = new WorkGroupMenuDetail
                    //{
                    //    MenuId = menu,
                    //    WorkgroupId = workGroupMaster.WorkGroupMasterWorkGroupId
                    //};
                    //_menuDetail.Insert(detail);

                    // Insert permissions for the workgroup
                    WorkGroupPermissions perm = new WorkGroupPermissions
                    {
                        WorkGroupPermissionsMenuId = menu,
                        WorkGroupPermissionsWorkGroupId = workGroupMaster.WorkGroupMasterWorkGroupId,
                        WorkGroupPermissionsUadd = true,
                        WorkGroupPermissionsUallow = true,
                        WorkGroupPermissionsUedit = true,
                        WorkGroupPermissionsDelStatus = false,
                        WorkGroupPermissionsUdelete = true,
                        WorkGroupPermissionsUview = true,
                        WorkGroupPermissionsUprint = true,
                        WorkGroupPermissionsFormType = ""
                    };
                    _groupPermissions.Insert(perm);
                }

                // Commit the transaction
                workGrouprepository.TransactionCommit();
            }
            catch (Exception ex)
            {
                valid = false;
                workGrouprepository.TransactionRollback();
                throw ex;

            }
            finally
            {
                //workGrouprepository.Dispose();
            }
            return this.GetAllWorkGroupById(workGroupMaster.WorkGroupMasterWorkGroupId);
        }
        public IEnumerable<WorkGroupMaster> UpdateWorkGroup(WorkGroupMaster workGroupMaster)
        {
            bool valid = false;
            try
            {
                workGrouprepository.Update(workGroupMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //WorkGrouprepository.Dispose();
            }
            return this.GetAllWorkGroupById(workGroupMaster.WorkGroupMasterWorkGroupId);
        }
        public IEnumerable<WorkGroupMaster> DeleteWorkGroup(WorkGroupMaster workGroupMaster)
        {
            bool valid = false;
            try
            {
                workGrouprepository.Delete(workGroupMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //workGrouprepository.Dispose();
            }
            return this.GetAllWorkGroup();
        }

        public IEnumerable<WorkGroupMaster> GetAllWorkGroup()
        {
            IEnumerable<WorkGroupMaster> workGroupMasters;
            try
            {
                workGroupMasters = workGrouprepository.GetAll().Where(a => a.WorkGroupMasterWorkGroupDelStatus != true);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //workGrouprepository.Dispose();
            }
            return workGroupMasters;
        }
        public IEnumerable<WorkGroupMaster> GetAllWorkGroupById(int id)
        {
            IEnumerable<WorkGroupMaster> workGroupMasters;
            try
            {
                workGroupMasters = workGrouprepository.GetAsQueryable().Where(k => k.WorkGroupMasterWorkGroupId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return workGroupMasters;

        }

    }
}
