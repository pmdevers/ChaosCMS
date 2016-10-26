using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaosCMS.Managers;

namespace ChaosCMS.Validators
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    public interface IPageValidator<TPage>
        where TPage : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<ChaosResult> ValidateAsync(PageManager<TPage> manager, TPage page);
    }
}
