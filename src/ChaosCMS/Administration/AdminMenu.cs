using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Administration
{
    public class AdminMenu
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public IList<MenuItem> MenuItems {get;set; }
    }

    public class MenuItem
    {
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
