using ChaosCMS.Managers;
using System;
using Xunit;

namespace ChaosCMS.Test
{
    public class PageManagerTest
    {
        [Fact]
        public void PageManger_MustHaveStore()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => new PageManager<TestPage>(null, null, null, null, null, null, null));
        }
    }
}
