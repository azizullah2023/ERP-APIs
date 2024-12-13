using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class SubMenu
    {
        public int SubMenuId { get; set; }
        public int MainMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuRouterLink { get; set; }
        public string SubMenuIcon { get; set; }
    }
}
