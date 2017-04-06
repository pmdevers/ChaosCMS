using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ChaosCMS.Managers;
using ChaosCMS.Stores;

namespace ChaosCMS.Test
{
    public static class MockHelper
    {
        public static Mock<PageManager<TPage>> MockPageManager<TPage>(IPageStore<TPage> store = null) where TPage : class
        {
            store = store ?? new Mock<IPageStore<TPage>>().Object;
            var mgr = new Mock<PageManager<TPage>>(store, null, null, null, null);
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
