
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Globalization;
using Inspire.Erp.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace Inspire.Erp.Infrastructure.CustomExceptionHandler
    {
        public class CustomExceptionHandlerMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger _logger;
        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
            {
                _next = next;
               _logger = logger;
            }
            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception error)
                {
                    var response = context.Response;
                    response.ContentType = "application/json";
                    var responseModel = ApiResponse<string>.Fail(error.Message);
                    var exception = error.Message;
                    _logger.LogWarning(9999, "Inspire Erp Exception => {exception}", exception);
                    switch (error)
                        {
                            case SomeException e:
                                // custom application error
                                response.StatusCode = (int)HttpStatusCode.BadRequest;
                                break;
                            case KeyNotFoundException e:
                                // not found error
                                response.StatusCode = (int)HttpStatusCode.NotFound;
                                break;
                            default:
                                // unhandled error
                                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                break;
                        }
                    var result = JsonSerializer.Serialize(responseModel);
                    await response.WriteAsync(result);
                }
            }
        }
        public class SomeException : Exception
        {
            public SomeException() : base()
            {
            }
            public SomeException(string message) : base(message)
            {
            }
            public SomeException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
            {
            }
        }
    }


