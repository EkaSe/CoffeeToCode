using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace C2C
{
    public class ReadFromFileHtmlGenerator : HtmlGeneratorBase, IHtmlGenerator
    {
        public async Task ProduceHtml(HttpContext context)
        {
            var requestedFile = context.Request.QueryString.ToString().Substring(1);
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
            
            var contentType =  ExtensionContentType.Instance[Path.GetExtension(requestedFile)];

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
}