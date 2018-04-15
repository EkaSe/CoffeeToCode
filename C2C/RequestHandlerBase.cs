using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders.Physical;

namespace C2C
{
    public abstract class RequestHandlerBase
    {
        protected abstract string route { get; }

        protected abstract string method { get; }

        public virtual bool CheckRequest(HttpContext context) =>
            context.Request?.Path.ToString().ToLower() == this.route.ToLower()
            && context.Request?.Method.ToUpper() == this.method.ToUpper();

        public virtual async Task HandleRequest(HttpContext context)
        {
            if (this.CheckRequest(context))
            {
                await this.Execute(context);
            }
        }

        protected abstract Task Execute(HttpContext context);


        protected async Task SendFileAsResponse(HttpContext context, string fileName, string contentType)
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