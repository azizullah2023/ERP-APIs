using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models.Settings
{
    public class GetMenusResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ParentId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string RouteUrl { get; set; }
        public string IconClass { get; set; }
        public int? Order { get; set; }
        public string Page { get; set; }
        public List<GetMenusResponse> Items { get; set; }
    }


    public class GetPrimeMenusResponse
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Target { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public List<GetPrimeMenusResponse> Items { get; set; }
    }
}
