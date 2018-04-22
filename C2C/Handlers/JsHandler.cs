using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders.Physical;

namespace C2C.Handlers
{
    public class JsHandler : RequestHandlerBase
    {
        protected override string route => "";

        protected override string method => "";

        public override bool CheckRequest(HttpContext context)
        {
            string fileName = context.Request?.Path.ToString();

            return fileName.EndsWith("js");
        }

        protected override async Task Execute(HttpContext context)
        {
            string fileName = context.Request?.Path.ToString();
            await SendFileAsResponse(
                    context, 
                    $"RazorPages/Scripts/{fileName}", 
                    ExtensionContentType.Instance["js"]);


            // var resultText = "";
            // string contentType = ExtensionContentType.Instance[".js"];

            // string fileName = context.Request?.Path.ToString();

            // try 
            // {
            //     var razor = HttpOperationUtils.ReadFromFile(fileName: fileName, directoryName: "RazorPages/Scripts");
            //     var code = ParseRazor(razor);
            //     resultText = HttpOperationUtils.CompileCode(code, context : context);
            // }
            // catch (Exception e)
            // {
            //     resultText = e.Message;
            //     contentType = ExtensionContentType.Instance[""];
            // }          

            // await HttpOperationUtils.SendResponse(context, resultText, contentType);
        }
    }
}