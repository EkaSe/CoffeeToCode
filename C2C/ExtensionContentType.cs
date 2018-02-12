using System.Collections;
using System.Collections.Generic;

namespace C2C
{
    internal sealed class ExtensionContentType
    {
        private static Dictionary<string, string> types;

        private ExtensionContentType()
        {
            types = new Dictionary<string, string>
                    {
                            {".html", "text/HTML"},
                            {".css", "text/plain"},
                            {".js", "text/plain"},
                            {".jpeg", "image/JPEG"},
                            {".jpg", "image/JPEG"}
                    };
        }

        private static ExtensionContentType instance;

        public static ExtensionContentType Instance
        {
            get 
            {
                if (instance is null) 
                {
                    instance = new ExtensionContentType();
                }
                return instance;
            }
        }
       
        public string this[string extension] => types.ContainsKey(extension) 
                    ? types[extension]
                    : "text/plain"; 
    }
}