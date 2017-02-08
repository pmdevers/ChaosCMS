using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using ChaosCMS.Managers;

namespace ChaosCMS.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public class StringRenderer<TContent> : IRenderer<TContent>
        where TContent : class
    {
        private readonly ContentManager<TContent> contentManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentManager"></param>
        public StringRenderer(ContentManager<TContent> contentManager)
        {
            this.contentManager = contentManager;
        }


        /// <summary>
        /// 
        /// </summary>
        public string TypeName => "string";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chaos"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public IHtmlContent RenderAsync(IChaos<TContent> chaos, TContent content)
        {
            var value = this.contentManager.GetValueAsync(content).GetAwaiter().GetResult();
            return new HtmlString(value);
        }
    }
}
