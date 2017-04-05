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
    public interface IAdminContext
    {
        /// <summary>
        /// 
        /// </summary>
        string UserName { get; }
        /// <summary>
        /// 
        /// </summary>
        ChaosOptions Options { get; set; }

        /// <summary>
        /// 
        /// </summary>
        SideMenu Menu { get; }
    }
}
