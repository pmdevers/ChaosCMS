using System;
using ChaosCMS.Helpers;
using ChaosCMS.Managers;

namespace ChaosCMS.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class PageManagerExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="manager"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static TPage FindByUrl<TPage>(this PageManager<TPage> manager, string url) 
            where TPage : class
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync<TPage>(() => manager.FindByUrlAsync(url));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetName<TPage>(this PageManager<TPage> manager, TPage page)
            where TPage : class
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync<string>(() => manager.GetNameAsync(page));
        }
    }
}