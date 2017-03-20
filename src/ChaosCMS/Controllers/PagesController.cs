using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Controllers
{
    [Route("/admin/pages", Name = "Pages")]
    public class PagesController : Controller
    {
        [Route("", Name ="Index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View("pages");
        }

        [Route("create", Name = "Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View("createpage");
        }
    }
}
