using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    public static class ChaosResourceTypes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetContentType(string path)
        {
            return MimeTypes[Path.GetExtension(path)];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static bool Contains(string extension)
        {
            return MimeTypes.ContainsKey(extension.ToLowerInvariant());
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly Dictionary<string, string> MimeTypes = new Dictionary<string, string>
        {
            {".js", "text/javascript"},
            {".css", "text/css"},
            {".gif", "image/gif"},
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".xml", "application/xml"},
            {".txt", "text/plain"},
            {".html", "text/html"}
        };
    }
}
