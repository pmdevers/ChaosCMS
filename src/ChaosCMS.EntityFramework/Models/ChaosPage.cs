using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS.Entityframework
{
    /// <summary>
    /// 
    /// </summary>
    public class ChaosPage : ChaosPage<string>
    {

    }


    /// <summary>
    /// The base of the Page
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key.</typeparam>
    public class ChaosPage<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// The primary key of the page.
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// The name of the page.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The url of the page.
        /// </summary>
        public string Url { get; set; }
    }
}
