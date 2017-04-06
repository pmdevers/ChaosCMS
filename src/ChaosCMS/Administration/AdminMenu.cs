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
    public class AdminMenu
    {
        /// <summary>
        /// Gets or Sets the FontAwesome icon
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IList<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
    /// <summary>
    /// 
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Action { get; set; }
    }
}
