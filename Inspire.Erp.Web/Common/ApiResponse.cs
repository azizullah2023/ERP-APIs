using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Common
{
    public class ApiResponse<T>
    {
        public T Result { get; set; }
        public bool? Valid { get; set; }

        public bool? Error { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public static ApiResponse<T> Fail(string errorMessage, string exception="")
        {
            return new ApiResponse<T> { Valid = false, Message = errorMessage, Exception=exception };
        }
        public static ApiResponse<T> Success(T data, string messageText = "")
        {
            return new ApiResponse<T> { Valid = true, Result = data, Message = messageText };
        }
    }

    public class ApiMethod
    {
        public Http HttpType { get; set; }
        public string Url { get; set; }
        public object Body { get; set; }
        public string DocType { get; set; }

    }

    public enum Http
    {
        POST = 1,
        GET,
        PUT,
        DELETE

    }
}
