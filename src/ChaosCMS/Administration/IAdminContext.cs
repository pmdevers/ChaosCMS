using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Administration
{
    public interface IAdminContext
    {
        string UserName { get; }
        IDictionary<string, IList<AdminMenu>> Menu { get; }
    }
}
