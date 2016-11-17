using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaosCMS.Extensions;
using ChaosCMS.Managers;
using ChaosCMS;
using ChaosCMS.Hal;

using Microsoft.AspNetCore.JsonPatch;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    [Route("api/content", Name = "contents")]
    public class ContentController<TContent> : Controller where TContent : class
    {
        private readonly ContentManager<TContent> manager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentManager"></param>
        public ContentController(ContentManager<TContent> contentManager)
        {
            this.manager = contentManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public IActionResult Get(int page = 1, int itemsPerPage = 25)
        {
            var content = this.manager.FindPagedAsync(page, itemsPerPage).Result;
            return this.PagedHal(content, item => this.CreateEmbeddedResponse(this.manager, item) , "contents");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}", Name = "content")]
        [HttpGet]
        public IActionResult Get(string id)
        {
            var content = this.manager.FindByIdAsync(id).Result;

            return this.Hal(content, new[]
            {
                this.SelfLink(this.manager, content),
                new Link("children", this.Url.RouteUrl("children", new { id = this.manager.GetIdAsync(content).Result})), 
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/children", Name = "children")]
        [HttpGet]
        public IActionResult GetChildren(string id)
        {
            var content = this.manager.FindByIdAsync(id).Result;
            var children = this.manager.GetChildrenAsync(content).Result;

            var response = new HalResponse(content);

            response.AddEmbeddedCollection("children", children);

            return this.Hal(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        [Route("{id}/children")]
        [HttpPost]
        public IActionResult PostChildren(string id, [FromBody] TContent child)
        {
            var parent = this.manager.FindByIdAsync(id).Result;

            if (parent == null)
            {
                return this.BadRequest();
            }

            this.manager.AddChildAsync(parent, child);
            var result = this.manager.UpdateAsync(parent).Result;
            return this.ChaosResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(string id, [FromBody] JsonPatchDocument<TContent> model)
        {
            var content = this.manager.FindByIdAsync(id).Result;

            model.ApplyTo(content, this.ModelState);
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }
            var result = this.manager.UpdateAsync(content).Result;

            return this.ChaosResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] TContent content)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var result = this.manager.CreateAsync(content).Result;

            return this.ChaosResult(result);
        }
    }
}
