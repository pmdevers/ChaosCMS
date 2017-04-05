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
    public class SideMenu
    {
        Dictionary<string, List<AdminMenu>> sideMenu = new Dictionary<string, List<AdminMenu>>();
        /// <summary>
        /// 
        /// </summary>
        public SideMenu()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public string[] Sections => sideMenu.Keys.ToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void AddSection(string name)
        {
            if (!sideMenu.ContainsKey(name))
            {
                sideMenu.Add(name, new List<AdminMenu>());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="menu"></param>
        public void AddAdminMenu(string section, AdminMenu menu)
        {
            if (!sideMenu.ContainsKey(section))
            {
                AddSection(section);
            }

            sideMenu[section].Add(menu);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="adminMenu"></param>
        /// <param name="item"></param>
        public void AddMenuItem(string section, string adminMenu, MenuItem item)
        {
            var menu = GetAdminMenu(section, adminMenu);
            menu.MenuItems.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public IList<AdminMenu> GetSection(string section)
        {
            if (!sideMenu.ContainsKey(section))
            {
                AddSection(section);
            }

            return sideMenu[section];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="menuName"></param>
        /// <returns></returns>
        public AdminMenu GetAdminMenu(string section, string menuName)
        {
            var menus = GetSection(section);
            var item = menus.FirstOrDefault(x => x.Name.Equals(menuName, StringComparison.CurrentCultureIgnoreCase));
            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public MenuItem CreateMenuItem(string name, string controller, string action)
        {
            return new MenuItem { Name = name, Controller = controller, Action = action };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public AdminMenu CreateAdminMenu(string name, string icon)
        {
            return new AdminMenu() { Name = name, Icon = icon };
        }
    }
}
