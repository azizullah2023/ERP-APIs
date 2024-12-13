using Inspire.Erp.Application.Settings.Interfaces;
using Inspire.Erp.Domain.DTO.WorkGropPermission;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Erp.Application.Settings.Implementations
{
    public class PermissionSettingService : IPermissionSettingService
    {

        private IRepository<WorkGroupPermissions> _groupPermissions;
        private IRepository<Menu> _menus;


        public PermissionSettingService(IRepository<WorkGroupPermissions> groupPermissions, IRepository<Menu> menus)
        {
            _groupPermissions = groupPermissions;
            _menus = menus;
            
        }
        public Response<List<WorkGroupPermissionsDto>> GetPermissionSetting(int? groupId)
        {
            List<WorkGroupPermissionsDto> permissionList = new List<WorkGroupPermissionsDto>();

            permissionList = (from wgp in _groupPermissions.GetAsQueryable().Where(x => x.WorkGroupPermissionsWorkGroupId == groupId)
                              join me in _menus.GetAsQueryable() on wgp.WorkGroupPermissionsMenuId equals me.Id into meGroup
                              from me in meGroup.DefaultIfEmpty()
                              select new WorkGroupPermissionsDto
                              {
                                  WorkGroupPermissionsSno=wgp.WorkGroupPermissionsSno,
                                  WorkGroupPermissionsMenuName = me.Title,
                                  WorkGroupPermissionsUadd = wgp.WorkGroupPermissionsUadd,
                                  WorkGroupPermissionsUallow = wgp.WorkGroupPermissionsUallow,
                                  WorkGroupPermissionsUdelete = wgp.WorkGroupPermissionsUdelete,
                                  WorkGroupPermissionsUview =   wgp.WorkGroupPermissionsUview,
                                  WorkGroupPermissionsUprint = wgp.WorkGroupPermissionsUprint,
                                  WorkGroupPermissionsUedit = wgp.WorkGroupPermissionsUedit,
                                  WorkGroupPermissionsMenuId = wgp.WorkGroupPermissionsMenuId,
                                  WorkGroupPermissionsWorkGroupId = wgp.WorkGroupPermissionsWorkGroupId,
                                 

                              }).ToList();

            return new Response<List<WorkGroupPermissionsDto>>
            { 
              Valid = true,
              Result = permissionList,
              Message = "Permission Data Found"
            };
        }

        public async Task<Response<List<WorkGroupPermissionsDto>>> UpdatePermissionSetting(List<WorkGroupPermissions> permissionSetting)
        {
            
             _groupPermissions.UpdateList(permissionSetting);
           
            return new Response<List<WorkGroupPermissionsDto>>
            {
                Result = this.GetPermissionSetting(permissionSetting[0].WorkGroupPermissionsWorkGroupId).Result,
                Valid = true,
                Message = "saved Successfully"
            };
                 
        }
    }
}
