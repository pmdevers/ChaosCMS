using System;
using System.Collections.Generic;
using System.Text;
using ChaosCMS.Models.Pages;

namespace ChaosCMS.AzureCosmosDB.Models
{
    public class CosmosPage : IEntity
    {
        public string Id { get; set; }
        public string Origin { get; set; }
        public string Url { get;  set; }
        public string Template { get;  set; }
        public int StatusCode { get;  set; }
        public string Type { get;  set; }
        public string Name { get;  set; }
        public IList<string> Hosts { get; set; } = new List<string>();
        public IList<Content> Content { get; set; } = new List<Content>();
    }
}
