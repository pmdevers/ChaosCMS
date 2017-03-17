using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using ChaosCMS.Managers;
using ChaosCMS.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChaosCMS.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public class DefaultRenderer<TContent> : IRenderer<TContent>
        where TContent : class
    {
        private readonly ContentManager<TContent> contentManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentManager"></param>
        public DefaultRenderer(ContentManager<TContent> contentManager)
        {
            this.contentManager = contentManager;
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeName => "default";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chaos"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<IHtmlContent> RenderAsync(IChaos<TContent> chaos, TContent content)
        {
            var type = await this.contentManager.GetTypeAsync(content);
            var tagBuilder = new TagBuilder(type);
            var value = await this.contentManager.GetValueAsync<RenderOptions, TContent>(content);

            value.CssClass.Reverse();
            value.CssClass.ForEach(tagBuilder.AddCssClass);
            tagBuilder.MergeAttributes(value.Attributes);

            var children = await this.contentManager.GetChildrenAsync(content);
            foreach(var child in children)
            {
                var html = await chaos.RenderAsync(child);
                tagBuilder.InnerHtml.AppendHtml(html);
            }

            return tagBuilder;
        }
    }
}
