using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OTP_API
{
    public class Registration
    {
        public static Task Process(HttpContext context) {
            string userName = context.Request.Query["user"]; 
            return context.Response.WriteAsync("process regg for " + context.Request.Query["user"]);
        }
    }
}