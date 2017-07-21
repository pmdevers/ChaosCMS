using System;

using ChaosCMS.Helpers;
using ChaosCMS.Managers;

namespace ChaosCMS.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class PageTypeManagerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPageType"></typeparam>
        /// <param name="manager"></param>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public static string GetId<TPageType>(this PageTypeManager<TPageType> manager, TPageType pageType) where TPageType : class
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.GetIdAsync(pageType));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TPageType"></typeparam>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetName<TPageType>(this PageTypeManager<TPageType> manager, TPageType page)
            where TPageType : class
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.GetNameAsync(page));
        }
    }
}
