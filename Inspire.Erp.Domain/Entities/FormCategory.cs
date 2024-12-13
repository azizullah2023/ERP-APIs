using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class FormCategory
    {
        public int? FormCategoryId { get; set; }
        public string FormCategoryCategory { get; set; }
        public bool? FormCategoryDelStatus { get; set; }
    }
}
