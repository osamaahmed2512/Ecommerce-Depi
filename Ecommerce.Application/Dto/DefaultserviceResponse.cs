using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce.Application.Dto
{
    public class DefaultserviceResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Type { get; set; }
        public int StatusCode { get; set; }

        public static DefaultserviceResponse Fail(string? message, int statusCode) => new () { Success= false , Message= message, StatusCode =statusCode };
        public static DefaultserviceResponse Ok(string? message ) => new() { Success= true , Message= message};
    }
    public class DefaultServiceResponseWithData<T> : DefaultserviceResponse
    {
        public T? Data { get; set; }

        public static DefaultServiceResponseWithData<T> Ok(T? data = default, string? message = null, int statusCode = 200) =>
            new() { Success = true, Message = message, StatusCode = statusCode, Data = data };

        public static DefaultServiceResponseWithData<T> Fail(string? message = null, int statusCode = 400) =>
            new() { Success = false, Message = message, StatusCode = statusCode };
    }
}
