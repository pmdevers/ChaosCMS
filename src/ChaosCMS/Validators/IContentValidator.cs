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
    /// <typeparam name="TContent"></typeparam>
    public interface IContentValidator<TContent>
        where TContent : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<ChaosResult> ValidateAsync(ContentManager<TContent> manager, TContent content);
    }
}
