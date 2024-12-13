using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
	public class PDCFilters
	{
		public DateTime? postDate { get; set; }
		public DateTime? dateFrom { get; set; }
		public DateTime? dateTo { get; set; }
		public string? pdcType { get; set; }
		public string? searchType { get; set; }
		public List<string>? keyWords { get; set; }
	}
}
