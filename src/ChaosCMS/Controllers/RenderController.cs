using System.Threading.Tasks;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ChaosCMS.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    [Route("{*url}", Name = "render")]
    public class RenderController<TPage> : Controller
        where TPage : class
    {
        private readonly PageManager<TPage> pageManager;

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageManager"></param>
        public RenderController(PageManager<TPage> pageManager)
        {
            this.pageManager = pageManager;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var page = await this.pageManager.FindByUrlAsync(this.Request.Path.Value);
            if (page == null)
            {
                page = await this.pageManager.FindByStatusCodeAsync(404);
                if (page == null)
                {
                    return NotFound();
                }
            }
            var statusCode = this.pageManager.GetStatusCodeAsync(page);
            var template = await this.pageManager.GetTemplateAsync(page);
            return this.View(template, page);
        }
    }
}