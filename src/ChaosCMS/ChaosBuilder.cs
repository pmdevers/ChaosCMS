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
        /// <param name="adminPageType"></param>
        /// <param name="adminPageTypeType"></param>
        /// <param name="identityBuilder"></param>
        /// <param name="mvcBuilder"></param>
        /// <param name="services">The <see cref="IServiceCollection"/> to attach to.</param>
        public ChaosBuilder(Type pageType, Type pageTypeType, Type adminPageType, Type adminPageTypeType, IdentityBuilder identityBuilder, IMvcBuilder mvcBuilder, IServiceCollection services)
        {
            this.Services = services;
            this.IdentityBuilder = identityBuilder;
            this.MvcBuilder = mvcBuilder;

            this.AdminBuilder = new ChaosAdminBuilder(adminPageType, adminPageTypeType, mvcBuilder, services);
            this.FrontBuilder = new ChaosAdminBuilder(pageType, pageTypeType, mvcBuilder, services);
        }

        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> services are attached to.
        /// </summary>
        /// <value>
        /// The <see cref="IServiceCollection"/> services are attached to.
        /// </value>
        public IServiceCollection Services { get; private set; }

        /// <summary>
        /// Gets the <see cref="IdentityBuilder"/>
        /// </summary>
        public IdentityBuilder IdentityBuilder { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IChaosBuilder AdminBuilder { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public IChaosBuilder FrontBuilder { get; private set; }

        /// <summary>
        /// Gets the <see cref="IMvcBuilder"/>
        /// </summary>
        public IMvcBuilder MvcBuilder { get; set; }

        
    }
}