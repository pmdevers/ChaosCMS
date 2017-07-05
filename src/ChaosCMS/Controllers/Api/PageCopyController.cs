using ChaosCMS.Converters;
using ChaosCMS.Extensions;
using ChaosCMS.Managers;
using ChaosCMS.Models.Pages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// A controller for handling a page
    /// </summary>
    [Route("api/page", Name = "copy")]
    public class PageCopyController<TPage> : Controller
        where TPage : class
    {
        private readonly IConverter<TPage, TPage> converter;
        private readonly PageManager<TPage> manager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="manager"></param>
        public PageCopyController(IConverter<TPage, TPage> converter, PageManager<TPage> manager)
        {
            this.converter = converter;
            this.manager = manager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}/copy")]
        public async Task<IActionResult> Post(string id, [FromBody] CopyPageModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var source = await this.manager.FindByIdAsync(id);
            
            if(source == null)
            {
                return this.ChaosResults(this.manager.ErrorDescriber.PageIdNotFound(id));
            }
                                    
            var result = await this.converter.Convert(source, config => {
                config.AlwaysNew = true;
                config.BeforeCreate = async (dest) => await manager.SetUrlAsync(dest, model.NewUrl);
            });

            return this.ChaosResults(result);
        }

    }
}
