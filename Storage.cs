using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OTP_API
{
    public class Storage
    {
        private static Dictionary<string,string> cache = new Dictionary<string, string>();

        public static void Set(string key, string data)
        {
            cache[key] = data;
        }

        public static string Get(string key) 
        {
            return cache[key];
        }
    }
}

