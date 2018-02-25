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
            //= new RuntimeCompilatorHtmlGenerator();
            = new RazorLikeHtmlGenerator();   

        public SimpleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) 
        {
            WriteToFile(context.Request.ToLog());
            await htmlGenerator.ProduceHtml(context);
            WriteToFile(context.Response.ToLog());            
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
        
        private static void WriteToFile(string textToWrite)
        {
            using (StreamWriter file = new StreamWriter(fileName, appendLog()))
            {
                file.Write(textToWrite);
            }
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