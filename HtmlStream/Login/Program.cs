﻿using System;
using System.IO;
using System.Text;

namespace HtmlStream
{
    class Program
    {
        static void Main(string[] args)
        {

            var htmlText = 
                "<!DOCTYPE><html><head><title>Coffee To Code - Login</title>" +
                "<link rel=\"stylesheet\" type=\"text/css\" href=\"style.css\">" +
                "</head><body><div class=\"topbar\"></div>" +
                "<form action=\"Canvas.html\" class=\"loginform\">" +
                "<label for \"login\">Login</label><br>" +
                "<input type=\"text\" id=\"login\"><br>" +
                "<label for \"password\">Password</label><br>" +
                "<input type=\"password\" id=\"password\"><br>" +
                "<input type=\"submit\" value=\"Login\" class=\"login-submit\">" +
                "</form>" +
                $"<div>Requested file is Login.html. {DateTime.Now}</div>" +
                "</body></html>";

            Console.WriteLine(htmlText);
        }
    }
}
