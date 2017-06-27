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
        /// <typeparam name="TContent"></typeparam>
        /// <param name="manager"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static TPage FindByUrl<TPage, TContent>(this PageManager<TPage, TContent> manager, string url) 
            where TPage : class
            where TContent : class
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
        /// <typeparam name="TContent"></typeparam>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetName<TPage, TContent>(this PageManager<TPage, TContent> manager, TPage page)
            where TPage : class
            where TContent : class
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync<string>(() => manager.GetNameAsync(page));
        }
    }
}