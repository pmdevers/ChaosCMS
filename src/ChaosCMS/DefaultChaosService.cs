using ChaosCMS.Managers;
using Microsoft.AspNetCore.Html;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

using ChaosCMS.Extensions;
using ChaosCMS.Rendering;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    /// <typeparam name="TContent"></typeparam>
    public class DefaultChaosService<TPage, TContent> : IChaos<TContent>
        where TPage : class
        where TContent : class
    {
        private readonly PageManager<TPage> pageManager;
        private readonly ContentManager<TContent> contentManager;
        private readonly HttpContext context;
        private readonly IEnumerable<IRenderer<TContent>> renderers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageManager"></param>
        /// <param name="contentManager"></param>
        /// <param name="context"></param>
        /// <param name="renderers"></param>
        public DefaultChaosService(PageManager<TPage> pageManager, ContentManager<TContent> contentManager, IHttpContextAccessor context, IEnumerable<IRenderer<TContent>> renderers)
        {
            this.pageManager = pageManager;
            this.contentManager = contentManager;
            this.context = context.HttpContext;
            this.renderers = renderers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TPage CurrentPage
        {
            get
            {
                if (currentPage == null)
                {
                    currentPage = this.pageManager.FindByUrl(context.Request.Path);
                }
                return currentPage;
            }
        }

        private TPage currentPage;
        /// <summary>
        /// 
        /// </summary>
        public string Name { get { return this.pageManager.GetName(CurrentPage); } }

        /// <summary>
        /// 
        /// </summary>
        public IHtmlHelper Helper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IHtmlContent RenderAsync(string name)
        {
            TContent content = Helper.ViewData.Model as TContent;
            var pageId = this.pageManager.GetIdAsync(CurrentPage).GetAwaiter().GetResult();
            if (content != null)
            {
                var children1 = this.contentManager.GetChildrenAsync(content).GetAwaiter().GetResult();
                var child1 = children1.FirstOrDefault(x => this.contentManager.GetNameAsync(x).GetAwaiter().GetResult().Equals(name, StringComparison.CurrentCultureIgnoreCase));
                return this.RenderAsync(child1);
            }
            var children = this.contentManager.FindByPageIdAsync(pageId).GetAwaiter().GetResult();
            var child = children.FirstOrDefault(x => this.contentManager.GetNameAsync(x).GetAwaiter().GetResult().Equals(name, StringComparison.CurrentCultureIgnoreCase));
            return this.RenderAsync(child);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IHtmlContent RenderAsync(TContent content)
        {
            if(content == null)
            {
                return HtmlString.Empty;
            }
            var type = this.contentManager.GetTypeAsync(content).GetAwaiter().GetResult();
            var renderer = this.renderers.FirstOrDefault(x => x.TypeName.Equals(type));
            return renderer.RenderAsync(this, content);
        }
    }
}