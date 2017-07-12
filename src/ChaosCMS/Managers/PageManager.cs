using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChaosCMS.Stores;
using ChaosCMS.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ChaosCMS.Models.Pages;

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
        /// <param name="urlFormatter"></param>
        /// <param name="validators"></param>
        /// <param name="services"></param>
        /// <param name="logger"></param>
        public PageManager(IPageStore<TPage> store,
            IOptions<ChaosOptions> optionsAccessor,
            ChaosErrorDescriber errors,
            IUrlFormatter urlFormatter,
            IEnumerable<IPageValidator<TPage>> validators,
            IServiceProvider services,
            ILogger<PageManager<TPage>> logger)
        {
            this.Store = store ?? throw new ArgumentNullException(nameof(store)); ;
            this.Options = optionsAccessor?.Value ?? new ChaosOptions();
            this.ErrorDescriber = errors ?? new ChaosErrorDescriber();
            this.UrlFormatter = urlFormatter ?? new DefaultUrlFormatter(optionsAccessor);
            this.Logger = logger;

            if (validators != null)
            {
                foreach (var validator in validators)
                {
                    this.PageValidators.Add(validator);
                }
            }

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
        /// The <see cref="IPageValidator{TPage}"/> used to validate pages.
        /// </summary>
        protected internal IList<IPageValidator<TPage>> PageValidators { get; } = new List<IPageValidator<TPage>>();

        /// <summary>
        /// The <see cref="ChaosErrorDescriber"/> used to generate error messages.
        /// </summary>
        protected internal ChaosErrorDescriber ErrorDescriber { get; set; }

        /// <summary>
        /// The <see cref="ChaosOptions"/> used to configure Chaos.
        /// </summary>
        protected internal ChaosOptions Options { get; set; }

        /// <summary>
        /// The <see cref="IUrlFormatter"/> used for formatting urls.
        /// </summary>
        protected internal IUrlFormatter UrlFormatter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool SupportsContents
        {
            get
            {
                this.ThrowIfDisposed();
                return this.Store is IPageContentStore<TPage>;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public virtual async Task<ChaosResult> CreateAsync(TPage page)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            var result = await ValidateInternal(page);

            if (!result.Succeeded)
            {
                return result;
            }

            await this.FormatUrlAsync(page);

            return await this.Store.CreateAsync(page, CancellationToken);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public virtual async Task<ChaosResult> UpdateAsync(TPage page)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            var result = await ValidateInternal(page);

            if (!result.Succeeded)
            {
                return result;
            }

            await this.FormatUrlAsync(page);

            return await this.Store.UpdateAsync(page, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public virtual Task<ChaosResult> DeleteAsync(TPage page)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return this.Store.DeleteAsync(page, CancellationToken);
        }

        private async Task<ChaosResult> ValidateInternal(TPage page)
        {
            var error = new List<ChaosError>();
            foreach (var validator in PageValidators)
            {
                var result = await validator.ValidateAsync(this, page);
                if (!result.Succeeded)
                {
                    error.AddRange(result.Errors);
                }
            }

            if (error.Count > 0)
            {
                return ChaosResult.Failed(error.ToArray());
            }

            return ChaosResult.Success;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public Task<ChaosPaged<TPage>> FindPagedAsync(int page = 1, int itemsPerPage = 25)
        {
            this.CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page < 1)
            {
                throw new InvalidOperationException(Resources.NegativePage);
            }
            if (itemsPerPage > this.Options.MaxItemsPerPage)
            {
                throw new InvalidOperationException(Resources.FormatMaxItemsPerPage(this.Options.MaxItemsPerPage));
            }
            return this.Store.FindPagedAsync(this.context.Request, page, itemsPerPage, this.CancellationToken);
        }

        /// <summary>
        /// Finds the page with the pageId
        /// </summary>
        /// <param name="pageId">The id of the page.</param>
        /// <returns></returns>
        public virtual Task<TPage> FindByIdAsync(string pageId)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (pageId == null)
            {
                throw new ArgumentNullException(nameof(pageId));
            }

            return this.Store.FindByIdAsync(pageId, CancellationToken);
        }

        /// <summary>
        /// Finds the page with the externalId
        /// </summary>
        /// <param name="origin">the origin source.</param>
        /// <returns>an instance of the page if founce.</returns>
        public virtual Task<TPage> FindByOriginAsync(string origin)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (string.IsNullOrEmpty(origin))
            {
                throw new ArgumentNullException(nameof(origin));
            }
            return this.Store.FindByOriginAsync(origin, CancellationToken);
        }

        /// <summary>
        /// Finds a page by a statusCode
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public virtual Task<TPage> FindByStatusCodeAsync(int statusCode)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            return this.Store.FindByStatusCodeAsync(statusCode, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Task<TPage> FindCurrentAsync()
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return this.Store.FindByRequestAsync(context.Request, CancellationToken);
        }

        private string FormatUrl(string urlPath)
        {
            var segments = urlPath?.Split('/');
            var formatedSegments = new List<string>();
            foreach(var segment in segments)
            {
                formatedSegments.Add(this.UrlFormatter.FormatUrl(segment));
            }
            return string.Join("/", formatedSegments);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public virtual Task<string> GetIdAsync(TPage page)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return this.Store.GetIdAsync(page, CancellationToken);
        }

        /// <summary>
        /// Gets the name of the page.
        /// </summary>
        /// <param name="page">The page to get the name from.</param>
        /// <returns></returns>
        public virtual Task<string> GetNameAsync(TPage page)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return this.Store.GetNameAsync(page, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual Task SetNameAsync(TPage page, string name)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return this.Store.SetNameAsync(page, name, CancellationToken);
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
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return this.Store.GetUrlAsync(page, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public virtual Task<string> GetHostAsync(TPage page)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return this.Store.GetHostAsync(page, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public virtual Task SetHostAsync(TPage page, string host)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return this.Store.SetHostAsync(page, host, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public virtual async Task SetUrlAsync(TPage page, string url)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            var formattedUrl = FormatUrl(url ?? await this.GetNameAsync(page));
            await this.Store.SetUrlAsync(page, formattedUrl, CancellationToken);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public virtual Task SetTemplateAsync(TPage page, string url)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return this.Store.SetTemplateAsync(page, url, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public virtual Task SetPageTypeAsync(TPage page, string pageType)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return this.Store.SetPageTypeAsync(page, pageType, CancellationToken);
        }

        /// <summary>
        /// Gets the type of page
        /// </summary>
        /// <param name="page"></param>
        /// <returns>the name of the type</returns>
        public virtual Task<string> GetPageTypeAsync(TPage page)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return this.Store.GetPageTypeAsync(page, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public Task<int> GetStatusCodeAsync(TPage page)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return this.Store.GetStatusCodeAsync(page, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual Task SetStatusCodeAsync(TPage page, int code)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return this.Store.SetStatusCodeAsync(page, code, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task SetOriginAsync(TPage page, string id)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Store.SetOriginAsync(page, id, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public virtual Task<IList<Content>> GetContentAsync(TPage page)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var store = this.GetPageContentStore();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return store.GetContentAsync(page, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public virtual Task SetContentAsync(TPage page, IEnumerable<Content> content)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var store = this.GetPageContentStore();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return store.SetContentAsync(page, content, CancellationToken);
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

        #endregion IDisposable Support


        private IPageContentStore<TPage> GetPageContentStore()
        {
            this.ThrowIfDisposed();
            var store = this.Store as IPageContentStore<TPage>;
            if(store == null)
            {
                throw new NotSupportedException(Resources.FormatStoreIsNotOfType(typeof(IPageContentStore<TPage>).Name));
            }

            return store;
        }

        private async Task FormatUrlAsync(TPage page)
        {
            var formattedUrl = FormatUrl(await this.GetUrlAsync(page));
            await this.Store.SetUrlAsync(page, formattedUrl, CancellationToken);
        }

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