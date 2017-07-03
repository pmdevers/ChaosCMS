using ChaosCMS.Managers;
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
    /// <typeparam name="TPageType"></typeparam>
    public interface IPageTypeValidator<TPageType>
        where TPageType : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="pageType"></param>
        /// <returns></returns>
        Task<ChaosResult> ValidateAsync(PageTypeManager<TPageType> manager, TPageType pageType);
    }
}
