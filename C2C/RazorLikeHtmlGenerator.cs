using System;
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

            var razor = ReadFromFile(fileName: Request(context), directoryName: "RazorPages");
            var code = ParseRazor(razor);

            
            

            await SendResponse(context, resultText, contentType);
        }

        ///<summary>
        ///converts razor syntax to the code to be invoked
        ///</summary>
        private string ParseRazor(string razorSource)
        {
            StringBuilder code = new StringBuilder("");

            StringBuilder pureHtmlPart = new StringBuilder("");

            for (int i = 0; i < razorSource.Length; i++)
            {
                if (razorSource[i] == '@' && i < razorSource.Length - 1)
                {
                    code.Append($"Console.Write(\"{pureHtmlPart.ToString()}\");/n");
                    pureHtmlPart = new StringBuilder("");

                    if (razorSource[i + 1] == '{')
                    {                     
                        var codePartLength = FindClosingParenthesisIndex(razorSource.Substring(i + 1));
                        code.Append(razorSource.Substring(i + 1, codePartLength));
                        i += codePartLength;
                    }
                    else
                    {
                        var varName = GetFirstWord(razorSource.Substring(i + 1));
                        code.Append($"Console.Write({varName});/n");
                        i += varName.Length;
                    }
                }
                else
                {
                    pureHtmlPart.Append(razorSource[i]);
                }                
            }
            code.Append(pureHtmlPart);

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