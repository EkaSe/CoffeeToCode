using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace C2C.HtmlGenerators
{
    public class RuntimeCompilatorHtmlGenerator : IHtmlGenerator
    {
        public async Task ProduceHtml(HttpContext context)
        {
            var request = context.Request?.QueryString.ToString()?.Substring(1);

            var code = HttpOperationUtils.ReadFromFile(fileName: request, directoryName: "SourceCode");

            var resultText = "";
            string contentType = "text/plain";

            try 
            {
                resultText = HttpOperationUtils.CompileCode(code, $"{request}.dll");
                
                contentType = "text/HTML";
            }
            catch (Exception e)
            {
                resultText = e.Message;
            }

            await HttpOperationUtils.SendResponse(context, resultText, contentType);
        }
    }
}