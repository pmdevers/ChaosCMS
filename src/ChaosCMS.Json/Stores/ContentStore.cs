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
    }
}