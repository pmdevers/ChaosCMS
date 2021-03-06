﻿using ChaosCMS.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ChaosCMS.Json.Models;
using Microsoft.Extensions.Options;

namespace ChaosCMS.Json.Stores
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPageType"></typeparam>
    public class PageTypeStore<TPageType> : JsonStore<TPageType>, IPageTypeStore<TPageType>
        where TPageType : JsonPageType
    {

        /// <summary>
        /// 
        /// </summary>
        public PageTypeStore(IOptions<ChaosJsonStoreOptions> options)
            : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<ChaosPaged<TPageType>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var items = this.Collection.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            return Task.FromResult(new ChaosPaged<TPageType>
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = this.Collection.Count(),
                Items = items
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetNameAsync(TPageType pageType, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }
            return Task.FromResult(pageType.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SetNameAsync(TPageType pageType, string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }

            pageType.Name = name;

            return Task.FromResult(0);
        }
    }
}
