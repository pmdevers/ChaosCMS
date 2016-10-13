using Microsoft.AspNetCore.Mvc;
using ChaosCMS.Managers;
using ChaosCMS.Mvc.Models;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly PageManager<Page> pageManager;
        
        public HomeController(PageManager<Page> pageManager)
        {
            this.pageManager = pageManager;
        } 

        public IActionResult Index()
        {
            return Content(this.pageManager.Test());
        }

    }
}