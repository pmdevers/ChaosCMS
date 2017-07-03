using System;

namespace ChaosCMS.Test
{
    public class TestContent : TestContent<string>
    {
        public TestContent()
        {
            Id = Guid.NewGuid().ToString();
        }

        public TestContent(string name) : this()
        {
            Name = name;
        }
    }

    public class TestContent<TKey> where TKey : IEquatable<TKey>
    {
        public TestContent()
        {
        }

        public TestContent(string name) : this()
        {
            Name = name;
            Type = "string";
            Value = string.Empty;
        }

        public virtual TKey Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string Value { get; set; }
    }
}