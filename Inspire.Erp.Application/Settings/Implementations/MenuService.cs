using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.MIS.Interfaces;
using Inspire.Erp.Application.Settings.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Settings;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Inspire.Erp.Domain.Models.Settings;
using System.Data;

namespace Inspire.Erp.Application.Settings.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly IRepository<Menu> _menu;
        private readonly IUtilityService _utilityService;
        public MenuService(IUtilityService utilityService, IRepository<Menu> menu)
        {
            _utilityService = utilityService;
            _menu = menu;
        }


        private List<GetPrimeMenusResponse> AddChildMenuItems(List<GetMenusResponse> allItems, GetMenusResponse parentMenu)
        {
            var childs = allItems.Where(y => y.ParentId == parentMenu.Id).OrderBy(x => x.Order).ToList();
            List<GetPrimeMenusResponse> subChilds = new List<GetPrimeMenusResponse>();
            foreach (GetMenusResponse child in childs)
            {
                var item = new GetPrimeMenusResponse()
                {
                    Target = child.Action,
                    Icon = child.IconClass,
                    Id = child.Id,
                    Url = child.RouteUrl,
                    Title = child.Title,
                    Label = child.Title,
                };
                item.Items = new List<GetPrimeMenusResponse>();
                item.Items = this.AddChildMenuItems(allItems, child).ToList();
                subChilds.Add(item);
            }
            return subChilds;
        }
        public async Task<Response<List<GetPrimeMenusResponse>>> GetMenusByWorkgroupId(int id)
        {
            try
            {
                List<GetMenusResponse> model = new List<GetMenusResponse>();
                List<GetPrimeMenusResponse> menus = new List<GetPrimeMenusResponse>();
                
                var MenuList = await _menu.GetAsQueryable().Where(a => a.IsDeleted != true)
                    .Include(x => x.WorkGroupPermissions)
                    .Where(a => a.WorkGroupPermissions.Any(wp => wp.WorkGroupPermissionsWorkGroupId == id && wp.WorkGroupPermissionsUallow == true))
                    .ToListAsync();
                //Include(x => x.MenuWorkGroupPermissions).Where(x => x.MenuWorkGroupPermissions.Where(c => c.WorkGroupPermissionsWorkGroupId == id).Count() > 0).ToListAsync();

                if (MenuList.Count > 0)
                {
                    var topMenu = MenuList.Where(x => x.ParentId == 0).Select(x => new GetMenusResponse()
                    {
                        Action = x.Action,
                        Controller = x.Controller,
                        CreatedAt = x.CreatedAt,
                        IconClass = x.IconClass,
                        Id = x.Id,
                        IsDeleted = x.IsDeleted,
                        ParentId = x.ParentId,
                        RouteUrl = x.RouteUrl,
                        Title = x.Title,
                        Page = x.Page,
                        Order = x.Order,
                    }).OrderBy(x => x.Order).ToList();

                    var allItems = MenuList.Where(x => x.ParentId != 0).Select(x => new GetMenusResponse()
                    {
                        Action = x.Action,
                        Controller = x.Controller,
                        CreatedAt = x.CreatedAt,
                        IconClass = x.IconClass,
                        Id = x.Id,
                        IsDeleted = x.IsDeleted,
                        ParentId = x.ParentId,
                        RouteUrl = x.RouteUrl,
                        Title = x.Title,
                        Page = x.Page,
                        Order = x.Order,
                    }).OrderBy(x => x.Order).ToList();


                    foreach (var pageItem in topMenu)
                    {
                        var item = new GetPrimeMenusResponse()
                        {
                            Target = pageItem.Action,
                            Icon = pageItem.IconClass,
                            Id = pageItem.Id,
                            Url = pageItem.RouteUrl,
                            Title = pageItem.Title,
                            Label = pageItem.Title,
                        };


                        item.Items = new List<GetPrimeMenusResponse>();
                        item.Items = this.AddChildMenuItems(allItems, pageItem);
                        menus.Add(item);
                    }
                }

                return Response<List<GetPrimeMenusResponse>>.Success(menus, "Records FOund.");


            }
            catch (Exception ex)
            {
                return Response<List<GetPrimeMenusResponse>>.Fail(new List<GetPrimeMenusResponse>(), ex.Message);
            }

            //public IEnumerable<Location> GetChild(int id)
            //{
            //    DBEntities db = new DBEntities();
            //    var locations = db.Locations.Where(x => x.ParentLocationID == id || x.LocationID == id).ToList();

            //    var child = locations.AsEnumerable().Union(
            //                                db.Locations.AsEnumerable().Where(x => x.ParentLocationID == id).SelectMany(y => GetChild(y.LocationId))).ToList();
            //    return child;
            //}
        }

        public async Task<Response<Menu>> SaveMenu(Menu menu)
        {
            _menu.Insert(menu);
            return new Response<Menu>
            {
                //Result = this.GetMenuById(menu.Id),
                Result = menu,
                Message = "Menu Save Successfuly",
                Valid = true,

            };
        }

        public async Task<Response<Menu>> GetMenuById(int id)
        {
            var menu = _menu.GetAsQueryable().Where(m => m.Id == id).FirstOrDefault();
            return new Response<Menu>
            {
                Result = menu,
                Message = "",
                Valid = true,
            };
        }

        public async Task<Response<Menu>> DeleteMenu(int id)
        {
            var menu = _menu.GetAsQueryable().Where(m => m.Id == id).FirstOrDefault();
            menu.IsDeleted = true;
            _menu.Update(menu);
            return new Response<Menu>
            {
                Result = menu,
                Message = "Menu Delete Successfuly",
                Valid = true,
            };
        }

        public async Task<Response<Menu>> UpdateMenu(Menu menu)
        {
            _menu.Update(menu);
            return new Response<Menu>
            {
                // Result = this.GetMenuById(menu.Id),
                Result = menu,
                Message = "Menu Update Successfuly",
                Valid = true,
            };
        }

        public async Task<Response<List<Menu>>> MenuList()
        {
            return new Response<List<Menu>>
            {
                Result = _menu.GetAll().Where(a => a.IsDeleted != true).ToList(),
                Valid = true,
                Message = "Data Found"
            };
        }
    }
}
