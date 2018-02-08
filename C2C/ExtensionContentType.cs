using System.Collections;
using System.Collections.Generic;

namespace C2C
{
    internal sealed class ExtensionContentType
    {
        private static Dictionary<string, string> _types;

        private static Dictionary<string, string> types 
        {
            get 
            {
                if (_types is null) 
                {
                    _types = new Dictionary<string, string>
                    {
                            {".html", "text/HTML"},
                            {".css", "text/plain"},
                            {".js", "text/plain"},
                            {".jpeg", "image/JPEG"},
                            {".jpg", "image/JPEG"}
                    };
                }
                return _types;
            }
        }
       
        public string this[string extension] => types.ContainsKey(extension) 
                    ? types[extension]
                    : "text/plain"; 
    }
}