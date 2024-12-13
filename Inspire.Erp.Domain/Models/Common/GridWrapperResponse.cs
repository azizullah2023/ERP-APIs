using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Common
{
    public class GridWrapperResponse<T>
    {
        public int Total { get; set; }
        public T Data { get; set; }
    }
}
