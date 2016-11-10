using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChaosCMS.Stores;
using ChaosCMS.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChaosCMS.Managers
{
    /// <summary>
    /// Provides the APIs for managing content in a presistence store.
    /// </summary>
    /// <typeparam name="TContent">The type encapsulating a content.</typeparam>
    public class ContentManager<TContent> : IDisposable
        where TContent : class
    {

        private readonly HttpContext context;

        /// <summary>
        /// The cancellation token assocated with the current HttpContext.RequestAborted or CancellationToken.None if unavailable.
        /// </summary>
        protected CancellationToken CancellationToken => context?.RequestAborted ?? CancellationToken.None;

        #region Constructor and Properties
        /// <summary>
        /// Constructs a new instance of <see cref="ContentManager{TContent}"/>
        /// </summary>
        /// <param name="store">The persistence store the manager will operate over.</param>
        /// <param name="optionsAccessor"></param>
        /// <param name="errors"></param>
        /// <param name="validators"></param>
        /// <param name="services"></param>
        /// <param name="logger"></param>
        public ContentManager(IContentStore<TContent> store,
            IOptions<ChaosOptions> optionsAccessor,
            ChaosErrorDescriber errors,
            IEnumerable<IContentValidator<TContent>> validators,
            IServiceProvider services,
            ILogger<ContentManager<TContent>> logger)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            this.Store = store;
            this.Options = optionsAccessor?.Value ?? new ChaosOptions();
            this.ErrorDescriber = errors;
            this.Logger = logger;

            if (validators != null)
            {
                foreach (var validator in validators)
                {
                    this.ContentValidators.Add(validator);
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
        protected internal IContentStore<TContent> Store { get; private set; }

        /// <summary>
        /// The <see cref="ILogger"/> used to log messages from the manager.
        /// </summary>
        /// <value>
        /// The <see cref="ILogger"/> used to log messages from the manager.
        /// </value>
        protected internal virtual ILogger Logger { get; set; }

        /// <summary>
        /// The <see cref="IContentValidator{TContent}"/> used to validate pages.
        /// </summary>
        protected internal IList<IContentValidator<TContent>> ContentValidators { get; } = new List<IContentValidator<TContent>>();

        /// <summary>
        /// The <see cref="ChaosErrorDescriber"/> used to generate error messages.
        /// </summary>
        protected internal ChaosErrorDescriber ErrorDescriber { get; set; }

        /// <summary>
        /// The <see cref="ChaosOptions"/> used to configure Chaos.
        /// </summary>
        protected internal ChaosOptions Options { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task<ChaosResult> CreateAsync(TContent content)
        {
            CancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var result = await this.ValidateInternal(content);
            if (!result.Succeeded)
            {
                return result;
            }

            return await this.Store.CreateAsync(content, CancellationToken);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public virtual async Task<ChaosResult> UpdateAsync(TContent content)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var result = await ValidateInternal(content);

            if (!result.Succeeded)
            {
                return result;
            }

            return await this.Store.UpdateAsync(content, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public Task<ChaosPaged<TContent>> FindPagedAsync(int page = 1, int itemsPerPage = 25)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page < 1)
            {
                throw new InvalidOperationException(Resources.NegativePage);
            }
            if (itemsPerPage > Options.MaxItemsPerPage)
            {
                throw new InvalidOperationException(Resources.FormatMaxItemsPerPage(Options.MaxItemsPerPage));
            }
            return this.Store.FindPagedAsync(page, itemsPerPage, CancellationToken);
        }

        /// <summary>
        /// Finds the content with the contentId
        /// </summary>
        /// <param name="contentId">The id of the content.</param>
        /// <returns></returns>
        public virtual Task<TContent> FindByIdAsync(string contentId)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (contentId == null)
            {
                throw new ArgumentNullException(nameof(contentId));
            }

            return this.Store.FindByIdAsync(contentId, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public virtual Task<string> GetIdAsync(TContent content)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return this.Store.GetIdAsync(content, CancellationToken);
        }

        /// <summary>
        /// Gets the name of the content.
        /// </summary>
        /// <param name="content">The content to get the name from.</param>
        /// <returns></returns>
        public virtual Task<string> GetNameAsync(TContent content)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return this.Store.GetNameAsync(content, CancellationToken);
        }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <param name="content">The content to get the type from.</param>
        /// <returns></returns>
        public virtual Task<string> GetTypeAsync(TContent content)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return this.Store.GetTypeAsync(content, CancellationToken);
        }

        /// <summary>
        /// Gets the value of the content.
        /// </summary>
        /// <param name="content">The content to get the value from.</param>
        /// <returns></returns>
        public virtual Task<string> GetValueAsync(TContent content)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return this.Store.GetValueAsync(content, CancellationToken);
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

        #region Private Methods

        private async Task<ChaosResult> ValidateInternal(TContent content)
        {
            var error = new List<ChaosError>();
            foreach (var validator in ContentValidators)
            {
                var result = await validator.ValidateAsync(this, content);
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
