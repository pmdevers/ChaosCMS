using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChaosCMS.Managers;

using Microsoft.AspNetCore.Mvc;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/resources", Name = "resources")]
    public class ResourceController : Controller
    {
        private readonly ResourceManager resourceManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceManager"></param>
        public ResourceController(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(this.resourceManager.Resources);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        [HttpGet("{*virtualPath}")]
        public IActionResult Get(string virtualPath)
        {
            var stream = this.resourceManager.FindByPath(virtualPath);
            if (stream == null)
            {
                return this.NotFound($"Virtual '{virtualPath}' path not found!");
            }

            return this.File(stream.CreateReadStream(), ChaosResourceTypes.GetContentType(virtualPath));
        }
    }
}
