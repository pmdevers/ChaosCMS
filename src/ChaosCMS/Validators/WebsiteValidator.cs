using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosCMS.Managers;
using ChaosCMS.Models.Website;

namespace ChaosCMS.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class WebsiteValidator : IWebsiteValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        public async Task<ChaosResult> ValidateAsync(WebsiteManager manager, Site site)
        {
            var errors = new List<ChaosError>();

            await this.ValidateNameAsync(manager, site, errors);

            if(errors.Count > 0)
            {
                return ChaosResult.Failed(errors.ToArray());
            }

            return ChaosResult.Success;
        }

        private async Task ValidateNameAsync(WebsiteManager manager, Site site, List<ChaosError> errors)
        {
            var existing = await manager.FindByNameAsync(site.Name);
            if(existing != null && (existing.Id == site.Id))
            {
                //errors.Add(manager.ErrorDescriber.DuplicateSite(site.Name));
            }
        }
    }
}
