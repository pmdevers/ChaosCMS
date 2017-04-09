using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaosCMS.Extensions;
using ChaosCMS.Managers;
using ChaosCMS.Rendering;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
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
        private readonly IList<IHtmlContent> scripts = new List<IHtmlContent>();

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
        public string Name
        {
            get
            {
                return this.pageManager.GetName(CurrentPage);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public IHtmlHelper Helper { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IHtmlContent> RenderAsync(string name)
        {
            var content = this.Helper.ViewData.Model as TContent;

            if (content != null)
            {
                content = await this.contentManager.FindChildByNameAsync(content, name);
            }
            else
            {
                var pageId = await this.pageManager.GetIdAsync(CurrentPage);
                content = await this.contentManager.FindByPageIdAsync(pageId, name);
            }

            return await this.RenderAsync(content);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<IHtmlContent> RenderAsync(TContent content)
        {
            if (content == null)
            {
                return HtmlString.Empty;
            }
            var type = await this.contentManager.GetTypeAsync(content);
            var renderer1 = this.renderers.FirstOrDefault(x => x.TypeName.Equals("default"));
            var renderer = this.renderers.FirstOrDefault(x => x.TypeName.Equals(type));

            if (renderer == null)
            {
                renderer = renderer1;
            }

            return await renderer.RenderAsync(this, content);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Task<IHtmlContent> Scripts()
        {
            var script = new TagBuilder("script");

            foreach (var row in scripts)
            {
                script.InnerHtml.AppendHtml(row);
            }

            return Task.FromResult<IHtmlContent>(script);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Task AddScript(IHtmlContent content)
        {
            scripts.Add(content);
            return Task.FromResult(0);
        }
    }
}