using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OTP_API.Common
{
    //Generally I prefer avoiding static methods and use a class instance configured with appropriate
    //parameters passed to the user classes. But due to the scope constraints I use static methods
    //both here and also in other classes (Login, Registration)
    public class Storage
    {
        private static Dictionary<string, CacheEntry> cache = new Dictionary<string, CacheEntry>();

        public static void Set(string key, string data)
        {
            cache[key] = new CacheEntry(data, Data.TTL);
        }

        public static void Remove(string key)
        {
            cache.Remove(key);
        }

        public static string Get(string key) 
        {
            if ( !cache.ContainsKey(key) )
            {
                return null;
            }
            
            var entry = cache[key];

            //Here we remove expired entries from cache upon access.
            //This approach has some advantages and disadvantages and
            //based on the context and project requirements this can be 
            //a good or a bad choice.
            //Another option is to sweep the whole cache for expired entries upon 
            //each login operation (more computations, quick removal of expired entries).
            //But here I just remove the item when it is accessed (less computation, 
            //but more memory usage because we keep expired items until they are accessed)
            //If we want to do hard-expiration (expire password exactly after 30 seconds)
            //we need to have a background thread which checks and removes expired entries.
            if ( entry.ExpirationTime <= DateTime.Now )
            {
                Remove(key);
                return null;
            }

            return entry.Data;
        }

        public static void Clear() 
        {
            cache.Clear();
        }

        private class CacheEntry
        {
            public string Data;
            public DateTime ExpirationTime = DateTime.MinValue;

            public CacheEntry(string data, int ttl)
            {
                Data = data;
                ExpirationTime = DateTime.Now.AddSeconds(ttl);
            }
        }
    }
}

