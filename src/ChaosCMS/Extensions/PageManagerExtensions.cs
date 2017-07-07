using System;
using ChaosCMS.Helpers;
using ChaosCMS.Managers;
using ChaosCMS.Models.Pages;
using System.Collections.Generic;

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
        public static string GetId<TPage>(this PageManager<TPage> manager, TPage page)
            where TPage : class
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.GetIdAsync(page));
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
            return AsyncHelper.RunSync(() => manager.GetNameAsync(page));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetUrl<TPage>(this PageManager<TPage> manager, TPage page)
            where TPage : class
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.GetUrlAsync(page));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static int GetStatusCode<TPage>(this PageManager<TPage> manager, TPage page)
            where TPage : class
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.GetStatusCodeAsync(page));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetTemplate<TPage>(this PageManager<TPage> manager, TPage page)
            where TPage : class
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.GetTemplateAsync(page));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetPageType<TPage>(this PageManager<TPage> manager, TPage page)
            where TPage : class
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.GetPageTypeAsync(page));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="manager"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IList<Content> GetContent<TPage>(this PageManager<TPage> manager, TPage page)
            where TPage : class
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.GetContentAsync(page));
        }
    }
}