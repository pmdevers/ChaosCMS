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
        where TContent : class
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
        IHtmlContent RenderAsync(IChaos<TContent> chaos, TContent content);
    }
}
