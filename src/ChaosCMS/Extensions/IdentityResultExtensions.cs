using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class IdentityResultExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ChaosResult ToChaosResult(this IdentityResult result)
        {
            if (result.Succeeded)
            {
                return ChaosResult.Success;
            }

            return ChaosResult.Failed(result.Errors.Select(x => new ChaosError() { Code = x.Code, Description = x.Description }).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IdentityResult ToIdentityResult(this ChaosResult result)
        {
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(result.Errors.Select(x => new IdentityError() { Code = x.Code, Description = x.Description }).ToArray());
        }
    }
}
