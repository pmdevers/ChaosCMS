using System;
using System.Reflection;
using ChaosCMS.Managers;
using ChaosCMS.Stores;
using ChaosCMS.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

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
        /// <param name="pageTypeType"></param>
        /// <param name="contentType">The <see cref="Type"/> to use for the content.</param>
        /// <param name="identityBuilder"></param>
        /// <param name="mvcBuilder"></param>
        /// <param name="services">The <see cref="IServiceCollection"/> to attach to.</param>
        public ChaosBuilder(Type pageType, Type pageTypeType, Type contentType, IdentityBuilder identityBuilder, IMvcBuilder mvcBuilder, IServiceCollection services)
        {
            this.Services = services;
            this.PageType = pageType;
            this.ContentType = contentType;
            this.PageTypeType = pageTypeType;
            this.IdentityBuilder = identityBuilder;
            this.MvcBuilder = mvcBuilder;
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

        /// <summary>
        /// Gets the <see cref="Type"/> used for content
        /// </summary>
        public Type ContentType { get; private set; }

        /// <summary>
        /// Gets the <see cref="Type"/> used for page type
        /// </summary>
        public Type PageTypeType { get; private set; }

        /// <summary>
        /// Gets the <see cref="IdentityBuilder"/>
        /// </summary>
        public IdentityBuilder IdentityBuilder { get; private set; }

        /// <summary>
        /// Gets the <see cref="IMvcBuilder"/>
        /// </summary>
        public IMvcBuilder MvcBuilder { get; set; }

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
            var pageManagerType = typeof(PageManager<,>).MakeGenericType(PageType, ContentType);
            var customType = typeof(TPageManager);

            if (pageManagerType == customType || !pageManagerType.GetTypeInfo().IsAssignableFrom(customType.GetTypeInfo()))
            {
                throw new InvalidOperationException(Resources.FormatInvalidManagerType(customType.Name, "PageManager", this.PageType));
            }
            return this;
        }

        /// <summary>
        /// Adds a <see cref="ContentManager{TContent}"/> for the <seealso cref="ContentType"/>.
        /// </summary>
        /// <typeparam name="TContentManager">The type of the page manager to add.</typeparam>
        /// <returns>The current <see cref="ChaosBuilder"/> instance.</returns>
        public virtual ChaosBuilder AddContentManager<TContentManager>() where TContentManager : class
        {
            var contentManagerType = typeof(ContentManager<>).MakeGenericType(ContentType);
            var customType = typeof(TContentManager);
            if (contentManagerType == customType ||
                !contentManagerType.GetTypeInfo().IsAssignableFrom(customType.GetTypeInfo()))
            {
                throw new InvalidOperationException(Resources.FormatInvalidManagerType(customType.Name, "ContentManager", this.ContentType));
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
        /// Adds an <see cref="IContentStore{TContent}"/> for the <seealso cref="ContentType"/>.
        /// </summary>
        /// <typeparam name="T">The content store.</typeparam>
        /// <returns>The current <see cref="ChaosBuilder"/> instance.</returns>
        public virtual ChaosBuilder AddContentStore<T>() where T : class
        {
            return AddScoped(typeof(IContentStore<>).MakeGenericType(ContentType), typeof(T));
        }

        /// <summary>
        /// Adds an <see cref="IPageValidator{TPage, TContent}"/> for the <seealso cref="PageType"/>.
        /// </summary>
        /// <typeparam name="T">The page validator.</typeparam>
        /// <returns>The current <see cref="ChaosBuilder"/> instance.</returns>
        public virtual ChaosBuilder AddPageValidator<T>() where T : class
        {
            return AddScoped(typeof(IPageValidator<,>).MakeGenericType(PageType, ContentType), typeof(T));
        }

        /// <summary>
        /// Adds an <see cref="IContentValidator{TPage}"/> for the <seealso cref="PageType"/>.
        /// </summary>
        /// <typeparam name="T">The content validator.</typeparam>
        /// <returns>The current <see cref="ChaosBuilder"/> instance.</returns>
        public virtual ChaosBuilder AddContentValidator<T>() where T : class
        {
            return AddScoped(typeof(IContentValidator<>).MakeGenericType(ContentType), typeof(T));
        }
    }
}