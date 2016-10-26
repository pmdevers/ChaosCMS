using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS.Hal.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class HalEmbeddedAttribute : HalPropertyAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string CollectionName { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collectionName"></param>
        public HalEmbeddedAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
