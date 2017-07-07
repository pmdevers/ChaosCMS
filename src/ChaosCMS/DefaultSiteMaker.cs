using ChaosCMS.Extensions;
using ChaosCMS.Managers;
using ChaosCMS.Models.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public class DefaultSiteMaker<TPage, TUser> : ISiteMaker
        where TPage : class, new()
        where TUser : class, new()
    {
        private readonly PageManager<TPage> pageManager;
        private readonly UserManager<TUser> userManager;
        private readonly SignInManager<TUser> signInManager;
        private readonly HttpContext context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageManager"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="services"></param>
        public DefaultSiteMaker(PageManager<TPage> pageManager, UserManager<TUser> userManager, SignInManager<TUser> signInManager, IServiceProvider services)
        {
            this.pageManager = pageManager ?? throw new ArgumentNullException(nameof(pageManager));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            if (services != null)
            {
                context = services.GetService<IHttpContextAccessor>()?.HttpContext;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ChaosResult> CreateAdministrator(string username, string password, string email)
        {
            var user = new TUser();
            await this.userManager.SetUserNameAsync(user, username);
            await this.userManager.SetEmailAsync(user, email);
            var result = await this.userManager.CreateAsync(user, password);


            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
            }

            return result.ToChaosResult();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ChaosResult> CreateHomepage()
        {
            var homepage = new TPage();

            await this.pageManager.SetNameAsync(homepage, "Home");
            await this.pageManager.SetPageTypeAsync(homepage, "Default");
            await this.pageManager.SetStatusCodeAsync(homepage, 200);
            await this.pageManager.SetTemplateAsync(homepage, "Index");
            await this.pageManager.SetUrlAsync(homepage, "/");
            await this.pageManager.AddHostAsync(homepage, this.context.Request.Host.Host);

            var result = await this.pageManager.CreateAsync(homepage);

            if (result.Succeeded)
            {
                var content = new Content() { Name = "title", Type = "string", Value = "Hello World!" };
                await this.pageManager.SetContentAsync(homepage, new[] { content });
                await this.pageManager.UpdateAsync(homepage);
            }

            return result;
        }
    }
}
