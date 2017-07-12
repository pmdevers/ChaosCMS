using ChaosCMS.Managers;
using ChaosCMS.Models.Website;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebsiteValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        Task<ChaosResult> ValidateAsync(WebsiteManager manager, Site site);
    }
}
