using ChaosCMS.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Internal;

namespace ChaosCMS.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class ContentStore<TContent, TContext, TKey> : IContentStore<TContent>
        where TContent : ChaosContent<TContent, TKey> 
        where TKey : struct, IEquatable<TKey>
        where TContext : DbContext
    {
        private bool _disposed;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="describer"></param>
        public ContentStore(TContext context, ChaosErrorDescriber describer = null)
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
        public virtual  TContext Context { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="ChaosErrorDescriber"/> for any error that occurred with the current operation.
        /// </summary>
        public virtual  ChaosErrorDescriber ErrorDescriber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual  bool AutoSaveChanges { get; set; } = true;

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

        /// <inhertdoc />
        public virtual  Task AddChildAsync(TContent parent, TContent child, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            child.Parent = parent;

            return TaskCache.CompletedTask;
        }

        /// <inhertdoc />
        public virtual  async Task<ChaosResult> CreateAsync(TContent content, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            Context.Add(content);
            await this.SaveChanges(cancellationToken);
            return ChaosResult.Success;
        }

        /// <inhertdoc />
        public virtual  async Task<ChaosResult> UpdateAsync(TContent content, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            Context.Attach(content);
            Context.Update(content);

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

        /// <inhertdoc />
        public virtual  async Task<ChaosResult> DeleteAsync(TContent content, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            Context.Remove(content);
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

        /// <inhertdoc />
        public virtual  Task<TContent> FindByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var contentId = ConvertIdFromString(id);
            return Contents.FirstOrDefaultAsync(r => r.Id.Equals(contentId), cancellationToken);
        }

        /// <inhertdoc />
        public virtual  Task<TContent> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Contents.FirstOrDefaultAsync(r => r.NormalizedName == normalizedName, cancellationToken);
        }

        /// <inhertdoc />
        public virtual  Task<TContent> FindByPageIdAsync(string pageId, string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var id = this.ConvertIdFromString(pageId);
            return Contents.FirstOrDefaultAsync(r => r.PageId.Equals(id) && r.NormalizedName == normalizedName, cancellationToken);
        }

        /// <inhertdoc />
        public virtual  Task<TContent> FindChildByNameAsync(TContent parent, string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Contents.FirstOrDefaultAsync(r => r.ParentId.Equals(parent.Id) && r.NormalizedName == normalizedName, cancellationToken);
        }

        /// <inhertdoc />
        public virtual  Task<ChaosPaged<TContent>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
        /// <inhertdoc />
        public virtual  Task<List<TContent>> GetChildrenAsync(TContent content, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Contents.Where(x => x.ParentId.Equals(content.Id)).ToListAsync(cancellationToken);
        }
        /// <inhertdoc />
        public virtual  Task<string> GetIdAsync(TContent content, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return Task.FromResult(this.ConvertIdToString(content.Id));
        }
        /// <inhertdoc />
        public virtual  Task<string> GetNameAsync(TContent content, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return Task.FromResult(content.Name);
        }
        /// <inhertdoc />
        public virtual  Task<string> GetTypeAsync(TContent content, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return Task.FromResult(content.Type);
        }
        /// <inhertdoc />
        public virtual  Task<string> GetValueAsync(TContent content, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return Task.FromResult(content.Value);
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
        /// Dispose the stores
        /// </summary>
        public virtual  void Dispose()
        {
            _disposed = true;
        }

        /// <summary>
        /// A navigation property for the roles the store contains.
        /// </summary>
        public virtual IQueryable<TContent> Contents
        {
            get { return Context.Set<TContent>(); }
        }

        #region Implementation of IContentStore<TContent>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SetValueAsync(TContent content, string value, CancellationToken cancellationToken)
        {
            content.Value = value;
            return Task.FromResult(0);
        }

        #endregion
    }
}
