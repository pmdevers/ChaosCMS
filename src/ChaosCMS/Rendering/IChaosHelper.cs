using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    public interface IChaosHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IHtmlContent Raw(string test);
    }
}
