using ChaosCMS.Managers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS
{
    /// <summary>
    /// The chaos middleware
    /// </summary>
    public class ChaosMiddleware<TPage>
        where TPage : class
    {
        private readonly RequestDelegate _next;
        private readonly PageManager<TPage> pageManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="ChaosMiddleware{TPage}"/>
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="pageManager">The chaos <see cref="PageManager{TPage}"/>.</param>
        public ChaosMiddleware(RequestDelegate next, PageManager<TPage> pageManager)
        {
            _next = next;
            this.pageManager = pageManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            var page = await pageManager.FindByUrlAsync(httpContext.Request.Path.Value);
            var name = await pageManager.GetNameAsync(page);
            await httpContext.Response.WriteAsync(name);

            await _next(httpContext);
        }
    }
}
