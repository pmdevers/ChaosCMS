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
    public interface ISiteMaker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<ChaosResult> CreateAdministrator(string username, string password, string email);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ChaosResult> CreateHomepage();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ChaosResult> CreateLoginpage();
    }
}
