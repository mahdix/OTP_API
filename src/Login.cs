using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OTP_API.Common;

namespace OTP_API
{
    public class Login
    {
        //The main entry gor Login checking which is called from web server
        public static Task Process(HttpContext context) {
            //fetch provided user and password
            string userName = context.Request.Query["user"]; 
            string password = context.Request.Query["pass"]; 
            bool success = false;

            //fail fast if inputs are invalid
            if ( userName == null || password == null || userName.Length == 0 || password.Length == 0 )
            {
                success = false;
            } 
            else 
            {
                string storedPassword = Storage.Get(userName);
                success = (storedPassword == password);
            }

            if ( success )
            {
                return context.Response.WriteAsync("OK");
            }
            else
            {
                return context.Response.WriteAsync("FAIL");
            }
        }
    }
}

