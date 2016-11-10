using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OTP_API
{
    public class Registration
    {
        public static Task Process(HttpContext context) {
            string userName = context.Request.Query["user"]; 
            string otp = GenerateOneTimePassword(userName);
            Storage.Set(userName, otp);

            return context.Response.WriteAsync(otp);
        }

        private static string GenerateOneTimePassword(string user) 
        {
            return Hash(user);
        }

        private static string Hash(string password)
        {
            using (var sha = SHA256.Create())
            {
                var computedHash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
                return BitConverter.ToString(computedHash).Replace("-","");
            }
        }
    }
}
