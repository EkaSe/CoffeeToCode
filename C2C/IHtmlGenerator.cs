using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace C2C
{
    public interface IHtmlGenerator
    {
         Task ProduceHtml(HttpContext context);
    }
}