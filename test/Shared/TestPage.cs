using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS.Test
{
    public class TestPage : TestPage<string>
    {
        public TestPage()
        {
            Id = Guid.NewGuid().ToString();
        }

        public TestPage(string name) : this()
        {
            Name = name;
        }
    }

    public class TestPage<TKey> where TKey : IEquatable<TKey>
    {
        public TestPage() { }

        public TestPage(string name) : this()
        {
            Name = name;
        }

        public virtual TKey Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Url { get; set; }
    }
}
