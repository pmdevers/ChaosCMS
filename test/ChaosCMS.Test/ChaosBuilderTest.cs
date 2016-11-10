using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChaosCMS.Stores;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ChaosCMS.Managers;
using ChaosCMS.Validators;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChaosCMS.Test
{
    public class ChaosBuilderTest
    {
        [Fact]
        public void CanOverridePageStore()
        {
            var services = new ServiceCollection();

            services.AddChaos<TestPage, TestContent>()
                .AddPageStore<UberStore>();

            var store = services.BuildServiceProvider().GetRequiredService<IPageStore<TestPage>>() as UberStore;

            Assert.NotNull(store);
        }

        [Fact]
        public void CanOverrideContentStore()
        {
            var services = new ServiceCollection();

            services.AddChaos<TestPage, TestContent>()
                .AddContentStore<UberStore>();

            var store = services.BuildServiceProvider().GetRequiredService<IContentStore<TestContent>>() as UberStore;

            Assert.NotNull(store);
        }

        [Fact]
        public void CanOverridePageManager()
        {
            var services = new ServiceCollection();
            services.AddChaos<TestPage, TestContent>()
                .AddPageStore<UberStore>()
                .AddPageManager<UberPageManager>();

            var uberPageManager = services.BuildServiceProvider().GetRequiredService<PageManager<TestPage>>() as UberPageManager;

            Assert.NotNull(uberPageManager);
        }

        [Fact]
        public void CanOverrideContentManager()
        {
            var services = new ServiceCollection();
            services.AddChaos<TestPage, TestContent>()
                .AddContentStore<UberStore>()
                .AddContentManager<UberContentManager>();

            var uberContentManager = services.BuildServiceProvider().GetRequiredService<ContentManager<TestContent>>() as UberContentManager;

            Assert.NotNull(uberContentManager);
        }

        [Fact]
        public void AddManagerWithWrongTypesThrows()
        {
            var services = new ServiceCollection();
            var builder = services.AddChaos<TestPage, TestContent>();
            Assert.Throws<InvalidOperationException>(() => builder.AddPageManager<PageManager<TestPage>>());
            Assert.Throws<InvalidOperationException>(() => builder.AddPageManager<object>());
            Assert.Throws<InvalidOperationException>(() => builder.AddContentManager<ContentManager<TestContent>>());
            Assert.Throws<InvalidOperationException>(() => builder.AddContentManager<object>());
        }


        private class UberPageManager : PageManager<TestPage>
        {
            public UberPageManager(IPageStore<TestPage> store, IOptions<ChaosOptions> optionsAccessor, ChaosErrorDescriber errors, IEnumerable<IPageValidator<TestPage>> validators, IServiceProvider services, ILogger<PageManager<TestPage>> logger) : base(store, optionsAccessor, errors, validators, services, logger)
            {
            }
        }

        private class UberContentManager : ContentManager<TestContent>
        {
            public UberContentManager(IContentStore<TestContent> store, IOptions<ChaosOptions> optionsAccessor, ChaosErrorDescriber errors, IEnumerable<IContentValidator<TestContent>> validators, IServiceProvider services, ILogger<ContentManager<TestContent>> logger) : base(store, optionsAccessor, errors, validators, services, logger)
            {
            }
        }

        private class UberStore : IPageStore<TestPage>, IContentStore<TestContent>
        {
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public Task<TestPage> FindByIdAsync(string pageId, CancellationToken cancelationToken)
            {
                throw new NotImplementedException();
            }

            public Task<ChaosResult> CreateAsync(TestContent content, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<ChaosResult> UpdateAsync(TestContent content, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            Task<ChaosPaged<TestContent>> IContentStore<TestContent>.FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetIdAsync(TestContent content, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetNameAsync(TestContent content, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetTypeAsync(TestContent content, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetValueAsync(TestContent content, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<TestPage> FindByUrlAsync(string urlPath, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetIdAsync(TestPage page, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetNameAsync(TestPage page, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetUrlAsync(TestPage page, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetTemplateAsync(TestPage page, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            Task<TestContent> IContentStore<TestContent>.FindByIdAsync(string contentId, CancellationToken cancelationToken)
            {
                throw new NotImplementedException();
            }

            public Task<ChaosPaged<TestPage>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public Task<ChaosResult> UpdateAsync(TestPage page, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
