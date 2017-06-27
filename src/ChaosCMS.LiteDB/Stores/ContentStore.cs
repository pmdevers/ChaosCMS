using ChaosCMS.LiteDB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using ChaosCMS.Stores;
using System.Threading;
using System.Threading.Tasks;

namespace ChaosCMS.LiteDB.Stores
{
    public class ContentStore<TContent> : LiteDBStore<TContent>, IContentStore<TContent>
        where TContent : LiteDBContent<TContent>
    {
        public ContentStore(IOptions<ChaosLiteDBStoreOptions> optionsAccessor) : base(optionsAccessor)
        {
        }

        public Task AddChildAsync(TContent parent, TContent child, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TContent> FindByNameAsync(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TContent> FindByPageIdAsync(string pageId, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TContent> FindChildByNameAsync(TContent parent, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<TContent>> GetChildrenAsync(TContent content, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return Task.FromResult(content.Children);
        }

        public Task<string> GetNameAsync(TContent content, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return Task.FromResult(content.Name);
        }

        public Task<string> GetTypeAsync(TContent content, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return Task.FromResult(content.Value);
        }

        public Task<string> GetValueAsync(TContent content, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            return Task.FromResult(content.Value);
        }

        public Task SetValueAsync(TContent content, string value, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            content.Value = value;
            return Task.FromResult(0);
        }
    }
}
