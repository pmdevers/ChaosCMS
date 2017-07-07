using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS
{
    public interface ISiteMaker
    {
        Task<ChaosResult> CreateAdministrator(string username, string password, string email);
        Task<ChaosResult> CreateHomepage();
        Task<ChaosResult> CreateLoginpage();
    }
}
