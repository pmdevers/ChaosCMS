using ChaosCMS.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChaosCMS.EntityFramework
{
    /// <summary>
    /// Creates a new instance of a persistence store for pages.
    /// </summary>
    /// <typeparam name="TPage">The type of the class representing a page</typeparam>
    /// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for a page</typeparam>
    public class PageStore<TPage, TContext, TKey> : IPageStore<TPage>
        where TPage : ChaosPage<TKey>
        where TContext : DbContext
        where TKey : struct, IEquatable<TKey>
    {
        private bool _disposed;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="describer"></param>
        public PageStore(TContext context, ChaosErrorDescriber describer = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            Context = context;
            ErrorDescriber = describer ?? new ChaosErrorDescriber();
        }


        /// <summary>
        /// Gets the database context for this store.
        /// </summary>
        public virtual TContext Context { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="ChaosErrorDescriber"/> for any error that occurred with the current operation.
        /// </summary>
        public virtual ChaosErrorDescriber ErrorDescriber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool AutoSaveChanges { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
        }

        /// <summary>Saves the current store.</summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        private async Task SaveChanges(CancellationToken cancellationToken)
        {
            if (AutoSaveChanges)
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        /// <inheritdoc />
        public async Task<ChaosPaged<TPage>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var pages = await  Pages.Skip(itemsPerPage * (page + 1)).Take(itemsPerPage).ToListAsync(cancellationToken);
            var count = await Pages.CountAsync();
            return new ChaosPaged<TPage> { CurrentPage = page, ItemsPerPage = itemsPerPage, Items = pages, TotalItems = count };
        }
        
        /// <inheritdoc />
        public async Task<ChaosResult> CreateAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            Context.Add(page);
            await this.SaveChanges(cancellationToken);
            return ChaosResult.Success;
        }
        /// <inheritdoc />
        public async Task<ChaosResult> DeleteAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            Context.Remove(page);
            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return ChaosResult.Failed(ErrorDescriber.ConcurrencyFailure());
            }
            return ChaosResult.Success;
        }

        /// <inheritdoc />
        public async Task<ChaosResult> UpdateAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            Context.Attach(page);
            Context.Update(page);

            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return ChaosResult.Failed(ErrorDescriber.ConcurrencyFailure());
            }

            return ChaosResult.Success;
        }

        /// <inheritdoc />
        public Task<TPage> FindByIdAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var pageId = ConvertIdFromString(id);
            return Pages.FirstOrDefaultAsync(r => r.Id.Equals(pageId), cancellationToken);
        }

        /// <inheritdoc />
        public Task<TPage> FindByUrlAsync(string urlPath, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Pages.FirstOrDefaultAsync(r => r.Url == urlPath, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetIdAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(this.ConvertIdToString(page.Id));
        }

        /// <inheritdoc />
        public Task<string> GetNameAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.Name);
        }
        /// <inheritdoc />
        public Task<string> GetUrlAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.Url);
        }

        /// <inheritdoc />
        public Task<string> GetTemplateAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.Template);
        }

        /// <summary>
        /// Throws if this class has been disposed.
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
        /// <summary>
        /// Converts the provided <paramref name="id"/> to a strongly typed key object.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>An instance of <typeparamref name="TKey"/> representing the provided <paramref name="id"/>.</returns>
        public virtual TKey ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(TKey);
            }
            return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
        }

        /// <summary>
        /// Converts the provided <paramref name="id"/> to its string representation.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>An <see cref="string"/> representation of the provided <paramref name="id"/>.</returns>
        public virtual string ConvertIdToString(TKey id)
        {
            if (id.Equals(default(TKey)))
            {
                return null;
            }
            return id.ToString();
        }

        /// <summary>
        /// A navigation property for the roles the store contains.
        /// </summary>
        public virtual IQueryable<TPage> Pages
        {
            get { return Context.Set<TPage>(); }
        }
    }
}