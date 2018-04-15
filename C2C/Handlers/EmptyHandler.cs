using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace C2C.Handlers
{
    public class EmptyHandler : RequestHandlerBase
    {
        protected override string route => "/";

        protected override string method => "";

        public override bool CheckRequest(HttpContext context) =>
            context.Request?.Path.ToString() == route;

        protected override Task Execute(HttpContext context)
        {
            context.Response.Redirect("/Index");
            return Task.CompletedTask;
        }
    }
}