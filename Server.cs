using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OTP_API
{
    public class Server
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {            
            app.Map("/register", ProcessRegistration);
            app.Map("/login", ProcessLogin);
        }

        private static void ProcessRegistration(IApplicationBuilder app)
        {
            app.Run(async context =>
                    {
                        await innerProcessRegistration(context);
                    });
        }

        private static void ProcessLogin(IApplicationBuilder app)
        {
            app.Run(async context =>
                    {
                        await innerProcessLogin(context);
                    });
        }

        private static Task innerProcessRegistration(HttpContext context) {
            return context.Response.WriteAsync("process regg");
        }

        private static Task innerProcessLogin(HttpContext context) {
            return context.Response.WriteAsync("process login");
        }
    }
}
