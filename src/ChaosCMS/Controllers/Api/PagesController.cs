using Microsoft.AspNetCore.Mvc;

namespace ChaosCMS.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Route("/admin/pages", Name = "Pages")]
    public class PagesController<TPage> : Controller
        where TPage : class, new()
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "Index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View("pages");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Route("create", Name = "Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View("createpage");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public IActionResult Post(TPage page)
        {
            return View("createpage");
        }
    }
}