using ChaosCMS.Extensions;
using ChaosCMS.Managers;
using ChaosCMS.Models.Account;
using ChaosCMS.Models.Pages;
using ChaosCMS.Models.Setup;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("admin/setup", Name = "Setup")]
    public class SetupController : Controller
    {
        private readonly ISiteMaker siteMaker;

        /// <summary>
        /// 
        /// </summary>
        
        public SetupController(ISiteMaker siteMaker)
        {
            this.siteMaker = siteMaker;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View("wizzard");
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index(SetupModel model)
        {
            if (this.ModelState.IsValid)
            {
                var errors = new List<ChaosError>();
                var result = await this.siteMaker.CreateAdministrator(model.Username, model.Password, model.Email);

                errors.AddRange(result.Errors);

                if (result.Succeeded)
                {
                    if (model.HomePage)
                        result = await this.siteMaker.CreateHomepage();

                    errors.AddRange(result.Errors);

                    if (model.LoginPage)
                        result = await this.siteMaker.CreateLoginpage();

                    errors.AddRange(result.Errors);
                } 

                if (errors.Count > 0)
                {
                    this.AddErrors(ChaosResult.Failed(errors.ToArray()));
                }
                else
                {
                    return RedirectToRoute("admin");
                }
            }
            return View("wizzard", model);
        }
    }
}
