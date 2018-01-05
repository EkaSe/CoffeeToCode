using System;
using System.IO;
using System.Text;

namespace Index
{
    class Program
    {
        static void Main(string[] args)
        {
            var initialFile = new FileInfo($"Index.html");

            if (initialFile.Exists)
            {
                var htmlText = File.ReadAllText(initialFile.FullName);
                var appendedHtml = new StringBuilder(htmlText)
                    .Insert(
                        htmlText.IndexOf("</body>"),  
                        $"<div>Requested file is Index.html. {DateTime.Now}</div>")
                    .ToString();

                string currentPath = Directory.GetCurrentDirectory();
                var path = currentPath.Substring(0, currentPath.IndexOf("HtmlGen")) + @"wwwroot/Index.html";

                using (StreamWriter outputFile = new StreamWriter(path)) 
                // '/home/semiash/projects/C2CGen/HtmlGen/Index/wwwroot/Index.html'.
                {
                    outputFile.WriteAsync(appendedHtml);
                }
            }
        }
    }
}
