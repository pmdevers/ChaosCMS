using ChaosCMS.Managers;
using ChaosCMS.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using ChaosCMS.Validators;

namespace ChaosCMS
{
    /// <summary>
    /// Helper functions for configuring Chaos services
    /// </summary>
    public class ChaosBuilder
    {
        /// <summary>
        /// Creates a new instance of <see cref="ChaosBuilder"/>.
        /// </summary>
        /// <param name="pageType">The <see cref="Type"/> to use for the pages.</param>
        /// <param name="services">The <see cref="IServiceCollection"/> to attach to.</param>
        public ChaosBuilder(Type pageType, IServiceCollection services)
        {
            this.Services = services;
            this.PageType = pageType;
        }

        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> services are attached to.
        /// </summary>
        /// <value>
        /// The <see cref="IServiceCollection"/> services are attached to.
        /// </value>
        public IServiceCollection Services { get; private set; }

        /// <summary>
        /// Gets the <see cref="Type"/> used for pages
        /// </summary>
        public Type PageType { get; private set; }


        private ChaosBuilder AddScoped(Type serviceType, Type concreteType)
        {
            Services.AddScoped(serviceType, concreteType);
            return this;
        }

        /// <summary>
        /// Adds an <see cref="ChaosErrorDescriber"/>.
        /// </summary>
        /// <typeparam name="TDescriber">The type of the error describer.</typeparam>
        /// <returns>The current <see cref="ChaosErrorDescriber"/> instance.</returns>
        public virtual ChaosBuilder AddErrorDescriber<TDescriber>() where TDescriber : ChaosErrorDescriber
        {
            Services.AddScoped<ChaosErrorDescriber, TDescriber>();
            return this;
        }

        /// <summary>
        /// Adds a <see cref="PageManager{TPage}"/> for the <seealso cref="PageType"/>.
        /// </summary>
        /// <typeparam name="TPageManager">The type of the page manager to add.</typeparam>
        /// <returns>The current <see cref="ChaosBuilder"/> instance.</returns>
        public virtual ChaosBuilder AddPageManager<TPageManager>() where TPageManager : class
        {
            var pageManagerType = typeof(PageManager<>).MakeGenericType(PageType);
            var customType = typeof(TPageManager);
            if(pageManagerType == customType ||
                !pageManagerType.GetTypeInfo().IsAssignableFrom(customType.GetTypeInfo()))
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
        public virtual ChaosBuilder AddPageStore<T>() where T : class
        {
            return AddScoped(typeof(IPageStore<>).MakeGenericType(PageType), typeof(T));
        }

        /// <summary>
        /// Adds an <see cref="IPageValidator{TPage}"/> for the <seealso cref="PageType"/>.
        /// </summary>
        /// <typeparam name="T">The page validator.</typeparam>
        /// <returns>The current <see cref="ChaosBuilder"/> instance.</returns>
        public virtual ChaosBuilder AddPageValidator<T>() where T : class 
        {
            return AddScoped(typeof(IPageValidator<>).MakeGenericType(PageType), typeof(T));
        }
    }
}
