using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS.Hal
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHalModelConfig
    {
        /// <summary>
        /// 
        /// </summary>
        string LinkBase { get; }
        /// <summary>
        /// 
        /// </summary>
        bool ForceHal { get; }
    }
}
