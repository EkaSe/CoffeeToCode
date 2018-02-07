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

namespace C2C
{
    public class RuntimeCompilatorHtmlGenerator : HtmlGeneratorBase, IHtmlGenerator
    {
        public async Task ProduceHtml(HttpContext context)
        {
            var code = ReadFromFile(fileName: Request(context), directoryName: "SourceCode");

            var resultText = "";
            string contentType = "text/plain";

            try 
            {
                resultText = CompileCode(code, $"{Request(context)}.dll");
                
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