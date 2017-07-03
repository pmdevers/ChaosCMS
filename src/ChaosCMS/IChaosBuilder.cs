using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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
    public interface IChaosBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        Type PageType { get; }
        /// <summary>
        /// 
        /// </summary>
        Type PageTypeType { get; }
        
        /// <summary>
        /// 
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPageManager"></typeparam>
        /// <returns></returns>
        IChaosBuilder AddPageManager<TPageManager>() where TPageManager : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IChaosBuilder AddPageStore<T>() where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IChaosBuilder AddPageValidator<T>() where T : class;

    }
}
