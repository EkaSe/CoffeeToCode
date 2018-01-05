using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace C2C
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<SimpleMiddleware>();       

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello world!");
            });

            app.MapWhen(
                context => context.Request.Path.ToString().EndsWith(".report"),
                appBranch => {
                    appBranch.UseMyHandler();
                });

            app.UseDefaultFiles();            
        }        
    }
}
