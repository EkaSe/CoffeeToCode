using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders.Physical;

namespace C2C.Handlers
{
    public class RazorJsHandler : RazorHandler
    {
        protected override string route => "";

        protected override string method => "";

        protected override bool CheckForJsComments => true;

        public override bool CheckRequest(HttpContext context)
        {
            string fileName = context.Request?.Path.ToString();

            return fileName.EndsWith("rjs");
        }

        protected override async Task Execute(HttpContext context)
        {
            // string fileName = context.Request?.Path.ToString();
            // await SendFileAsResponse(
            //         context, 
            //         $"RazorPages/Scripts/{fileName}", 
            //         ExtensionContentType.Instance["js"]);


            var resultText = "";
            string contentType = ExtensionContentType.Instance[".js"];

            string fileName = context.Request?.Path.ToString();

            try 
            {
                var razor = Utils.ReadFromFile(fileName: fileName, directoryName: "RazorPages/Scripts");
                var code = ParseRazor(razor);
                resultText = Utils.CompileCode(code, context : context);
            }
            catch (Exception e)
            {
                resultText = e.Message;
                contentType = ExtensionContentType.Instance[""];
            }          

            await Utils.SendResponse(context, resultText, contentType);
        }
    }
}