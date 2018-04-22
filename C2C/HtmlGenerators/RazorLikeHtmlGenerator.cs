using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2C.Handlers;
using Microsoft.AspNetCore.Http;

namespace C2C.HtmlGenerators
{
    public class RazorLikeHtmlGenerator : IHtmlGenerator
    {
        public RazorLikeHtmlGenerator()
        {
            HandlerRegistrar.Register<EmptyHandler>();
            HandlerRegistrar.Register<CanvasSaveHandler>();
            HandlerRegistrar.Register<SavedDataHandler>();

            HandlerRegistrar.Register<RazorJsHandler>();

            //default:
            HandlerRegistrar.Register<JsHandler>();
            HandlerRegistrar.Register<RazorHandler>();
        }

        public async Task ProduceHtml(HttpContext context)
        {
            await context.HandleRequest(tryAllHandlers: false);
        }      
        
    }
}