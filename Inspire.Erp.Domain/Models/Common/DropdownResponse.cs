using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Common
{
    public class DropdownResponse
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
    }
    public class ResponseInfo
    {
        public ResponseInfo() { IsSuccess = true; }
        public bool IsSuccess { get; set; }
        public object ResultSet { get; set; }
        public string FilePath { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public object data { get; set; }
    }
}
