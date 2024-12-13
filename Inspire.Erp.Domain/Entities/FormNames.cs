using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class FormNames
    {
        public int? FormNamesId { get; set; }
        public string FormNamesName { get; set; }
        public string FormNamesAliasName { get; set; }
        public string FormNamesMenuName { get; set; }
        public bool? FormNamesIsVisible { get; set; }
        public bool? FormNamesIsMenuVisible { get; set; }
        public int? FormNamesCategoryId { get; set; }
        public bool? FormNamesDelStatus { get; set; }
    }
}
