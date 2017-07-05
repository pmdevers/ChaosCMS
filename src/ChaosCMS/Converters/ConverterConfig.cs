using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class ConverterConfig<TDestination>
    {
        /// <summary>
        /// 
        /// </summary>
        public bool AlwaysNew { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Func<TDestination, Task> BeforeCreate { get; set; } = (dest) => Task.FromResult(0);

        /// <summary>
        /// 
        /// </summary>
        public Func<TDestination, Task> AfterCreate { get; set; } = (dest) => Task.FromResult(0);
    }
}
