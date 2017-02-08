using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChaosCMS.Managers;
using ChaosCMS.Extensions;

namespace ChaosCMS.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public class DivRenderer<TContent> : IRenderer<TContent>
        where TContent : class
    {
        private readonly ContentManager<TContent> contentManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentManager"></param>
        public DivRenderer(ContentManager<TContent> contentManager)
        {
            this.contentManager = contentManager;
        }

        /// <summary>
        /// 
        /// </summary>
        public string TypeName => "div";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chaos"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public IHtmlContent RenderAsync(IChaos<TContent> chaos, TContent content)
        {
            var grid = new TagBuilder("div");
            var value = this.contentManager.GetValueAsync<GridOptions, TContent>(content).GetAwaiter().GetResult();
            grid.AddCssClass(value.CssClass);
            RenderRowsAsync(chaos, grid, content).GetAwaiter().GetResult();
            return grid;
        }

        private async Task RenderRowsAsync(IChaos<TContent> chaos, TagBuilder grid, TContent content)
        {
            var children = await this.contentManager.GetChildrenAsync(content);
            
            foreach (var item in children)
            {
                grid.InnerHtml.AppendHtml(chaos.RenderAsync(item));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class GridOptions
        {
            /// <summary>
            /// 
            /// </summary>
            public string CssClass { get; set; }
        }
    }
}
