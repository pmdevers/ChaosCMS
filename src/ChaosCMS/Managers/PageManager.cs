using System;
using ChaosCMS.Stores;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace ChaosCMS.Managers
{
    /// <summary>
    /// Provides the APIs for managing pages in a persistence store.
    /// </summary>
    /// <typeparam name="TPage">The type encapsulating a page.</typeparam>
    public class PageManager<TPage> : IDisposable
        where TPage : class
    {
        private readonly HttpContext context;

        /// <summary>
        /// The cancellation token assocated with the current HttpContext.RequestAborted or CancellationToken.None if unavailable.
        /// </summary>
        protected CancellationToken CancellationToken => context?.RequestAborted ?? CancellationToken.None;

        /// <summary>
        /// Constructs a new instance of <see cref="PageManager{TPage}"/>.
        /// </summary>
        /// <param name="store">The persistence store the manager will operate over.</param>
        /// <param name="optionsAccessor"></param>
        /// <param name="errors"></param>
        /// <param name="services"></param>
        /// <param name="logger"></param>
        public PageManager(IPageStore<TPage> store,
            IOptions<ChaosOptions> optionsAccessor,
            ChaosErrorDescriber errors,
            IServiceProvider services,
            ILogger<PageManager<TPage>> logger)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            this.Store = store;
            this.Options = optionsAccessor?.Value ?? new ChaosOptions();
            this.ErrorDescriber = errors;
            this.Logger = logger;

            if (services != null)
            {
                context = services.GetService<IHttpContextAccessor>()?.HttpContext;
            }
        }

        /// <summary>
        /// Gets or Sets the presistence store the manager operates over.
        /// </summary>
        /// <value>
        /// The presistence store the manager operates over.
        /// </value>
        protected internal IPageStore<TPage> Store { get; private set; }

        /// <summary>
        /// The <see cref="ILogger"/> used to log messages from the manager.
        /// </summary>
        /// <value>
        /// The <see cref="ILogger"/> used to log messages from the manager.
        /// </value>
        protected internal virtual ILogger Logger { get; set; }

        /// <summary>
        /// The <see cref="ChaosErrorDescriber"/> used to generate error messages.
        /// </summary>
        protected internal ChaosErrorDescriber ErrorDescriber { get; set; }

        /// <summary>
        /// The <see cref="ChaosOptions"/> used to configure Chaos.
        /// </summary>
        protected internal ChaosOptions Options { get; set; }

        /// <summary>
        /// Finds the assosiated page with the urlPath.
        /// </summary>
        /// <param name="urlPath">The url of the page.</param>
        /// <returns></returns>
        public virtual Task<TPage> FindByUrlAsync(string urlPath)
        {
            this.ThrowIfDisposed();
            if(urlPath == null)
            {
                throw new ArgumentNullException(nameof(urlPath));
            }

            return Store.FindByUrlAsync(urlPath, CancellationToken);
        }

        /// <summary>
        /// Gets the name of the page.
        /// </summary>
        /// <param name="page">The page to get the name from.</param>
        /// <returns></returns>
        public virtual Task<string> GetNameAsync(TPage page)
        {
            this.ThrowIfDisposed();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return this.Store.GetNameAsync(page, CancellationToken);
        }

        /// <summary>
        /// Gets the url of the page.
        /// </summary>
        /// <param name="page">The page to get the url from.</param>
        /// <returns></returns>
        public virtual Task<string> GetUrlAsync(TPage page)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return this.Store.GetUrlAsync(page, CancellationToken);
        }

        /// <summary>
        /// Gets the template of the Page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public virtual Task<string> GetTemplateAsync(TPage page)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return this.Store.GetTemplateAsync(page, CancellationToken);
        }

        #region IDisposable Support
        private bool isDisposed = false; // To detect redundant calls

        /// <summary>
        /// Releases the unmanaged resources used by the page manager and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !isDisposed)
            {
                Store.Dispose();
                isDisposed = true;
            }
        }

        /// <summary>
        /// Releases all resources used by the page manager.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion


        /// <summary>
        /// Throws if this class has been disposed.
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}