using System;

namespace ChaosCMS.Json.Models
{
    public class JsonPage
    {
        public JsonPage()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}