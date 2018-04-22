using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using C2C;
using System;
using System.Linq;

namespace C2C.Handlers
{
    public class SavedDataHandler : RequestHandlerBase
    {
        protected override string route => "/savedData";

        protected override string method => "GET";

        protected override async Task Execute(HttpContext context)
        {
            var dataDirectory = "Data";
            var files = Directory.GetFiles(dataDirectory);
            if (files.Length != 0)
            {
                Array.Sort(files, StringComparer.InvariantCulture);
                var fileName = files.Last();
                await this.SendFileAsResponse(context, fileName, ExtensionContentType.Instance[".json"]);
            }

            await Utils.SendResponse(context, "{}", ExtensionContentType.Instance[".json"]);
        }
    }
}