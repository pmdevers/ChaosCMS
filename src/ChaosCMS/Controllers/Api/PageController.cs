using ChaosCMS.Extensions;
using ChaosCMS.Hal;
using ChaosCMS.Managers;
using ChaosCMS.Models.Pages;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// A controller for handling a page
    /// </summary>
    [Route("api/page", Name = "pages")]
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
        [HttpGet(Name = "pages")]
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

            return this.Hal(page, this.GetPageLinks(manager, page));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/children", Name = "page-children")]
        public async Task<IActionResult> GetChildren(string id)
        {
            var page = await this.manager.FindByIdAsync(id);

            if (page == null)
            {
                return this.ChaosResults(this.manager.ErrorDescriber.PageIdNotFound(id));
            }

            var children = await this.manager.GetChildrenAsync(page);
            var list = children.Select(x => new Link("children", Url.RouteUrl("page", new { id = this.manager.GetId(x) })));            
            return this.Hal(list);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        [HttpPost("{id}/children")]
        public async Task<IActionResult> GetChildren(string id, [FromBody] ChildModel child)
        {
            var page = await this.manager.FindByIdAsync(id);

            if (page == null)
            {
                return this.ChaosResults(this.manager.ErrorDescriber.PageIdNotFound(id));
            }

            var childPage = await this.manager.FindByIdAsync(child.Id);
            if(childPage == null)
            {
                return this.ChaosResults(this.manager.ErrorDescriber.PageIdNotFound(id));
            }

            var result = await this.manager.AddChildAsync(page, childPage);
            
            return this.ChaosResults(result);
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