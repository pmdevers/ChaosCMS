using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.EntityFramework
{
    /// <inhertis />
    public class ChaosContent : ChaosContent<ChaosContent, int>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class ChaosContent<TContent, TKey>
        where TKey : struct, IEquatable<TKey>
        where TContent : ChaosContent<TContent, TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TKey? ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NormalizedName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual TContent Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<TContent> Children { get; set; } = new HashSet<TContent>();
        /// <summary>
        /// 
        /// </summary>
        public TKey PageId { get; set; }
    }
}
