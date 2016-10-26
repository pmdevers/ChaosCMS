using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS.Hal.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class HalLinkAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Rel { get; }
        /// <summary>
        /// 
        /// </summary>
        public string Href { get; }
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; }
        /// <summary>
        /// 
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rel"></param>
        /// <param name="href"></param>
        /// <param name="title"></param>
        /// <param name="method"></param>
        public HalLinkAttribute(string rel, string href, string title = null, string method = null)
        {
            Rel = rel;
            Href = href;
            Title = title;
            Method = method;
        }
    }
}
