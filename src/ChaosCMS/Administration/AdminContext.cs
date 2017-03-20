using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Administration
{
    public class AdminContext<TUser> : IAdminContext
        where TUser : class
    {
        private readonly UserManager<TUser> userManager;
        private readonly HttpContext context;

        public AdminContext(UserManager<TUser> userManager, IHttpContextAccessor context)
        {
            this.userManager = userManager;
            this.context = context.HttpContext;
        }

        public string UserName => userManager.GetUserName(context.User);
        public IDictionary<string, IList<AdminMenu>> Menu
        {
            get
            {
                var menu = new Dictionary<string, IList<AdminMenu>>();
                var general = new List<AdminMenu>();

                general.Add(new AdminMenu() { Icon ="files-o", Name = "Pages", MenuItems = new List<MenuItem>()
                {
                    new MenuItem() { Name = "Pages", Controller = "Pages", Action = "index" },
                    new MenuItem() { Name = "Create Page", Controller = "Pages", Action = "Create" },
                    //new MenuItem() { Name = "Pages", Controller = "Pages", Action = "index" },
                }
                });

                menu.Add("General", general);
                return menu;
            }
        }
    }
}
