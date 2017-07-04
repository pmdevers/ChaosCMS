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
    public class ConverterConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public bool AlwaysNew { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Func<Task> BeforeCreate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Func<Task> AfterCreate { get; set; }
    }
}
