using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS.Hal
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHalConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool CanConvert(Type type);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        HalResponse Convert(object model);
    }
}
