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

            return fileName.Split(".").LastOrDefault() == "js";
        }

        protected override async Task Execute(HttpContext context)
        {
            string fileName = context.Request?.Path.ToString();
            await SendFileAsResponse(
                    context, 
                    $"RazorPages/Scripts/{fileName}", 
                    ExtensionContentType.Instance["js"]);
        }

        private async Task SendFileAsResponse(HttpContext context, string fileName, string contentType)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            var file = new FileInfo(fileName);
            
            if (file.Exists)
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                context.Response.ContentLength = file.Length;

                await context.Response.SendFileAsync(new PhysicalFileInfo(file));
            }
        }

    }
}