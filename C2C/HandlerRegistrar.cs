using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders.Physical;

namespace C2C
{
    public class HandlerRegistrar
    {
        private static List<RequestHandlerBase> handlers = new List<RequestHandlerBase>();

        public static void Register<T>() where T: RequestHandlerBase
        {
            var handler = Activator.CreateInstance(typeof(T)) as T;
            if (handler!= null && !handlers.Contains(handler))
            {
                handlers.Add(handler);
            }
        }

        public static async Task HandleRequest(HttpContext context, bool tryAllHandlers = false)
        {
            var isRequestHandled = false;
            foreach (var handler in handlers)
            {
                if (tryAllHandlers || !isRequestHandled)
                { 
                    isRequestHandled = isRequestHandled || handler.CheckRequest(context);
                    await handler.HandleRequest(context);
                }
            }
        }
    }

    public static class HandlerExtensions
    {
        public static async Task HandleRequest(this HttpContext context, bool tryAllHandlers = false)
        {
            await HandlerRegistrar.HandleRequest(context, tryAllHandlers);
        }
    }
}