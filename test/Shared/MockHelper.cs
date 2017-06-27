using ChaosCMS.Managers;
using ChaosCMS.Stores;
using Moq;

namespace ChaosCMS.Test
{
    public static class MockHelper
    {
        public static Mock<PageManager<TPage, TContent>> MockPageManager<TPage, TContent>(IPageStore<TPage> store = null) where TPage : class
            where TContent : class
        {
            store = store ?? new Mock<IPageStore<TPage>>().Object;
            var mgr = new Mock<PageManager<TPage, TContent>>(store, null, null, null, null);
            return mgr;
        }

        public static Mock<ContentManager<TContent>> MockContentManager<TContent>(IContentStore<TContent> store = null) where TContent : class
        {
            store = store ?? new Mock<IContentStore<TContent>>().Object;
            var mgr = new Mock<ContentManager<TContent>>(store, null, null, null, null);
            return mgr;
        }
    }
}