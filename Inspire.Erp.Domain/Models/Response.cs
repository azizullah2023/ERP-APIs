using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
   public  class Response<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public bool Valid { get; set; }
        public static Response<T> Success(T data,string message)
        {
            return new Response<T>()
            {
                Valid = true,
                Message=message,
                Result = data,
                Status="Success"
            };
        }
        public static Response<T> Fail(T data, string message)
        {
            return new Response<T>()
            {
                Valid = false,
                Message = message,
                Result = data,
                Status = "Fail"
            };
        }

        
    }

}
