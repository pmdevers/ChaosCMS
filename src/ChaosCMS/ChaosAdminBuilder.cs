using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ChaosCMS.Managers;
using ChaosCMS.Stores;
using ChaosCMS.Validators;
using System.Reflection;

namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    public class ChaosAdminBuilder : IChaosBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="pageTypeType"></param>
        /// <param name="mvcBuilder"></param>
        /// <param name="services"></param>
        public ChaosAdminBuilder(Type pageType, Type pageTypeType, IMvcBuilder mvcBuilder, IServiceCollection services)
        {
            this.PageType = pageType;
            this.PageTypeType = pageTypeType;
            MvcBuilder = mvcBuilder;
            Services = services;
        }

        /// <summary>
        /// 
        /// </summary>
        public Type PageType { get; }
        /// <summary>
        /// 
        /// </summary>
        public Type PageTypeType { get; }
        /// <summary>
        /// 
        /// </summary>
        public IMvcBuilder MvcBuilder { get; }
        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }

        private IChaosBuilder AddScoped(Type serviceType, Type concreteType)
        {
            Services.AddScoped(serviceType, concreteType);
            return this;
        }

        /// <summary>
        /// Adds an <see cref="ChaosErrorDescriber"/>.
        /// </summary>
        /// <typeparam name="TDescriber">The type of the error describer.</typeparam>
        /// <returns>The current <see cref="ChaosErrorDescriber"/> instance.</returns>
        public virtual IChaosBuilder AddErrorDescriber<TDescriber>() where TDescriber : ChaosErrorDescriber
        {
            Services.AddScoped<ChaosErrorDescriber, TDescriber>();
            return this;
        }

        /// <summary>
        /// Adds a <see cref="PageManager{TPage}"/> for the <seealso cref="PageType"/>.
        /// </summary>
        /// <typeparam name="TPageManager">The type of the page manager to add.</typeparam>
        /// <returns>The current <see cref="ChaosBuilder"/> instance.</returns>
        public virtual IChaosBuilder AddPageManager<TPageManager>() where TPageManager : class
        {
            var pageManagerType = typeof(PageManager<>).MakeGenericType(PageType);
            var customType = typeof(TPageManager);

            if (pageManagerType == customType || !pageManagerType.GetTypeInfo().IsAssignableFrom(customType.GetTypeInfo()))
            {
                throw new InvalidOperationException(Resources.FormatInvalidManagerType(customType.Name, "PageManager", this.PageType));
            }
            return this;
        }

        /// <summary>
        /// Adds an <see cref="IPageStore{TPage}"/> for the <seealso cref="PageType"/>.
        /// </summary>
        /// <typeparam name="T">The page store.</typeparam>
        /// <returns>The current <see cref="ChaosBuilder"/> instance.</returns>
        public virtual IChaosBuilder AddPageStore<T>() where T : class
        {
            return AddScoped(typeof(IPageStore<>).MakeGenericType(PageType), typeof(T));
        }


        /// <summary>
        /// Adds an <see cref="IPageValidator{TPage}"/> for the <seealso cref="PageType"/>.
        /// </summary>
        /// <typeparam name="T">The page validator.</typeparam>
        /// <returns>The current <see cref="ChaosBuilder"/> instance.</returns>
        public virtual IChaosBuilder AddPageValidator<T>() where T : class
        {
            return AddScoped(typeof(IPageValidator<>).MakeGenericType(PageType), typeof(T));
        }
    }
}
