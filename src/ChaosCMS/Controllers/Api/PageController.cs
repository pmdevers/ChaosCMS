using ChaosCMS.Extensions;
using ChaosCMS.Hal;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// A controller for handling a page
    /// </summary>
    [Route("api/pages", Name = "pages")]
    public class PageController<TPage> : Controller 
        where TPage : class
    {
        private readonly PageManager<TPage> manager;

        /// <summary>
        ///
        /// </summary>
        /// <param name="manager"></param>
        public PageController(PageManager<TPage> manager)
        {
            this.manager = manager;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(int page = 1, int itemsPerPage = 25)
        {
            var pages = this.manager.FindPagedAsync(page, itemsPerPage).Result;
            return this.PagedHal(pages, item => this.CreateEmbeddedResponse(this.manager, item), "pages");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "page")]
        public async Task<IActionResult> Get(string id)
        {
            var page = await this.manager.FindByIdAsync(id);

            if(page == null)
            {
                return this.ChaosResults(this.manager.ErrorDescriber.PageIdNotFound(id));
            }

            return this.Hal(page, new[]
            {
                this.SelfLink(this.manager, page),
                new Link("content", "/api/page/{id}/content"),
                new Link("ac:copy", "/api/page/{id}/copy"),
                new Link("ac:publish", "/api/page/{id}/publish")
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TPage page)
        {
            var result = await this.manager.CreateAsync(page);
            return this.ChaosResults(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<TPage> model)
        {
            var page = await this.manager.FindByIdAsync(id);

            model.ApplyTo(page, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await this.manager.UpdateAsync(page);

            return this.ChaosResults(result);
        }
    }
}