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

namespace ChaosCMS.Managers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPageType"></typeparam>
    public class PageTypeManager<TPageType> : IDisposable
        where TPageType : class
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
        /// <param name="errorDescriber"></param>
        /// <param name="validators"></param>
        /// <param name="services"></param>
        /// <param name="logger"></param>
        public PageTypeManager(
            IPageTypeStore<TPageType> store, 
            IOptions<ChaosOptions> optionsAccessor, 
            ChaosErrorDescriber errorDescriber,
            IEnumerable<IPageTypeValidator<TPageType>> validators,
            IServiceProvider services,
            ILogger<PageTypeManager<TPageType>> logger
            )
        {
            if(store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            this.Store = store;
            this.Options = optionsAccessor?.Value ?? new ChaosOptions();
            this.ErrorDescriber = errorDescriber ?? new ChaosErrorDescriber();
            this.Logger = logger;

            if(validators != null)
            {
                foreach (var validator in validators)
                {
                    this.PageTypeValidators.Add(validator);
                }
            }

            if(services != null)
            {
                context = services.GetService<IHttpContextAccessor>()?.HttpContext;
            }

        }

        /// <summary>
        /// Gets or Sets the presistence store the manager operates over.
        /// </summary>
        protected internal IPageTypeStore<TPageType> Store { get; }
        
        /// <summary>
        /// The <see cref="ILogger"/> used to log messages from the manager.
        /// </summary>
        /// <value>
        /// The <see cref="ILogger"/> used to log messages from the manager.
        /// </value>
        protected internal virtual ILogger Logger { get; set; }
        
        /// <summary>
        /// The IPageTypeValidator{TPageType} used to validate page types.
        /// </summary>
        protected internal IList<IPageTypeValidator<TPageType>> PageTypeValidators { get; } = new List<IPageTypeValidator<TPageType>>();
        
        /// <summary>
        /// The <see cref="ChaosErrorDescriber"/> used to generate error messages.
        /// </summary>
        protected internal ChaosErrorDescriber ErrorDescriber { get; set; }
        
        /// <summary>
        /// The <see cref="ChaosOptions"/> used to configure Chaos.
        /// </summary>
        protected internal ChaosOptions Options { get; set; }

        #region CRUD

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public virtual async Task<ChaosResult> CreateAsync(TPageType pageType)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }

            var result = await this.ValidateInternalAsync(pageType);

            if (!result.Succeeded)
            {
                return result;
            }

            return await this.Store.CreateAsync(pageType, CancellationToken);
        }

        #region READ

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public Task<ChaosPaged<TPageType>> FindPagedAsync(int page = 1, int itemsPerPage = 25)
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
            return this.Store.FindPagedAsync(page, itemsPerPage, this.CancellationToken);
        }

        /// <summary>
        /// Finds the page with the pageId
        /// </summary>
        /// <param name="pageTypeId">The id of the page.</param>
        /// <returns></returns>
        public virtual Task<TPageType> FindByIdAsync(string pageTypeId)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (pageTypeId == null)
            {
                throw new ArgumentNullException(nameof(pageTypeId));
            }

            return this.Store.FindByIdAsync(pageTypeId, CancellationToken);
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public virtual async Task<ChaosResult> UpdateAsync(TPageType pageType)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }

            var result = await this.ValidateInternalAsync(pageType);
            if (!result.Succeeded)
            {
                return result;
            }

            return await this.Store.UpdateAsync(pageType, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public virtual Task<ChaosResult> DeleteAsync(TPageType pageType)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }

            return this.Store.DeleteAsync(pageType, CancellationToken);
        }

        #endregion

        #region Gets and Sets

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public virtual Task<string> GetIdAsync(TPageType pageType)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }

            return this.Store.GetIdAsync(pageType, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public Task<string> GetNameAsync(TPageType pageType)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }

            return this.Store.GetNameAsync(pageType, CancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task SetNameAsync(TPageType pageType, string name)
        {
            CancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }

            return this.Store.SetNameAsync(pageType, name, CancellationToken);
        }

        #endregion


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
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing)
        {
            if(disposing && !this.isDisposed)
            {
                this.Store.Dispose();
                this.isDisposed = true;
            }
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

        private async Task<ChaosResult> ValidateInternalAsync(TPageType pageType)
        {
            var errors = new List<ChaosError>();

            foreach (var validator in this.PageTypeValidators)
            {
                var result = await validator.ValidateAsync(this, pageType);

                if (!result.Succeeded)
                {
                    errors.AddRange(result.Errors);
                }
            }

            if(errors.Count > 0)
            {
                return ChaosResult.Failed(errors.ToArray());
            }

            return ChaosResult.Success;
        }
    }
}
