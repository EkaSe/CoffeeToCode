using Microsoft.AspNet.Builder;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Net;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace C2C
{
    public class SimpleMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IHtmlGenerator htmlGenerator 
            //= new ReadFromFileHtmlGenerator();
            //= new ConsoleStreamHtmlGenerator(); 
            = new RuntimeCompilatorHtmlGenerator();   

        private static ExtensionContentType extensionContentTypeMap = new ExtensionContentType();

        public SimpleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) => await htmlGenerator.ProduceHtml(context);      

    }
}