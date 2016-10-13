using ChaosCMS.Stores;
namespace ChaosCMS.EntityFramework
{
    /// <summary>
    /// Creates a new instance of a persistence store for pages.
    /// </summary>
    /// <typeparam name="TPage">The type of the class representing a page</typeparam>
    public class PageStore<TPage> : IPageStore<TPage>
        where TPage : class
    {
        
    }
}