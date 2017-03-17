using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    public interface IChaos
    {
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        IHtmlHelper Helper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IHtmlContent> RenderAsync(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IHtmlContent> Scripts();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task AddScript(IHtmlContent content);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public interface IChaos<TContent> : IChaos
         where TContent : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<IHtmlContent> RenderAsync(TContent content);
    }
}
