using System.Web;
using System.Net;
using System.Net.Sockets;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace C2C
{
    public class MyHandlerMiddleware
    {

        // Must have constructor with this signature, otherwise exception at run time
        private readonly RequestDelegate _next;
        
        public MyHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // public async Task Invoke(HttpContext context)
        // {
        //     string response = GenerateResponse(context);

        //     context.Response.ContentType = GetContentType();
        //     await context.Response.WriteAsync(response);
        // }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.QueryString.ToString().Contains("simple"))
            {
                await ReturnIndexPage(context);          
                return;
            }
            
            await _next.Invoke(context);
            //await ReturnIndexPage(context);  
        }

        private static async Task ReturnIndexPage(HttpContext context)
        {
            var file = new FileInfo(@"wwwroot\Index.html");
            var testFile = new FileInfo(fileName: @"blabla.txt");
            using (StreamWriter sw = testFile.CreateText()) 
            {
                sw.WriteLine("Hello");
                sw.WriteLine("And");
                sw.WriteLine("Welcome");
            }	
            byte[] buffer;
            if (file.Exists)
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "text/html";

                buffer = File.ReadAllBytes(file.FullName);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "text/plain";
                buffer = Encoding.UTF8
                    .GetBytes(s: $"Unable to find the requested file0{file.Directory}");
            }

            context.Response.ContentLength = buffer.Length;

            using (var stream = context.Response.Body)
            {
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.FlushAsync();
            }    
        }

        private string GenerateResponse(HttpContext context)
        {
            string title = context.Request.Query["title"];
            context.Response.WriteAsync("<html><body><h1>Index.html</h1></body></html>");

            ReturnIndexPage(context); 

        //     Response.Write("<html>");
        // Response.Write("<body>");
        // Response.Write("<h1>Hello from a synchronous custom HTTP handler.</h1>");
        // Response.Write("</body>");
        // Response.Write("</html>");
            return $"Title of the report: {title}";
        }

        private string GetContentType()
        {
            return "text/plain";
        }
    }

    public static class MyHandlerExtensions
    {
        public static IApplicationBuilder UseMyHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyHandlerMiddleware>();
        }
    }
}