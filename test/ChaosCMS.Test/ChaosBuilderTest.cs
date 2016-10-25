using System;
using System.Threading;
using System.Threading.Tasks;
using ChaosCMS.Stores;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ChaosCMS.Managers;
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

            services.AddChaos<TestPage>()
                .AddPageStore<UberStore>();

            var store = services.BuildServiceProvider().GetRequiredService<IPageStore<TestPage>>() as UberStore;

            Assert.NotNull(store);
        }

        [Fact]
        public void CanOverridePageManager()
        {
            var services = new ServiceCollection();
            services.AddChaos<TestPage>()
                .AddPageStore<UberStore>()
                .AddPageManager<UberPageManager>();

            var uberPageManager = services.BuildServiceProvider().GetRequiredService<PageManager<TestPage>>() as UberPageManager;

            Assert.NotNull(uberPageManager);
        }

        [Fact]
        public void AddManagerWithWrongTypesThrows()
        {
            var services = new ServiceCollection();
            var builder = services.AddChaos<TestPage>();
            Assert.Throws<InvalidOperationException>(() => builder.AddPageManager<PageManager<TestPage>>());
            Assert.Throws<InvalidOperationException>(() => builder.AddPageManager<object>());
        }


        private class UberPageManager : PageManager<TestPage>
        {
            public UberPageManager(IPageStore<TestPage> store, IOptions<ChaosOptions> optionsAccessor, ChaosErrorDescriber errors, IServiceProvider services, ILogger<PageManager<TestPage>> logger) : base(store, optionsAccessor, errors, services, logger)
            {
            }
        }
        private class UberStore : IPageStore<TestPage>
        {
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public Task<TestPage> FindByIdAsync(string pageId, CancellationToken cancelationToken)
            {
                throw new NotImplementedException();
            }

            public Task<TestPage> FindByUrlAsync(string urlPath, CancellationToken cancellationToken)
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
        }
    }
}
