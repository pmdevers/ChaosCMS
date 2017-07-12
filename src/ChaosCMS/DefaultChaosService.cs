using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaosCMS.Extensions;
using ChaosCMS.Managers;
using ChaosCMS.Rendering;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using ChaosCMS.Models.Pages;

namespace ChaosCMS
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    public class DefaultChaosService<TPage> : IChaos
        where TPage : class
    {
        private readonly PageManager<TPage> pageManager;
        private readonly HttpContext context;
        private readonly IList<IHtmlContent> scripts = new List<IHtmlContent>();
        private readonly IEnumerable<IRenderer> renderers;
        /// <summary>
        ///
        /// </summary>
        /// <param name="pageManager"></param>
        /// <param name="context"></param>
        /// <param name="renderers"></param>
        public DefaultChaosService(PageManager<TPage> pageManager, IHttpContextAccessor context, IEnumerable<IRenderer> renderers)
        {
            this.pageManager = pageManager;
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
                    currentPage = this.pageManager.FindCurrent();
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
            Content content = await Getcontent(name);

            return await this.RenderAsync(content);
        }

        private async Task<Content> Getcontent(string name)
        {

            if (this.Helper.ViewData.Model is Content content)
            {
                content = content.Children.FirstOrDefault(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            }
            else
            {
                var contents = await this.pageManager.GetContentAsync(CurrentPage);
                content = contents.FirstOrDefault(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            }

            return content;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<IHtmlContent> RenderAsync(Content content)
        {
            if (content == null)
            {
                return HtmlString.Empty;
            }
            
            var renderer = this.renderers.FirstOrDefault(x => x.TypeName.Equals(content.Type));

            if (renderer == null)
            {
                renderer = this.renderers.FirstOrDefault(x => x.TypeName.Equals("default")); 
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JObject GetJson()
        {
            if (this.Helper.ViewData.Model is Content content)
            {
                return JObject.Parse(content.Value);
            }
            return new JObject();
        }
    }
}