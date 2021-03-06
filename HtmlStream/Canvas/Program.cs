﻿using System;
using System.IO;
using System.Text;

namespace Canvas
{
    class Program
    {
        static void Main(string[] args)
        {

            var htmlText = 
                "<!DOCTYPE><html><head>" +
                    "<title>Coffee To Code</title>" +
                    "<meta name=\"keywords\" content=\"start\">" +
                    "<meta name=\"description\" content=\"ASP.NET - let's start\">" +
                    "<link rel=\"stylesheet\" type=\"text/css\" href=\"style.css\"></head>" +
                    "<body><div class=\"topbar\">" +
                    "<a class=\"C2C-logo\" href=\"https://github.com/EkaSe/CoffeeToCode/\">CoffeeToCode</a>" +
                    "<a href=\"Login.html\">Logout</a></div>" +
                    "<div class=\"canvas\"><canvas id=\"myCanvas\"></canvas></div>" +
                    "<div class=\"bottombar\"><a href=\"https://github.com/EkaSe/CoffeeToCode/\">CoffeeToCode</a></div>" +
                    "<script>" +
                    "var canvasSetup = document.getElementById(\"myCanvas\");" +
                    "var ctx = canvasSetup.getContext(\"2d\");" +
                    "ctx.beginPath();" +
                    "ctx.lineWidth=\"6\";" +
                    "ctx.strokeStyle=\"red\";" +
                    "ctx.rect(5,5,290,140);" +
                    "ctx.stroke();" +
                    "ctx.beginPath();" +
                    "ctx.lineWidth=\"4\";" +
                    "ctx.strokeStyle=\"green\";" +
                    "ctx.rect(30,30,50,50);" +
                    "ctx.stroke();" +
                    "ctx.beginPath();" +
                    "ctx.lineWidth=\"10\";" +
                    "ctx.strokeStyle=\"blue\";" +
                    "ctx.rect(50,50,150,80);" +
                    "ctx.stroke(); " +
                    "</script>" +
                $"<div>Requested file is Canvas.html. {DateTime.Now}</div>" +
                "</body></html>";

            Console.WriteLine(htmlText);
        }
    }
}
