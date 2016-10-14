using ChaosCMS.Stores;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChaosCMS.EntityFramework
{
    /// <summary>
    /// Creates a new instance of a persistence store for pages.
    /// </summary>
    /// <typeparam name="TPage">The type of the class representing a page</typeparam>
    /// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for a page</typeparam>
    public class PageStore<TPage, TContext, TKey> : IPageStore<TPage>
        where TPage : class
        where TContext : DbContext
        where TKey : IEquatable<TKey>
    {
        
    }
}