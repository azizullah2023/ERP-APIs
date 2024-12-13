using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models
{
    public class ApiResponse<T>
    {
        public T Result { get; set; }
        public bool? Valid { get; set; }

        public bool? Error { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public static ApiResponse<T> Fail(string errorMessage)
        {
            return new ApiResponse<T> { Valid = false, Message = errorMessage };
        }
        public static ApiResponse<T> Success(T data, string messageText="")
        {
            return new ApiResponse<T> { Valid = true, Result = data, Message = messageText };
        }
    }
}
