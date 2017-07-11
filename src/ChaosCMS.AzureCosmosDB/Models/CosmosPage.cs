using System;
using System.Collections.Generic;
using System.Text;

namespace ChaosCMS.AzureCosmosDB.Models
{
    public class CosmosPage : IEntity
    {
        public string Id { get; set; }
        public string Origin { get; set; }
        public string Url { get; internal set; }
        public string Template { get; internal set; }
        public int StatusCode { get; internal set; }
        public string Type { get; internal set; }
        public string Name { get; internal set; }
        public IList<string> Hosts { get; internal set; }
    }
}
