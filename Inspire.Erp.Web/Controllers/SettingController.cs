using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.Settings.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public SettingController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        #region MENU 
        [HttpPost("GetMenusByWorkgroupId")]
        public async Task<IActionResult> GetMenusByWorkgroupId(int id)
        {
            return Ok(await _menuService.GetMenusByWorkgroupId(id));
        }

        [HttpGet("GetAllMenuList")]
        public async Task<IActionResult> MenuList()
        {
            return Ok(await _menuService.MenuList());
        }

        [HttpPost("InsertMenu")]
        public async Task<IActionResult> SaveMenu([FromBody] Menu menu)
        {
            return Ok(await _menuService.SaveMenu(menu));
        }
        [HttpPut("UpdateMenu")]
        public async Task<IActionResult> UpdateMenu([FromBody] Menu menu)
        {
            return Ok(await _menuService.UpdateMenu(menu));
        }
        [HttpPost("DeleteMenu")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            return Ok(await _menuService.DeleteMenu(id));
        }


        #endregion
    }
}
