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
using C2C.Handlers;
using C2C.HtmlGenerators;

namespace C2C
{
    public class Middleware
    {
        private readonly RequestDelegate _next;

        private readonly IHtmlGenerator htmlGenerator 
            //= new ReadFromFileHtmlGenerator();
            //= new ConsoleStreamHtmlGenerator(); 
            //= new RuntimeCompilatorHtmlGenerator();
            = new RazorLikeHtmlGenerator();   

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) 
        {
            Utils.WriteToFile(context.Request.ToLog(), fileName, appendLog());
            await htmlGenerator.ProduceHtml(context);
            Utils.WriteToFile(context.Response.ToLog(), fileName, appendLog());            
        }

        private static string fileName => "requestLog";
        
        private static bool firstTime = true;
        private static bool appendLog()
        {
            if (firstTime)
            {
                firstTime = false;
                return false;
            }
            return true;
        }
    }

    internal static class ContextLogger
    {
        public static string ToLog(this HttpRequest request)
        {
            return $"\nPath: {request.Path}\n" +
                $"Method: {request.Method}\n" +
                $"QueryString: {request.QueryString}\n" +
                $"ContentType: {request.ContentType}\n";
        }   

        public static string ToLog (this HttpResponse response)
        {
            return $"Status code: {response.StatusCode}\n" +
                $"Content type: {response.ContentType}\n";
        } 
    }
}