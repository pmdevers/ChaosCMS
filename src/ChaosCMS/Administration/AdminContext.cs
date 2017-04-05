using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Administration
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class AdminContext<TUser> : IAdminContext
        where TUser : class
    {
        private readonly UserManager<TUser> userManager;
        private readonly HttpContext context;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="context"></param>
        /// <param name="options"></param>
        public AdminContext(UserManager<TUser> userManager, IHttpContextAccessor context, IOptions<ChaosOptions> options)
        {
            this.userManager = userManager;
            this.context = context.HttpContext;
            this.Options = options.Value;


            
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserName => userManager.GetUserName(context.User);

        /// <summary>
        /// 
        /// </summary>
        public ChaosOptions Options { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SideMenu Menu
        {
            get
            {
                return Options.Admin.AdminMenu;
            }
        }
    }
}
