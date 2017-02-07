using ChaosCMS.Managers;
using Microsoft.AspNetCore.Html;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

using ChaosCMS.Extensions;
using ChaosCMS.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    /// <typeparam name="TContent"></typeparam>
    public class DefaultChaosService<TPage, TContent> : IChaos
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
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IHtmlContent> RenderAsync(string name)
        {
            var content = await this.contentManager.FindByNameAsync(name);
            var type = await this.contentManager.GetTypeAsync(content);
            var renderer = this.renderers.FirstOrDefault(x => x.TypeName.Equals(type));
            return await renderer.RenderAsync(this, content);
        }
    }
}