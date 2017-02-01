using System;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ChaosCMS.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    public class ChaosHelper<TPage> : ChaosHelper<dynamic, TPage> where TPage : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <param name="pageManager"></param>
        public ChaosHelper(IHttpContextAccessor contextAccessor, PageManager<TPage> pageManager) : base(contextAccessor, pageManager)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TPage"></typeparam>
    public class ChaosHelper<TModel, TPage> : IChaosHelper<TModel, TPage> where TPage : class
    {
        private readonly PageManager<TPage> pageManager;
        private readonly HttpContext context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <param name="pageManager"></param>
        public ChaosHelper(IHttpContextAccessor contextAccessor, PageManager<TPage> pageManager)
        {
            this.pageManager = pageManager;
            this.context = contextAccessor.HttpContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public IHtmlContent Raw(string test)
        {
            throw new ArgumentNullException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IHtmlContent Test()
        {

            return new HtmlString("test");
        }

        /// <inheritdoc />
        public TPage CurrentPage()
        {
            var url = this.context.Request.Path;
            var page = this.pageManager.FindByUrlAsync(url).Result;
            return page;
        }
    }
}
