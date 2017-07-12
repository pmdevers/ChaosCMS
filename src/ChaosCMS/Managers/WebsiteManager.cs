using ChaosCMS.Models.Website;
using ChaosCMS.Stores;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using ChaosCMS.Validators;

namespace ChaosCMS.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public class WebsiteManager : IDisposable
    {
        private bool isDisposed;

        private readonly HttpContext context;

        /// <summary>
        /// The cancellation token assocated with the current HttpContext.RequestAborted or CancellationToken.None if unavailable.
        /// </summary>
        protected CancellationToken CancellationToken => context?.RequestAborted ?? CancellationToken.None;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="store"></param>
        /// <param name="optionsAccessor"></param>
        /// <param name="validators"></param>
        /// <param name="errors"></param>
        /// <param name="lookupNormalizer"></param>
        /// <param name="logger"></param>
        /// <param name="services"></param>
        public WebsiteManager(
            IWebsiteStore store, 
            IOptions<ChaosOptions> optionsAccessor, 
            IEnumerable<IWebsiteValidator> validators, 
            ChaosErrorDescriber errors, 
            ILookupNormalizer lookupNormalizer, 
            ILogger<WebsiteManager> logger, 
            IServiceProvider services)
        {
            this.Store = store ?? throw new ArgumentNullException(nameof(store));

            this.Options = optionsAccessor?.Value ?? new ChaosOptions();
            this.ErrorDescriber = errors;
            this.LookupNormalizer = lookupNormalizer;
            this.Logger = logger;

            if(validators != null)
            {
                foreach(var v in validators)
                {
                    this.SiteValidators.Add(v);
                }
            }

            if (services != null)
            {
                context = services.GetService<IHttpContextAccessor>()?.HttpContext;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal IWebsiteStore Store { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected internal ChaosOptions Options { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected internal ChaosErrorDescriber ErrorDescriber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected internal ILookupNormalizer LookupNormalizer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected internal IList<IWebsiteValidator> SiteValidators { get; } = new List<IWebsiteValidator>();

        /// <summary>
        /// 
        /// </summary>
        protected internal ILogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public virtual Task<ChaosResult> CreateAsync(Site site)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(site == null)
            {
                throw new ArgumentNullException(nameof(site));
            }


                        // TODO: Validation

            return this.Store.CreateAsync(site, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public virtual Task<ChaosResult> UpdateAsync(Site site)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(site == null)
            {
                throw new ArgumentNullException(nameof(site));
            }

            return this.Store.UpdateAsync(site, this.CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public virtual Task<ChaosResult> DeleteAsync(Site site)
        {
            this.CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(site == null)
            {
                throw new ArgumentNullException(nameof(site));
            }

            return this.Store.DeleteAsync(site, this.CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public virtual Task<Site> FindByHostAsync(HostString host)
        {
            this.ThrowIfDisposed();
            return this.Store.FindByHostAsync(host, CancellationToken);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual Task<Site> FindByNameAsync(string name)
        {
            this.ThrowIfDisposed();
            if(name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            name = this.Normalize(name);

            return this.Store.FindByNameAsync(name, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Task<Site> FindCurrentAsync()
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return this.Store.FindByRequestAsync(context.Request, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string Normalize(string value)
        {
            return (this.LookupNormalizer == null) ? value : LookupNormalizer.Normalize(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing)
        {
            if(disposing && this.isDisposed)
            {
                this.isDisposed = true;
                this.Store.Dispose();
            }
        }

        private void ThrowIfDisposed()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }
    }
}
