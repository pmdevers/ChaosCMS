using ChaosCMS.Managers;
using ChaosCMS.Razor;
using Microsoft.AspNetCore.Http;
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
        private readonly IChaosRazorEngine engine;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChaosMiddleware{TPage}"/>
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="pageManager">The chaos <see cref="PageManager{TPage}"/>.</param>
        /// <param name="engine">The <see cref="IChaosRazorEngine"/> used to generate the template</param>
        public ChaosMiddleware(RequestDelegate next, PageManager<TPage> pageManager, IChaosRazorEngine engine)
        {
            _next = next;
            this.pageManager = pageManager;
            this.engine = engine;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            var page = await pageManager.FindByUrlAsync(httpContext.Request.Path.Value);
            if(page == null)
            {
                throw ChaosHttpExeption.PageNotFound(httpContext.Request.Path.Value);
            }

            var templateName = await pageManager.GetTemplateAsync(page);
            var results = engine.Parse(templateName + ".cshtml", new object());

            httpContext.Response.ContentType = "text/html";
                                   
            await httpContext.Response.WriteAsync(results);

            await _next(httpContext);
        }
    }
}
