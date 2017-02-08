using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChaosCMS.Json.Models;
using ChaosCMS.Stores;
using Microsoft.Extensions.Options;

namespace ChaosCMS.Json.Stores
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public class ContentStore<TContent> : JsonStore<TContent>, IContentStore<TContent>
        where TContent : JsonContent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsAccessor"></param>
        public ContentStore(IOptions<ChaosJsonStoreOptions> optionsAccessor) : base(optionsAccessor)
        {
        }

        /// <inheritdoc />
        public Task<ChaosPaged<TContent>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var allItems = ReadFile();
            var items = allItems.Where(x=> x.ParentId.Equals(default(Guid))).Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            return Task.FromResult(new ChaosPaged<TContent>
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = allItems.Count(),
                Items = items
            });

        }

        /// <inheritdoc />
        public Task<IEnumerable<TContent>> FindByPageIdAsync(string pageId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (string.IsNullOrWhiteSpace(pageId))
            {
                throw new ArgumentNullException(nameof(pageId));
            }
            var id = Guid.Empty;
            Guid.TryParse(pageId, out id);
            var item = this.ReadFile().Where(x => x.PageId.Equals(id));
            return Task.FromResult(item);
        }

        /// <inheritdoc />
        public Task<TContent> FindByNameAsync(string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            var item = this.ReadFile().FirstOrDefault(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            return Task.FromResult(item);
        }

        /// <inheritdoc />
        public Task<string> GetNameAsync(TContent content, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return Task.FromResult(content.Name);
        }
        /// <inheritdoc />
        public Task<string> GetTypeAsync(TContent content, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return Task.FromResult(content.Type);
        }
        /// <inheritdoc />
        public Task<string> GetValueAsync(TContent content, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return Task.FromResult(content.Value);
        }

        /// <inheritdoc />
        public Task<IList<TContent>> GetChildrenAsync(TContent content, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var items = this.ReadFile().Where(x=>x.ParentId.Equals(content.Id)).ToList();

            return Task.FromResult<IList<TContent>>(items);
        }

        /// <inheritdoc />
        public Task AddChildAsync(TContent parent, TContent child, CancellationToken cancellationToken)
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

            child.ParentId = parent.Id;

            return Task.FromResult(0);
        }
    }
}