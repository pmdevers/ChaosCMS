using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaosCMS.Extensions;
using ChaosCMS.Managers;
using ChaosCMS;
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
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}", Name = "content")]
        [HttpGet]
        public IActionResult Get(string id)
        {
            var content = this.manager.FindByIdAsync(id).Result;

            return this.Hal(content, new[]
            {
                this.SelfLink(this.manager, content)
            });
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
            var page = this.manager.FindByIdAsync(id).Result;

            model.ApplyTo(page, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = this.manager.UpdateAsync(page).Result;

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
                return BadRequest(ModelState);
            }

            var result = this.manager.CreateAsync(content).Result;

            return this.ChaosResult(result);
        }
    }
}
