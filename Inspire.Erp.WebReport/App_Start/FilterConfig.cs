﻿using System.Web;
using System.Web.Mvc;

namespace Inspire.Erp.WebReport
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
