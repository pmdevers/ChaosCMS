using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILookupNormalizer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Normalize(string value);
    }
}
