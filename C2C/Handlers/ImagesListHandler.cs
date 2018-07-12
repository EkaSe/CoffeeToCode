using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using C2C;
using System;
using System.Linq;

namespace C2C.Handlers
{
    public class ImagesListHandler : RequestHandlerBase
    {
        protected override string route => "/imagesList";

        protected override string method => "GET";

        protected override async Task Execute(HttpContext context)
        {
            var dataDirectory = "Data";
            var files = Directory.GetFiles(dataDirectory);
            
            await Utils.SendResponse(context, JsonConvert.SerializeObject(files), ExtensionContentType.Instance[".json"]);
        }
    }
}