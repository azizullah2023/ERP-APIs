using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Models.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Settings.Interfaces
{
    public interface IMenuService
    {
        Task<Response<List<GetPrimeMenusResponse>>> GetMenusByWorkgroupId(int id);
        Task<Response<Menu>> SaveMenu(Menu menu);
        Task<Response<Menu>> DeleteMenu(int id);
        Task<Response<Menu>> UpdateMenu(Menu menu);
        Task<Response<Menu>> GetMenuById(int id);
        Task<Response<List<Menu>>> MenuList();
    }
}
