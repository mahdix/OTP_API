using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OTP_API
{
    public class Login
    {
        public static Task Process(HttpContext context) {
            string userName = context.Request.Query["user"]; 
            string password = context.Request.Query["pass"]; 

            string storedPassword = Storage.Get(userName);
            bool success = (storedPassword == password);

            return context.Response.WriteAsync(success ? "OK":"FAIL");
        }
    }
}

