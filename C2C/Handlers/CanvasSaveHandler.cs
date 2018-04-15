using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace C2C.Handlers
{
    public class CanvasSaveHandler : RequestHandlerBase
    {
        protected override string route => "/canvas/save";

        protected override string method => "POST";

        protected override async Task Execute(HttpContext context)
        {     
            string clicks;

            using (var requestReader = new StreamReader(context.Request.Body))
            {
                clicks = requestReader.ReadToEnd();
            }

            string path = $"Data/clicks{DateTime.Now.ToString("s")}";
            
            using (StreamWriter file = new StreamWriter(path, false))
            {
                await file.WriteAsync(clicks);
            }
        }
    }
}