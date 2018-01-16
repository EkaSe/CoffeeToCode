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

        private static ExtensionContentTypeMap extensionContentTypeMap = new ExtensionContentTypeMap();

        public SimpleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await GeneratePage(context, context.Request.QueryString.ToString().Substring(1));
        }

        private static async Task GeneratePage(HttpContext context, string requestedFile)
        {
            string resultText = "";
            var fileGeneratorFound = false;
            var requestedFileName = requestedFile
                .Split('.', new StringSplitOptions())
                .SkipLast(1)
                .Aggregate((item, next) => String.Concat(item, next));

            try 
            {
                GenerateHtml(requestedFileName);
                fileGeneratorFound = true;
            }
            catch (Exception e)
            {
                resultText = e.Message;
            }
            
            var file = new FileInfo($"wwwroot/{requestedFile}");
            
            var contentType = extensionContentTypeMap[Path.GetExtension(requestedFile)];

            byte[] buffer;
            if (file.Exists)
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = contentType;
                if (contentType == "text/HTML")
                {
                    var htmlText = File.ReadAllText(file.FullName);
                    var appendedHtml = new StringBuilder(htmlText)
                        .Insert(
                            htmlText.IndexOf("</body>"),  
                            $"<div>Requested file is {requestedFile}</div>")
                        .ToString();

                    buffer = Encoding.UTF8.GetBytes(fileGeneratorFound ? htmlText : appendedHtml);
                }
                else if (contentType == "text/plain")
                {
                    buffer = Encoding.UTF8.GetBytes(
                        $"{File.ReadAllText(file.FullName)}\nRequested file is {requestedFile}");
                }
                else 
                {
                    buffer = File.ReadAllBytes(file.FullName);
                }
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "text/plain";
                buffer = Encoding.UTF8.GetBytes($"Unable to find the requested file {requestedFile}");
            }

            using (var stream = context.Response.Body)
            {
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.FlushAsync();
            }

            context.Response.ContentLength = buffer.Length;
        }

        private static void GenerateHtml(string htmlName)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = "HtmlGen",
                    FileName = "dotnet",
                    Arguments = $"{htmlName}.dll",
                    UseShellExecute = true,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
        }
    }

    internal class ExtensionContentTypeMap
    {
        private static Dictionary<string, string> types;
        private Dictionary<string, string> Types()
        {
            if (types is null)
            {
                types = new Dictionary<string, string>();
                types.Add(".html", "text/HTML");
                types.Add(".css", "text/plain");
                types.Add(".js", "text/plain");
                types.Add(".jpeg", "image/JPEG");
                types.Add(".jpg", "image/JPEG");
            }
            return types;
        }

        public string this[string extension] { get => Types().ContainsKey(extension) ? Types()[extension] : "text/plain"; }
    }
}