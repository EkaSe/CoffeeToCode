using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace C2C
{
    public class RazorLikeHtmlGenerator : HtmlGeneratorBase, IHtmlGenerator
    {
        public async Task ProduceHtml(HttpContext context)
        {
            var resultText = "";
            string contentType = "text/plain";

            string fileName = context.Request?.Path.ToString();
            if (fileName == "/")
            {
                fileName = "Index";
            }
            if (fileName.StartsWith('/') || fileName.StartsWith('?'))
            {
                fileName = fileName.Substring(1);
            }

            if (fileName.Split(".").LastOrDefault() == "js")
            {
                await SendFileAsResponse(
                    context, 
                    $"RazorPages/Scripts/{fileName}", 
                    ExtensionContentType.Instance["js"]);
            }
            else
            {
                try 
                {
                    var razor = ReadFromFile(fileName: fileName, directoryName: "RazorPages");
                    var code = ParseRazor(razor);
                    resultText = CompileCode(code, context : context);

                    string fileExtension =  fileName.Split(".").LastOrDefault();
                    contentType = context.Request.ContentType ?? ExtensionContentType.Instance[fileExtension];
                }
                catch (Exception e)
                {
                    resultText = e.Message;
                }          

                await SendResponse(context, resultText, contentType);
            }
        }

        ///<summary>
        ///converts razor syntax to the code to be invoked
        ///</summary>
        private string ParseRazor(string razorSource)
        {
            var outputBufferName = "___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq";

            StringBuilder code = new StringBuilder("using System.Text;\n" +
                "using System;\n" +
                "using Microsoft.AspNetCore.Http;\n" +
                "public class Program{\n" +
                "public static string Main(object args_context) {\n" +
                "HttpContext _context = args_context as HttpContext;\n" +
                $"var {outputBufferName} = new StringBuilder(\"\");\n");

            StringBuilder pureHtmlPart = new StringBuilder("");

            for (int i = 0; i < razorSource.Length; i++)
            {
                if (razorSource[i] == '@' && i < razorSource.Length - 1)
                {
                    code.Append($"{outputBufferName}.Append(\"{pureHtmlPart.ToString()}\");\n");
                    pureHtmlPart = new StringBuilder("");

                    if (razorSource[i + 1] == '{')
                    {                     
                        var codePartLength = FindClosingParenthesisIndex(razorSource.Substring(i + 1));
                        code.Append(razorSource.Substring(i + 2, codePartLength - 2) + '\n');
                        i += codePartLength;
                    }
                    else
                    {
                        var varName = GetFirstWord(razorSource.Substring(i + 1));
                        code.Append($"{outputBufferName}.Append({varName}.ToString());\n");
                        i += varName.Length;
                    }
                }
                else if (razorSource[i] == '\n')
                {
                    pureHtmlPart.Append("\" + \n \"");
                }
                else if (razorSource[i] == '"')
                {
                    pureHtmlPart.Append("\\\"");
                }
                else
                {
                    pureHtmlPart.Append(razorSource[i]);
                }                
            }
            code.Append($"{outputBufferName}.Append(\"{pureHtmlPart.ToString()}\");\n");

            code.Append($"return {outputBufferName}.ToString();\n" +"}\n}");

            Console.WriteLine(code);

            return code.ToString();
        }

        private int FindClosingParenthesisIndex(string text)
        {
            int parenthesisCount = 1;
            int i;
            for (i = 1; i < text.Length && parenthesisCount != 0; i++)
            {   
                if (text[i] == '{')
                {
                    parenthesisCount++;
                }
                if (text[i] == '}')
                {
                    parenthesisCount--;
                }
            }

            if(parenthesisCount != 0 && i == text.Length)
            {
                throw new Exception("Invalid razor syntax: parentheses inconsistency");
            }
            
            return i;
        }

        private string GetFirstWord(string text)
        {
            int i = 0;
            StringBuilder result = new StringBuilder("");
            while (i < text.Length && IsIdentifierChar(text[i], i == 0))
            {
                result.Append(text[i++]);
            }
            return result.ToString();
        }

        private bool IsLetter (char symbol) 
        {
			int code = (int)symbol;
			return ((int)'A' <= code && code <= (int)'Z' 
                || (int)'a' <= code && code <= (int)'z');
		}

		private bool IsIdentifierChar(char symbol, bool isFirstChar) =>
            IsLetter (symbol)
            || symbol == '_'
            || IsDigit(symbol) && !isFirstChar;

        private bool IsDigit(char symbol) => (int) symbol >= (int) '0' && (int) symbol <= (int) '9';
        
    }
}