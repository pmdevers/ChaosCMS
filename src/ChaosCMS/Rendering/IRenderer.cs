using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public interface IRenderer<TContent>
    {
        /// <summary>
        /// 
        /// </summary>
        string TypeName { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="chaos"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<IHtmlContent> RenderAsync(IChaos chaos, TContent content);
    }
}
