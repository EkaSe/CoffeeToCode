using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace C2C
{
    public class ConsoleStreamHtmlGenerator : HtmlGeneratorBase, IHtmlGenerator
    {
        public async Task ProduceHtml(HttpContext context)
        {
            var request = base.Request(context);
            string resultText = "";
            string contentType = "text/plain";

            try 
            {
                resultText = base.ReadProcessConsoleOutput(base.GetProcess(
                    dllName: request, 
                    workingDirectory: "HtmlStream", 
                    captureConsoleOutput: true));            
                contentType = "text/HTML";
            }
            catch (Exception e)
            {
                resultText = e.Message;
            }

            await SendResponse(context, resultText, contentType);
        }
    }
}