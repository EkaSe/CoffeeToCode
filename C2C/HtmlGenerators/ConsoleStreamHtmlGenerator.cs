using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace C2C.HtmlGenerators
{
    public class ConsoleStreamHtmlGenerator : IHtmlGenerator
    {
        public async Task ProduceHtml(HttpContext context)
        {
            var request = context.Request?.QueryString.ToString()?.Substring(1);
            string resultText = "";
            string contentType = "text/plain";

            try 
            {
                resultText = Utils.ReadProcessConsoleOutput(
                    Utils.GetProcess(
                        dllName: request, 
                        workingDirectory: "HtmlStream", 
                        captureConsoleOutput: true));            
                contentType = "text/HTML";
            }
            catch (Exception e)
            {
                resultText = e.Message;
            }

            await Utils.SendResponse(context, resultText, contentType);
        }
    }
}