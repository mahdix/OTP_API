using System;
using System.IO;
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
                    await context.Response.WriteAsync("procee reg");
                    });
        }

        private static void ProcessLogin(IApplicationBuilder app)
        {
            app.Run(async context =>
                    {
                    await context.Response.WriteAsync("process login");
                    });
        }
    }
}
