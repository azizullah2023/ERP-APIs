using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class MenusPermissions
    {
        public int Id { get; set; }
        public int MainMenuId { get; set; }
        public int SubMenuId { get; set; }
        public bool IsMainMenuAllowed { get; set; }
        public bool IsSubMenuAllowed { get; set; }
        public int UserGroupId { get; set; }
    }
}
