using System.Collections;
using System.Collections.Generic;

namespace C2C
{
    internal class ExtensionContentType
    {
        private static Dictionary<string, string> types = new Dictionary<string, string>
        {
                {".html", "text/HTML"},
                {".css", "text/plain"},
                {".js", "text/plain"},
                {".jpeg", "image/JPEG"},
                {".jpg", "image/JPEG"}
        };
       
        public string this[string extension] 
        { 
            get => 
                types.ContainsKey(extension) 
                ? types[extension] 
                : "text/plain"; 
        }
    }
}