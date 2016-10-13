using ChaosCMS.Stores;

namespace ChaosCMS.Managers
{
    /// <summary>
    /// Provides the APIs for managing pages in a persistence store.
    /// </summary>
    /// <typeparam name="TPage">The type encapsulating a page.</typeparam>
    public class PageManager<TPage>
        where TPage : class
    {
        /// <summary>
        /// Constructs a new instance of <see cref="PageManager{TPage}"/>.
        /// </summary>
        /// <param name="store">The persistence store the manager will operate over.</param>
        public PageManager(IPageStore<TPage> store)
        {
            
        }

        /// <summary>
        /// Test Method
        /// </summary>
        public string Test(){
            return "Hello Test";
        }
    }
}