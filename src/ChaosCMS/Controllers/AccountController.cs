using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ChaosCMS.Models.Account;
using Microsoft.Extensions.Options;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    [Route("", Name = "Account")]
    public class AccountController<TUser> : Controller
        where TUser : class, new()
    {
        private readonly UserManager<TUser> userManager;
        private readonly SignInManager<TUser> signInManager;
        private ChaosOptions options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="optionsAccessor"></param>
        public AccountController(UserManager<TUser> userManager,  SignInManager<TUser> signInManager, IOptions<ChaosOptions> optionsAccessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.options = optionsAccessor.Value ?? new ChaosOptions();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new TUser();
                await this.userManager.SetUserNameAsync(user, model.Email);
                await this.userManager.SetEmailAsync(user, model.Email);
                var result = await this.userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: model.IsPersistent);
                    return Redirect(options.Security.RedirectAfterLoginPath);
                }
            }

            return Redirect(options.Security.RegistrationPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await this.signInManager.PasswordSignInAsync(model.Username, model.Password, model.Remember, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    throw new NotImplementedException();
                }

                if (result.IsLockedOut)
                {
                    return View("lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return LocalRedirect(this.options.Security.RedirectAfterLogoutPath);
        }
    }
}
